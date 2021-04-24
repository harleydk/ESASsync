using Synchronization.ESAS.DAL;
using Synchronization.ESAS.DAL.Models;
using Synchronization.ESAS.UtilityComponents;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;

namespace Synchronization.ESAS
{
    public partial class EsasSynchronizationService : ServiceBase
    {
        private readonly int _minuteIntervalBetweenEsasWsHealthcheck;
        private readonly IEnumerable<SyncStrategyBundle> _syncStrategyBundles;
        private readonly IEsasDbContextFactory _esasStagingDbContextFactory;
        private readonly EsasWebServiceHealthChecker _esasWebServiceHealthChecker;
        private readonly ILogger _logger;
        private readonly IEmailService _emailService;

        private bool _isSynchronizationActive;

        public EsasSynchronizationService(IList<SyncStrategyBundle> syncStrategyBundles, IEsasDbContextFactory esasStagingDbContextFactory, EsasWebServiceHealthChecker esasWebServiceHealthChecker, ILogger logger, IEmailService emailService)
        {
            logger.LogInformation("Service initializing...");
            _syncStrategyBundles = syncStrategyBundles;
            _esasStagingDbContextFactory = esasStagingDbContextFactory;
            _esasWebServiceHealthChecker = esasWebServiceHealthChecker;
            _logger = logger;
            _emailService = emailService;
            _isSynchronizationActive = false;

            _minuteIntervalBetweenEsasWsHealthcheck = Convert.ToInt32(ConfigurationManager.AppSettings["MinuteIntervalBetweenEsasWsHealthcheck"]);
            _logger.LogInformation($"{System.Environment.MachineName} - Esas sync Service was initialized. Happy trails!");
        }


        protected override void OnStart(string[] args)
        {
            double serviceIntervalInMilliseconds = TimeSpan.FromMinutes(1).TotalMilliseconds; // set a timer with a one-minute interval
            this._logger.LogInformation($"Started kp.esas.syncservice, with an update interval of {serviceIntervalInMilliseconds} milliseconds.");
            System.Timers.Timer serviceTimer = new System.Timers.Timer(serviceIntervalInMilliseconds);
            serviceTimer.Elapsed += PerformSynchronization;
            serviceTimer.Start();

            _emailService.SendStatusMail($"Started kp.esas.syncservice on {System.Environment.MachineName}");
        }


        public void PerformSynchronization(object sender, System.Timers.ElapsedEventArgs e)
        {
            _logger.LogInformation("PerformSynchronization() was called...");

            try
            {
                if (_isSynchronizationActive) // do nothing if a sync is already in progress.
                    return;

                // for every '_minuteIntervalBetweenEsasWsHealthcheck' minutes, do a health-check
                if (DateTime.Now.Minute % _minuteIntervalBetweenEsasWsHealthcheck == 0)
                {
                    _logger.LogInformation($"Running an ESAS WS-Healthcheck...");
                    EsasWebServiceHealthCheck healthCheck = _esasWebServiceHealthChecker.PerformHealthCheck();
                    var dbContext = _esasStagingDbContextFactory.CreateDbContext();
                    using (dbContext)
                    {
                        dbContext.HealthChecks.Add(healthCheck);
                        dbContext.SaveChanges();
                    }

                    _logger.LogInformation($"ESAS WS-Healthcheck reported status {healthCheck.HttpStatusCode}.");
                    if (healthCheck.HttpStatusCode != "200")
                    {
                        Exception healthCheckException = new Exception(healthCheck.Message);
                        _logger.LogError(healthCheckException.Message, healthCheckException);
                        return;
                    }
                }

                _isSynchronizationActive = true;
                executeSyncStrategies();
                _isSynchronizationActive = false;
            }
            catch (Exception ex)
            {
                // Allow the service to continue, but do log the error.
                _logger.LogError("ERROR: " + ex.Message, ex);
                _isSynchronizationActive = false;
            }
        }
        public void executeSyncStrategies()
        {
            var syncBundlesToRun = _syncStrategyBundles.Where(bundle => bundle.IsTimeToExecute(DateTime.Now));
            foreach (var syncBundle in syncBundlesToRun)
            {
                string syncStartMessage = $"{System.Environment.MachineName}: Executing sync-bundle {syncBundle.GetType().Name}.";
                System.Diagnostics.Debug.WriteLine(syncStartMessage);
                _logger.LogInformation(syncStartMessage);
                syncBundle.ExecuteSync();
            }
        }

        protected override void OnStop()
        {
            _emailService.SendStatusMail($"Stopped kp.esas.syncservice on {System.Environment.MachineName}");
        }
    }
}

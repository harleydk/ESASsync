using Synchronization.ESAS.DAL;
using System.Collections.Generic;
using System.Linq;
using System;
using Synchronization.ESAS.DAL.Models;
using System.Diagnostics;


namespace Synchronization.ESAS.SyncDestinations
{
    public class EsasStagingDbSyncDestination : IEsasSyncDestination
    {
        private IEnumerable<IEsasStagingDbDestination> _esasStagingDbSyncStrategies;
        private IEsasDbContextFactory _esasDbContextFactory;

        public EsasStagingDbSyncDestination(IEsasDbContextFactory esasDbContextFactory, IEnumerable<IEsasStagingDbDestination> esasStagingDbSyncStrategies)
        {
            _esasDbContextFactory = esasDbContextFactory;
            _esasStagingDbSyncStrategies = esasStagingDbSyncStrategies;
        }

        public EsasSendResult Deliver(object[] objects)
        {
            EsasSendResult esasSendResult = new EsasSendResult();
            esasSendResult.SendDestinationStrategyName = GetType().Name;
            esasSendResult.SendStartTime = DateTime.Now;

            if (!objects.Any())
            {
                // TODO: do some logging here
                esasSendResult.SendToDestinationStatus = EsasOperationResultStatus.OperationFailed;
            }
            else
            {
                Type t = objects.First().GetType();
                var stagingDbSyncStrategy = _esasStagingDbSyncStrategies.Single(strat => strat.IsStrategyMatch(t));

                Stopwatch sp = new Stopwatch();
                sp.Start();
                stagingDbSyncStrategy.Deliver(objects, _esasDbContextFactory);
                sp.Stop();

                esasSendResult.SendToDestinationStatus = EsasOperationResultStatus.OperationSuccesful;
                esasSendResult.SendTimeMs = sp.ElapsedMilliseconds;
            }

            esasSendResult.SendEndTime = DateTime.Now;
            return esasSendResult;
        }

    }
}



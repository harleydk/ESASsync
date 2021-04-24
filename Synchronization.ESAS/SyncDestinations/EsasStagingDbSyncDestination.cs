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
            esasSendResult.SendDestinationStrategyName = this.GetType().Name;
            esasSendResult.SendStartTimeUTC = DateTime.UtcNow;

            if (!objects.Any())
            {
                // TODO: do some logging here
                esasSendResult.SendToDestinationStatus = EsasOperationResultStatus.OperationFailed;
            }
            else
            {
                Type t = objects.First().GetType(); // used to discern which destination-strategy we'll use.
                var stagingDbSyncStrategy = _esasStagingDbSyncStrategies.Single(strat => strat.IsStrategyMatch(t));

                Stopwatch sp = new Stopwatch();
                sp.Start();

                try
                {
                    stagingDbSyncStrategy.Deliver(objects, _esasDbContextFactory);
                    esasSendResult.SendToDestinationStatus = EsasOperationResultStatus.OperationSuccesful;
                }
                catch (Exception ex)
                {
                    esasSendResult.SendToDestinationStatus = EsasOperationResultStatus.OperationFailed;
                }
              
                sp.Stop();
                esasSendResult.SendTimeMs = sp.ElapsedMilliseconds;
            }

            esasSendResult.SendEndTimeUTC = DateTime.UtcNow;
            return esasSendResult;
        }

    }
}



using Microsoft.Extensions.Logging;
using Synchronization.ESAS.Synchronizations;
using System;
using System.Collections.Generic;

namespace Synchronization.ESAS
{
    /// <summary>
    /// En facade-klasse for en mængde af esas synkroniseringer, der skal eksekveres på et specifikt tidspunkt.
    /// </summary>
    public class SyncStrategyBundle
    {
        private readonly TimeSpan _syncTime;
        private readonly IEnumerable<IEsasSyncStrategy> _syncStrategies;
        private readonly ILogger _logger;

        public SyncStrategyBundle(TimeSpan syncTime, IEnumerable<IEsasSyncStrategy> syncStrategies, ILogger logger)
        {
            _syncTime = syncTime;
            _syncStrategies = syncStrategies;
            _logger = logger;
        }

        public bool IsTimeToExecute(DateTime comparisonTime)
        {
            if (_syncTime.Hours == comparisonTime.Hour && _syncTime.Minutes == comparisonTime.Minute)
                return true;

            return false;
        }

        public void ExecuteSync()
        {
            foreach (IEsasSyncStrategy esasSyncStrategy in _syncStrategies)
            {
                string syncStrategyStartMessage = $"Sync-strategy: Executing sync-strategy {esasSyncStrategy} - start.";
                System.Diagnostics.Debug.WriteLine(syncStrategyStartMessage);
                _logger.LogInformation(syncStrategyStartMessage);

                esasSyncStrategy.ExecuteSyncStrategy();

                syncStrategyStartMessage = $"Sync-strategy: Executing sync-strategy {esasSyncStrategy} - end.";
                System.Diagnostics.Debug.WriteLine(syncStrategyStartMessage);
                _logger.LogInformation(syncStrategyStartMessage);

            }
        }
    }
}

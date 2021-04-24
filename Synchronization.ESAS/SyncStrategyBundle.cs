using Synchronization.ESAS.Synchronizations;
using Microsoft.Extensions.Logging;
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

        public SyncStrategyBundle(TimeSpan syncTime, IEnumerable<IEsasSyncStrategy> syncStrategies, ILogger logger )
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
            _logger.LogInformation($"Logging ");
            foreach( IEsasSyncStrategy esasSyncStrategy in _syncStrategies)
            {
                string syncStrategyStartMessage = $"{System.Environment.MachineName}: Executing sync-strategy {esasSyncStrategy}.";
                System.Diagnostics.Debug.WriteLine(syncStrategyStartMessage);
                _logger.LogInformation(syncStrategyStartMessage);
                esasSyncStrategy.ExecuteSyncStrategy();
            }
        }
    }
}

using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Logging;
using Synchronization.ESAS.DAL.Models;

namespace Synchronization.ESAS.Synchronizations.EntityLoaderStrategies
{
    /// <summary>
    /// Specific loader-strategy for 'OptionSetValueString' entiteten - denne har ikke et 'modified timestamp'
    /// som de øvrige entiteter.
    /// </summary>
    public class OptionSetValueLoadStrategy : IEsasEntitiesLoaderStrategy
    {
        private Default.Container _esasContainer;
        private readonly EsasWsContextFactory _esasContextFactory;
        private readonly ILogger _logger;

        public OptionSetValueLoadStrategy(EsasWsContextFactory esasContextFactory, ILogger logger)
        {
            _esasContextFactory = esasContextFactory;
            _logger = logger;
        }

        public (EsasLoadResult esasLoadResult, object[] loadedObjects) Load()
        {
            _esasContainer = _esasContextFactory.Create();

            EsasLoadResult loadResult = new EsasLoadResult();
            loadResult.LoaderStrategyName = this.GetType().Name;
            loadResult.LoadStartTimeUTC = DateTime.UtcNow;
            DateTime deltaTimeLoadvalue = new DateTime(1963, 11, 22).ToUniversalTime(); // J.F.K. RIP
            loadResult.ModifiedOnDateTimeUTC = deltaTimeLoadvalue;

            object[] loadedObjects = null;
            try
            {
                Stopwatch sp = new Stopwatch();
                sp.Start();
                loadedObjects = _esasContainer.OptionSetValueString.ToArray();
                sp.Stop();

                loadResult.EsasLoadStatus = EsasOperationResultStatus.OperationSuccesful;
                loadResult.LoadTimeMs = sp.ElapsedMilliseconds;
                loadResult.Message = $"{loadedObjects.Count()} objects loaded";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                loadResult.EsasLoadStatus = EsasOperationResultStatus.OperationFailed;
                loadResult.Message = $"Exception: {ex.Message}";
            }

            loadResult.LoadEndTimeUTC = DateTime.UtcNow;
            return (loadResult, loadedObjects);
        }
    }
}

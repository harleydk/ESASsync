using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Synchronization.ESAS.DAL.Models;


namespace Synchronization.ESAS.Synchronizations.EntityLoaderStrategies
{
    /// <summary>
    /// Implements a basic load strategy, with a results-delivery pipeline and a logger
    /// </summary>
    public abstract class BaseLoadStrategy : IEsasEntitiesLoaderStrategy
    {
        protected readonly Default.Container _esasContainer;
        protected readonly ILoadTimeStrategy _loadTimeStrategy;
        private readonly ILogger _logger;

        public BaseLoadStrategy(Default.Container esasContainer, ILoadTimeStrategy loadTimeStrategy, ILogger logger)
        {
            _esasContainer = esasContainer;
            _logger = logger;
            _loadTimeStrategy = loadTimeStrategy;
        }

        public (EsasLoadResult esasLoadResult, object[] loadedObjects) Load(int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            EsasLoadResult loadResult = new EsasLoadResult();
            loadResult.LoaderStrategyName = this.GetType().Name;
            loadResult.LoadStartTime = DateTime.Now;
            DateTime deltaTimeLoadvalue = _loadTimeStrategy.GetLoadTimeCutoff(this);
            loadResult.ModifiedOnDateTimeValue = deltaTimeLoadvalue;
            
            object[] loadedObjects = null;
            try
            {
                Stopwatch sp = new Stopwatch();
                sp.Start();
                loadedObjects = LoadObjects(deltaTimeLoadvalue, indexToStartLoadFrom, howManyRecordsToGet);
                sp.Stop();

                loadResult.EsasLoadStatus = EsasOperationResultStatus.OperationSuccesful;
                loadResult.LoadTimeMs = sp.ElapsedMilliseconds;
                loadResult.NumberOfObjectsLoaded = loadedObjects.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                loadResult.EsasLoadStatus = EsasOperationResultStatus.OperationFailed;
                loadResult.Message = $"Exception: {ex.Message}";
            }

            loadResult.LoadEndTime = DateTime.Now;
            return (loadResult, loadedObjects);
        }

        protected abstract object[] LoadObjects(DateTime loadTimeCutoff, int indexToStartLoadFrom, int howManyRecordsToGet);
    }

} 
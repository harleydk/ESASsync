using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Synchronization.ESAS.DAL.Models;

namespace Synchronization.ESAS.Synchronizations.EntityLoaderStrategies
{
    /// <summary>
    /// Implements an abstract basic load strategy, with a results-delivery pipeline and a logger.
    /// </summary>
    public abstract class BaseLoadStrategy : IEsasEntitiesLoaderStrategy
    {
        protected readonly EsasWsContextFactory _esasWebServiceContextFactory;
        protected readonly ILoadTimeStrategy _loadTimeStrategy;
        private readonly ILogger _logger;

        public BaseLoadStrategy(EsasWsContextFactory esasWebServiceContextFactory, ILoadTimeStrategy loadTimeStrategy, ILogger logger)
        {
            _esasWebServiceContextFactory = esasWebServiceContextFactory;
            _logger = logger;
            _loadTimeStrategy = loadTimeStrategy;
        }

        public (EsasLoadResult esasLoadResult, object[] loadedObjects) Load()
        {
            EsasLoadResult loadResult = new EsasLoadResult();
            loadResult.LoaderStrategyName = this.GetType().Name;
            loadResult.LoadStartTimeUTC = DateTime.UtcNow;

            object[] loadedObjects = null;
            try
            {
                var esasContext = _esasWebServiceContextFactory.Create();

                Stopwatch sp = new Stopwatch();
                sp.Start();

                DateTime deltaTimeLoadvalue = _loadTimeStrategy.GetLoadTimeCutoff(this);
                _logger.LogInformation($"Set a load-cutoff time of {deltaTimeLoadvalue.ToString("O")}");
                loadedObjects = LoadObjects(deltaTimeLoadvalue);
                loadResult.ModifiedOnDateTimeUTC = deltaTimeLoadvalue;

                sp.Stop();

                loadResult.EsasLoadStatus = EsasOperationResultStatus.OperationSuccesful;
                loadResult.LoadTimeMs = sp.ElapsedMilliseconds;
                if (loadedObjects != null)
                    loadResult.NumberOfObjectsLoaded = loadedObjects.Count();

                esasContext = null; // mark as ready for disposal
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

        protected abstract object[] LoadObjects(DateTime loadTimeCutoff);

        /// <summary>
        /// Used only for bulk-data collection, when data must be initially read from the source. 
        /// Use for periodic backups of ESAS, perhaps?
        /// </summary>
        protected virtual object[] LoadObjectsAndPersistToDisk()
        {
            return new object[0];
        }
    }

}
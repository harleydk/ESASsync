using System;
using System.Diagnostics;
using System.Linq;
using Synchronization.ESAS.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Synchronization.ESAS.Synchronizations.EntityLoaderStrategies
{
    /// <summary>
    /// Specific loader-strategy for 'AnsoegningPlanlaegningsUddannelseselement' entiteten - denne har ikke et 'modified timestamp'
    /// som de øvrige entiteter.
    /// </summary>
    public class AnsoegningPlanlaegningsUddannelseselementLoadStrategy : IEsasEntitiesLoaderStrategy
    {
        private readonly Default.Container _esasContainer;
        private readonly ILogger _logger;

        public AnsoegningPlanlaegningsUddannelseselementLoadStrategy(Default.Container esasContainer, ILogger logger)
        {
            _esasContainer = esasContainer;
            _logger = logger;
        }

        public (EsasLoadResult esasLoadResult, object[] loadedObjects) Load(int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            EsasLoadResult loadResult = new EsasLoadResult();
            loadResult.LoaderStrategyName = this.GetType().Name;
            loadResult.LoadStartTime = DateTime.Now;
            DateTime deltaTimeLoadvalue = new DateTime(1963, 11, 22); // J.F.K. RIP
            loadResult.ModifiedOnDateTimeValue = deltaTimeLoadvalue;

            object[] loadedObjects = null;
            try
            {
                Stopwatch sp = new Stopwatch();
                sp.Start();
                loadedObjects = _esasContainer.AnsoegningPlanlaegningsUddannelseselement.ToArray();
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

            loadResult.LoadEndTime = DateTime.Now;
            return (loadResult, loadedObjects);
        }
    }
}

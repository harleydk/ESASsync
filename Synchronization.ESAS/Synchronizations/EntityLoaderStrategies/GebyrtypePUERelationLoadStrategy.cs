using System;
using Synchronization.ESAS.DAL.Models;
using Microsoft.Extensions.Logging;

namespace Synchronization.ESAS.Synchronizations.EntityLoaderStrategies
{
    /// <summary>
    /// Specific loader-strategy for 'GebyrtypePUERelation' entiteten - denne har ikke et 'modified timestamp'
    /// som de øvrige entiteter.
    /// </summary>
    public class GebyrtypePUERelationLoadStrategy : IEsasEntitiesLoaderStrategy
    {
        private readonly Default.Container _esasContainer;
        private readonly ILogger _logger;

        public GebyrtypePUERelationLoadStrategy(Default.Container esasContainer, ILogger logger)
        {
            _esasContainer = esasContainer;
            _logger = logger;
        }

        public (EsasLoadResult esasLoadResult, object[] loadedObjects) Load(int indexToStartLoadFrom, int howManyRecordsToGet)
        {
            throw new NotImplementedException("Afventer impl.");
        }
    }
}

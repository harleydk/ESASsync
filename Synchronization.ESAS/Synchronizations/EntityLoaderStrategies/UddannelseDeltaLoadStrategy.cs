using Default;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KP.Synchronization.ESAS.Synchronizations.EntityLoaderStrategies
{

    public class PostnummerDeltaLoadStrategy : IEsasEntitiesLoaderStrategy
    {
        private readonly Container _esasContainer;

        public PostnummerDeltaLoadStrategy(Default.Container esasContainer)
        {
            this._esasContainer = esasContainer;
        }
        public IEnumerable<object> Load()
        {
            var modifiedObjects = _esasContainer.Postnummer.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }

    public class LandDeltaLoadStrategy : IEsasEntitiesLoaderStrategy
    {
        private readonly Container _esasContainer;

        public LandDeltaLoadStrategy(Default.Container esasContainer)
        {
            this._esasContainer = esasContainer;
        }
        public IEnumerable<object> Load()
        {
            var modifiedObjects = _esasContainer.Land.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }
    }


    public class UddannelseDeltaLoadStrategy : IEsasEntitiesLoaderStrategy
    {
        private readonly Container _esasContainer;

        public UddannelseDeltaLoadStrategy(Default.Container esasContainer)
        {
            this._esasContainer = esasContainer;
        }
        public IEnumerable<object> Load()
        {
            var modifiedObjects = _esasContainer.Uddannelse.Where(t => t.ModifiedOn <= DateTime.Now.AddHours(-1));
            return modifiedObjects;
        }

    }
}


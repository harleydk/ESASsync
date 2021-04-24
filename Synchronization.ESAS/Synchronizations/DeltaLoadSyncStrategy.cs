using Default;
using esas.Websites.Models.Models;
using KP.Synchronization.ESAS.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KP.Synchronization.ESAS.Synchronizations
{
    //public interface IEsasSyncCompositeStrategy
    //{

    //}

    //public class EsasSyncStrategyComposite : BaseSyncStrategy, IEsasSyncStrategy, IEsasSyncCompositeStrategy
    //{
    //    public EsasSyncStrategyComposite(Container esasODataContainer) : base(esasODataContainer)
    //    {
    //    }

    //    public override SyncResult Sync()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}


    /// <summary>
    /// A delivery-mechanism is something that knows how to take the loaded entites from an IEsasEntitiesLoaderStrategy 
    /// and send it somewhere - to rabbitMq, to a database, what have you.
    /// </summary>
    public interface IEsasDeliveryMechanism
    {
        void Deliver(object o);
    }


    public class EsasSyncStrategy : IEsasSyncStrategy
    {
        private readonly IEsasSyncStrategy _nestedEsasSyncStrategy;
        private readonly IEsasEntitiesLoaderStrategy _esasEntitiesLoaderStrategy;
        private readonly IEsasDeliveryMechanism _deliveryMechanism;

        public EsasSyncStrategy(IEsasEntitiesLoaderStrategy esasEntitiesLoaderStrategy, IEsasDeliveryMechanism deliveryMechanism)
        {
            _esasEntitiesLoaderStrategy = esasEntitiesLoaderStrategy;
            _deliveryMechanism = deliveryMechanism;
        }


        public EsasSyncStrategy(IEsasEntitiesLoaderStrategy esasEntitiesLoaderStrategy, IEsasDeliveryMechanism deliveryMechanism, IEsasSyncStrategy nestedEsasSyncStrategy)
        {
            _esasEntitiesLoaderStrategy = esasEntitiesLoaderStrategy;
            _deliveryMechanism = deliveryMechanism;
            _nestedEsasSyncStrategy = nestedEsasSyncStrategy;
        }


        public void PerformSync()
        {
            // grab modified objects. send to rabbit-mq.
            var loadedObjects = _esasEntitiesLoaderStrategy.Load();
            foreach (var loadedObject in loadedObjects)
                _deliveryMechanism.Deliver(loadedObject);

            SyncResult syncResult = null;
            // throw errors on fails. sync-result is only for metrics.

            // TODO: impl.
            if (syncResult.SyncResultStatus == SyncResultStatus.Succes && _nestedEsasSyncStrategy != null)
                _nestedEsasSyncStrategy.PerformSync();
        }

    }


    //public class EsasEntitiesLoader
    //{
    //    private IEnumerable<IEsasEntitiesLoaderStrategy> entityLoaderStrategies;

    //    public EsasEntitiesLoader()
    //    {
    //    }
    //}

    public enum EsasLoadStrategyType
    {
        DeltaLoad,
        FullLoad
    }


    public interface IEsasEntitiesLoaderStrategy
    {
        /// <summary>
        /// Load objects from the datasource
        /// </summary>
        /// <returns></returns>
        IEnumerable<object> Load();
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


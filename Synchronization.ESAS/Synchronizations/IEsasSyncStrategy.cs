namespace Synchronization.ESAS.Synchronizations
{
    public interface IEsasSyncStrategy
    {
        IEsasEntitiesLoaderStrategy EsasEntitiesLoaderStrategy { get; }

        void ExecuteSyncStrategy();
    }
}


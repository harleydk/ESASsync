using Synchronization.ESAS.DAL.Models;

namespace Synchronization.ESAS.Synchronizations
{
    public interface IEsasEntitiesLoaderStrategy
    {
        /// <summary>
        /// Load objects from the datasource
        /// </summary>
        /// <returns></returns>
        (EsasLoadResult esasLoadResult, object[] loadedObjects) Load(int indexToStartLoadFrom, int howManyRecordsToGet);
    }
}


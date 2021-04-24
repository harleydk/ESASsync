using Synchronization.ESAS.DAL.Models;

namespace Synchronization.ESAS.Synchronizations.EntityLoaderStrategies
{
    public class NullObjectLoadResultDestination : ISyncResultsDestination
    {
        public void PrepareSyncResult(EsasSyncResult esasSyncResult)
        {
            // do nothing
        }

        public void UpdateResult(EsasSyncResult esasSyncResult)
        {
            // do nothing
        }
    }
}
using Synchronization.ESAS.DAL.Models;

namespace Synchronization.ESAS
{
    public interface ISyncResultsDestination
    {
        void PrepareSyncResult(EsasSyncResult esasSyncResult);
        void UpdateResult(EsasSyncResult esasSyncResult);
    }
} 
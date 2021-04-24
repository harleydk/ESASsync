using Synchronization.ESAS.DAL.Models;

namespace Synchronization.ESAS.SyncDestinations
{
    /// <summary>
    /// Null-object sync-destination, for unit test-purposes.
    /// </summary>
    public class NullObjectSyncDestination : IEsasSyncDestination
    {
        public EsasSendResult Deliver(object[] objects)
        {
            // do nothing spectacular
            return new EsasSendResult();
        }
    }

}

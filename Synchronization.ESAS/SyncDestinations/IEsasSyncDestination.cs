using Synchronization.ESAS.DAL.Models;

namespace Synchronization.ESAS.SyncDestinations
{
    /// <summary>
    /// A delivery-mechanism is something that knows how to take the loaded entites from an IEsasEntitiesLoaderStrategy 
    /// and send it somewhere - to rabbitMq, to a database, what have you.
    /// </summary>
    public interface IEsasSyncDestination
    {
        EsasSendResult Deliver(object[] objects);
    }


}


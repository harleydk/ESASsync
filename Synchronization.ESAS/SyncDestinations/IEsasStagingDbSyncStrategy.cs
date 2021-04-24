using System;
using Synchronization.ESAS.DAL;

namespace Synchronization.ESAS.SyncDestinations
{
    public interface IEsasStagingDbDestination
    {
        void Deliver(object[] objects, IEsasDbContextFactory dbContextFactory);
        bool IsStrategyMatch(Type type);
    }
}
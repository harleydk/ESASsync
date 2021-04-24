using Synchronization.ESAS.Synchronizations;
using System;

namespace Synchronization.ESAS
{
    /// <summary>
    /// En full-load synkronisering returnerer et tudse-gammelt timestamp, så vi kan være sikker på at få alle data med.
    /// </summary>
    public class FullLoadCutoffStrategy : ILoadTimeStrategy
    {
        public DateTime GetLoadTimeCutoff(IEsasEntitiesLoaderStrategy esasEntitiesLoaderStrategy)
        {
            return new DateTime(1963, 11, 22); // J.F.K. RIP
        }
    }
}

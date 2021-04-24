using Synchronization.ESAS.Synchronizations;
using System;

namespace Synchronization.ESAS
{
    /// <summary>
    /// Hvordan ved fra hvornår - tidsstempel - vi skal hente data fra web-sericen fra (qua 'ModifiedOn' time-stamps)? Ved at hente en DateTime-værdi til sammenligning;
    /// og en klasse som implementerer dette interface, ved hvorfra den DateTime skal hentes.
    /// </summary>
    public interface ILoadTimeStrategy
    {
        DateTime GetLoadTimeCutoff(IEsasEntitiesLoaderStrategy esasEntitiesLoaderStrategy);
    }
}

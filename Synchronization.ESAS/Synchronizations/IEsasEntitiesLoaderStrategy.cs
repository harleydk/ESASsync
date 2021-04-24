using esas.Dynamics.Models.Contracts;
using Microsoft.Extensions.Logging;
using Synchronization.ESAS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Synchronization.ESAS.Synchronizations
{
    public interface IEsasEntitiesLoaderStrategy
    {
        /// <summary>
        /// Load objects from the datasource
        /// </summary>
        /// <returns></returns>
        (EsasLoadResult esasLoadResult, object[] loadedObjects) Load();
    }

}


using Synchronization.ESAS.DAL;
using Synchronization.ESAS.Synchronizations;
using System;
using System.Linq;

namespace Synchronization.ESAS
{
    /// <summary>
    ///  Henter info om seneste _succesfulde_ load af data, så vi ved fra hvilket tidspunkt vi skal hente ændrede data.
    /// </summary>
    public class LatestSuccesfulLoadStrategy : ILoadTimeStrategy
    {
        private readonly IEsasDbContextFactory esasDbContextFactory;

        public LatestSuccesfulLoadStrategy(IEsasDbContextFactory esasDbContextFactory)
        {
            this.esasDbContextFactory = esasDbContextFactory;
        }

        public DateTime GetLoadTimeCutoff(IEsasEntitiesLoaderStrategy esasEntitiesLoaderStrategy)
        {
            string loaderStrategyName = esasEntitiesLoaderStrategy.GetType().Name;
            var dbContext = esasDbContextFactory.CreateDbContext();
            using (dbContext)
            {
                var latestSuccesfulLoadTime = dbContext.EsasSyncResults
                    .OrderByDescending(ls => ls.SyncStartTimeUTC)
                    .FirstOrDefault(ls => ls.esasLoadResult.LoaderStrategyName == loaderStrategyName && ls.esasLoadResult.EsasLoadStatus == DAL.Models.EsasOperationResultStatus.OperationSuccesful
                    && ls.esasSendResult.SendToDestinationStatus == DAL.Models.EsasOperationResultStatus.OperationSuccesful
                    );

                if (latestSuccesfulLoadTime != null)
                    return latestSuccesfulLoadTime.SyncStartTimeUTC;
                else
                {
                    // there's not a latest load-time available to us, so let's go 10 minutes back in time
                    return DateTime.UtcNow.AddMinutes(-10);
                }

            }
        }
    }
}

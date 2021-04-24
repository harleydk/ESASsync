using Synchronization.ESAS.DAL;
using Synchronization.ESAS.DAL.Models;
using Synchronization.ESAS.Synchronizations;
using Synchronization.ESAS.Synchronizations.EntityLoaderStrategies;

namespace Synchronization.ESAS
{
    internal class EsasStagingDbLoadResultDestination : ISyncResultsDestination
    {
        private IEsasDbContextFactory _esasStagingDbContextFactory;

        public EsasStagingDbLoadResultDestination(IEsasDbContextFactory esasStagingDbContextFactory)
        {
            _esasStagingDbContextFactory = esasStagingDbContextFactory;
        }

        public void PrepareSyncResult(EsasSyncResult esasSyncResult)
        {
            var dbContext = _esasStagingDbContextFactory.CreateDbContext();
            using (dbContext)
            {
                dbContext.EsasSyncResults.Add(esasSyncResult);
                dbContext.SaveChanges();
            }
        }

        public void UpdateResult(EsasSyncResult esasSyncResult)
        {
            var dbContext = _esasStagingDbContextFactory.CreateDbContext();
            using (dbContext)
            {
                var existingSyncResult = dbContext.EsasSyncResults.Find(esasSyncResult.Id);
                dbContext.Entry(existingSyncResult).CurrentValues.SetValues(esasSyncResult);
                dbContext.SaveChanges();
            }
        }

    }
}
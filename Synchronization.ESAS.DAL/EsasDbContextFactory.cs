namespace Synchronization.ESAS.DAL
{
    public class EsasDbContextFactory : IEsasDbContextFactory
    {
        public EsasStagingDbContext CreateDbContext()
        {
            EsasStagingDbContext dbContext = new EsasStagingDbContext();
            return dbContext;
        }

    }



}

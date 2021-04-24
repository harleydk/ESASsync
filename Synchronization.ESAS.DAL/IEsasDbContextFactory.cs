namespace Synchronization.ESAS.DAL
{
    public interface IEsasDbContextFactory
    {
        EsasStagingDbContext CreateDbContext();
    }
}
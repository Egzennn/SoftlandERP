using SoftlandERP.Data.Entities.Vocabularies.Forms.Ogolne;

namespace SoftlandERP.Core.Repositories.Interfaces.Ogolne
{
    public interface IRecepturyNormWykonawczychRepository : IRepository<OgolneRecepturyNormWykonawczych>
    {
        Task<IEnumerable<string?>?> FindNRByRPAsync(string? rp);

        Task<TimeSpan?> GetTimeByNR(string? nr);

        Task<decimal?> GetKwotaByNR(string? nr);
    }
}

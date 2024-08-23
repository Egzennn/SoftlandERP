using SoftlandERP.Data.Entities.Vocabularies.Forms.Spedycja;
using static SoftlandERP.Core.Repositories.Spedycja.CzynnosciMagazynowoSpedycyjneLogRepository;

namespace SoftlandERP.Core.Repositories.Interfaces.Spedycja
{
    public interface ICzynnosciMagazynowoSpedycyjneLogRepository
    {
        Task<IEnumerable<SpedycjaCzynnosciMagazynowoSpedycyjneLog?>?> GetByColumnAsync(string? columnValue);

        Task<CzynnosciLog?> FindOpisByAkronimLog(string? akronim);
    }
}

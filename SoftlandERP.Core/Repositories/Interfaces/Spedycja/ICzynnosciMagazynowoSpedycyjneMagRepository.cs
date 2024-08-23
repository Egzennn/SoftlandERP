using SoftlandERP.Data.Entities.Vocabularies.Forms.Spedycja;
using static SoftlandERP.Core.Repositories.Spedycja.CzynnosciMagazynowoSpedycyjneMagRepository;

namespace SoftlandERP.Core.Repositories.Interfaces.Spedycja
{
    public interface ICzynnosciMagazynowoSpedycyjneMagRepository
    {
        Task<IEnumerable<SpedycjaCzynnosciMagazynowoSpedycyjneMag?>?> GetByColumnAsync(string? columnValue);

        Task<CzynnosciMag?> FindOpisByAkronimMag(string? akronim);
    }
}

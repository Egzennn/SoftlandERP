using SoftlandERP.Data.Entities.Views;
using static SoftlandERP.Core.Repositories.Spedycja.SekcjaTowarRepository;

namespace SoftlandERP.Core.Repositories.Interfaces.Spedycja
{
    public interface ISekcjaTowarRepository : IRepository<SpedycjaKonfekcjaSekcje>
    {
        Task<IEnumerable<string?>?> FindTowarBySekcjeAsync(string? sekcja);

        Task<IloscSummary?> FindIloscByTowarAsync(string? towar);
    }
}
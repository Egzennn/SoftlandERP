using SoftlandERP.Data.Entities.Vocabularies.Forms.Spedycja;
using static SoftlandERP.Core.Repositories.Spedycja.StandardyPrzygotowaniaRepository;

namespace SoftlandERP.Core.Repositories.Interfaces.Spedycja
{
    public interface IStandardyPrzygotowaniaRepository : IRepository<SpedycjaStandardyPrzygotowania>
    {
        Task<IEnumerable<SpedycjaStandardyPrzygotowania?>?> GetByColumnAsync(string? columnValue);

        Task<MagOpis?> FindOpisByAkronim(string? akronim);

        Task<TimeSpan?> GetTimeByAkronimLog(string? akronim);
    }
}

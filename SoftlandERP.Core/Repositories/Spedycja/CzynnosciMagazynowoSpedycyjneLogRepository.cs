using Microsoft.EntityFrameworkCore;
using SoftlandERP.Core.Repositories.Interfaces.Spedycja;
using SoftlandERP.Data.DB;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Spedycja;

namespace SoftlandERP.Core.Repositories.Spedycja
{
    public class CzynnosciMagazynowoSpedycyjneLogRepository : Repository<SpedycjaCzynnosciMagazynowoSpedycyjneLog>, ICzynnosciMagazynowoSpedycyjneLogRepository
    {
        public CzynnosciMagazynowoSpedycyjneLogRepository(MainContext mainContext)
            : base(mainContext)
        {
        }

        public async Task<IEnumerable<SpedycjaCzynnosciMagazynowoSpedycyjneLog?>?> GetByColumnAsync(string? columnValue)
        {
            try
            {
                return await this.mainContext.SpedycjaCzynnosciMagazynowoSpedycyjneLogVocabulary.Where(x => (x.Akronim ?? string.Empty) == (columnValue ?? string.Empty)).ToListAsync().ConfigureAwait(true);
            }
            catch
            {
                throw;
            }
        }

        public async Task<CzynnosciLog?> FindOpisByAkronimLog(string? akronim)
        {
            try
            {
                var summary = await this.mainContext.SpedycjaCzynnosciMagazynowoSpedycyjneLogVocabulary
                    .Where(x => (x.Akronim ?? string.Empty) == (akronim ?? string.Empty))
                    .Select(x => new CzynnosciLog
                    {
                        Czynnosc = x.Czynnosc,
                        RodzajPrac = x.RodzajPrac,
                        Opis = x.Opis
                    })
                    .FirstOrDefaultAsync();

                return summary;
            }
            catch
            {
                throw;
            }
        }

        public class CzynnosciLog
        {
            public string? Czynnosc { get; set; }

            public string? RodzajPrac { get; set; }

            public string? Opis { get; set; }
        }
    }
}

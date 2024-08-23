using Microsoft.EntityFrameworkCore;
using SoftlandERP.Core.Repositories.Interfaces.Spedycja;
using SoftlandERP.Data.DB;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Spedycja;

namespace SoftlandERP.Core.Repositories.Spedycja
{
    public class CzynnosciMagazynowoSpedycyjneMagRepository : Repository<SpedycjaCzynnosciMagazynowoSpedycyjneMag>, ICzynnosciMagazynowoSpedycyjneMagRepository
    {
        public CzynnosciMagazynowoSpedycyjneMagRepository(MainContext mainContext)
            : base(mainContext)
        {
        }

        public async Task<IEnumerable<SpedycjaCzynnosciMagazynowoSpedycyjneMag?>?> GetByColumnAsync(string? columnValue)
        {
            try
            {
                return await this.mainContext.SpedycjaCzynnosciMagazynowoSpedycyjneMagVocabulary.Where(x => (x.Akronim ?? string.Empty) == (columnValue ?? string.Empty)).ToListAsync().ConfigureAwait(true);
            }
            catch
            {
                throw;
            }
        }

        public async Task<CzynnosciMag?> FindOpisByAkronimMag(string? akronim)
        {
            try
            {
                var summary = await this.mainContext.SpedycjaCzynnosciMagazynowoSpedycyjneMagVocabulary
                    .Where(x => (x.Akronim ?? string.Empty) == (akronim ?? string.Empty))
                    .Select(x => new CzynnosciMag
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

        public class CzynnosciMag
        {
            public string? Czynnosc { get; set; }

            public string? RodzajPrac { get; set; }

            public string? Opis { get; set; }
        }
    }
}

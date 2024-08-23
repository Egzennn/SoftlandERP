using Microsoft.EntityFrameworkCore;
using SoftlandERP.Core.Repositories.Interfaces.Spedycja;
using SoftlandERP.Data.DB;
using SoftlandERP.Data.Entities.Views;

namespace SoftlandERP.Core.Repositories.Spedycja
{
    public class SekcjaTowarRepository : Repository<SpedycjaKonfekcjaSekcje>, ISekcjaTowarRepository
    {
        public SekcjaTowarRepository(MainContext mainContext)
            : base(mainContext)
        {
        }

        public async Task<IEnumerable<string?>?> FindTowarBySekcjeAsync(string? sekcja)
        {
            try
            {
                return await this.mainContext.SpedycjaKonfekcjaSekcje.Where(x => (x.Sekcja ?? string.Empty) == (sekcja ?? string.Empty)).Select(x => x.Twr_Kod).Distinct().ToListAsync().ConfigureAwait(true);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IloscSummary?> FindIloscByTowarAsync(string? towar)
        {
            try
            {
                var summary = await this.mainContext.SpedycjaKonfekcjaSekcje
                    .Where(x => (x.Twr_Kod ?? string.Empty) == (towar ?? string.Empty))
                    .Select(x => new IloscSummary
                    {
                        IloscMagazynowa = x.IloscMagazynowa,
                        Suma_Sprzedazy = x.Suma_Sprzedazy,
                        Min_Ilosc = x.Min_Ilosc,
                        Max_Ilosc = x.Max_Ilosc
                    })
                    .FirstOrDefaultAsync();

                return summary;
            }
            catch
            {
                throw;
            }
        }

        public class IloscSummary
        {
            public int? IloscMagazynowa { get; set; }

            public int? Suma_Sprzedazy { get; set; }

            public int? Min_Ilosc { get; set; }

            public int? Max_Ilosc { get; set; }
        }
    }
}
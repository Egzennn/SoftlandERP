using Microsoft.EntityFrameworkCore;
using SoftlandERP.Core.Repositories.Interfaces.Ogolne;
using SoftlandERP.Data.DB;
using SoftlandERP.Data.Entities.Forms;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Ogolne;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Spedycja;

namespace SoftlandERP.Core.Repositories.Ogolne
{
    public class RecepturyNormWykonawczychRepository : Repository<OgolneRecepturyNormWykonawczych>, IRecepturyNormWykonawczychRepository
    {
        public RecepturyNormWykonawczychRepository(MainContext mainContext)
            : base(mainContext)
        {
        }

        public async Task<IEnumerable<string?>?> FindNRByRPAsync(string? rp)
        {
            try
            {
                return await this.mainContext.OgolneRecepturyNormWykonawczychVocabulary.Where(x => (x.RodzajPracy ?? string.Empty) == (rp ?? string.Empty)).Select(x => x.NazwaReceptury).Distinct().ToListAsync().ConfigureAwait(true);
            }
            catch
            {
                throw;
            }
        }

        public async Task<TimeSpan?> GetTimeByNR(string? nr)
        {
            try
            {
                var timeSpans = await this.mainContext.OgolneRecepturyNormWykonawczychVocabulary
                    .Where(x => x.NazwaReceptury == nr)
                    .Select(x => x.Czas)
                    .ToListAsync()
                    .ConfigureAwait(true);

                TimeSpan totalTime = TimeSpan.Zero;
                foreach (var timeSpan in timeSpans)
                {
                    totalTime += timeSpan;
                }

                return totalTime;
            }
            catch
            {
                throw;
            }
        }

        public async Task<decimal?> GetKwotaByNR(string? nr)
        {
            try
            {
                var kwota = await this.mainContext.OgolneRecepturyNormWykonawczychVocabulary
                    .Where(x => x.NazwaReceptury == nr)
                    .Select(x => x.Kwota)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                return kwota;
            }
            catch
            {
                throw;
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SoftlandERP.Core.Repositories.Interfaces.Spedycja;
using SoftlandERP.Data.DB;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Spedycja;

namespace SoftlandERP.Core.Repositories.Spedycja
{
    public class StandardyPrzygotowaniaRepository : Repository<SpedycjaStandardyPrzygotowania>, IStandardyPrzygotowaniaRepository
    {
        public StandardyPrzygotowaniaRepository(MainContext mainContext)
            : base(mainContext)
        {
        }

        public async Task<IEnumerable<SpedycjaStandardyPrzygotowania?>?> GetByColumnAsync(string? columnValue)
        {
            try
            {
                return await this.mainContext.SpedycjaStandardyPrzygotowaniaVocabulary.Where(x => (x.AkronimLog ?? string.Empty) == (columnValue ?? string.Empty)).ToListAsync().ConfigureAwait(true);
            }
            catch
            {
                throw;
            }
        }

        public async Task<TimeSpan?> GetTimeByAkronimLog(string? akronim)
        {
            try
            {
                var timeSpans = await this.mainContext.SpedycjaStandardyPrzygotowaniaVocabulary
                    .Where(x => x.AkronimLog == akronim)
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

        public async Task<MagOpis?> FindOpisByAkronim(string? akronim)
        {
            try
            {
                var summary = await this.mainContext.SpedycjaCzynnosciMagazynowoSpedycyjneMagVocabulary
                    .Where(x => (x.Akronim ?? string.Empty) == (akronim ?? string.Empty))
                    .Select(x => new MagOpis
                    {
                        Akronim = x.Akronim,
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

        public class MagOpis
        {
            public string? Akronim { get; set; }

            public string? Czynnosc { get; set; }

            public string? RodzajPrac { get; set; }

            public string? Opis { get; set; }
        }
    }
}

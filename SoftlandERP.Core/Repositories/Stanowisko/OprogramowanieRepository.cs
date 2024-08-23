using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftlandERP.Core.Repositories.Interfaces.Stanowisko;
using SoftlandERP.Data.DB;
using SoftlandERP.Data.Entities.Forms.Stanowisko;

namespace SoftlandERP.Core.Repositories.Stanowisko
{
    public class OprogramowanieRepository : Repository<StanowiskoOprogramowanieForm>, IOprogramowanieRepository
    {
        public OprogramowanieRepository(MainContext mainContext)
            : base(mainContext)
        {
        }

        public async Task<IEnumerable<string?>?> FindNazwaByGrupaAsync(string? grupa)
        {
            try
            {
                return await this.mainContext.StanowiskoGrupaNazwaOprogramowaniaVocabulary.Where(x => (x.Typ ?? string.Empty) == (grupa ?? string.Empty)).Select(x => x.Wartosc).Distinct().ToListAsync().ConfigureAwait(true);
            }
            catch
            {
                throw;
            }
        }
    }
}

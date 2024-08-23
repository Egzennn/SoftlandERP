using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftlandERP.Core.Repositories.Interfaces.Stanowisko
{
    public interface IOprogramowanieRepository
    {
        Task<IEnumerable<string?>?> FindNazwaByGrupaAsync(string? grupa);
    }
}

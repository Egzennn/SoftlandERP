using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SoftlandERP.Data.Entities.Views
{
    [Table("PraceZleconeDokumenty")]
    [Keyless]
    public class PraceZleconeDokumenty
    {
        [DisplayName("Dokument")]
        public string? Dokument { get; set; }

        [DisplayName("Towar")]
        public string? Kod { get; set; }

        [DisplayName("Ilosc")]
        public decimal Ilosc { get; set; }
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SoftlandERP.Data.Entities.Views
{
    [Table("SpedycjaKonfekcjaSekcje")]
    [Keyless]
    public class SpedycjaKonfekcjaSekcje
    {
        [DisplayName("Odpowiedzialny")]
        public string? Odpowiedzialny { get; set; }

        [DisplayName("Sekcja")]
        public string? Sekcja { get; set; }

        [DisplayName("Towar")]
        public string? Twr_Kod { get; set; }

        [DisplayName("Ilość magazynowa")]
        public int? IloscMagazynowa { get; set; }

        [DisplayName("Suma sprzedaży")]
        public int? Suma_Sprzedazy { get; set; }

        [DisplayName("Min ilość")]
        public int? Min_Ilosc { get; set; }

        [DisplayName("Max ilość")]
        public int? Max_Ilosc { get; set; }
    }
}

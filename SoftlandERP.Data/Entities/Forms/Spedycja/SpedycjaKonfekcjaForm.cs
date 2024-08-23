using System.ComponentModel;

namespace SoftlandERP.Data.Entities.Forms.Spedycja
{
    public class SpedycjaKonfekcjaForm : BaseEntity
    {
        [DisplayName("Grupa standardu zlecenia")]
        public string? AkronimLog { get; set; }

        [DisplayName("Sekcja")]
        public string? Sekcja { get; set; }

        [DisplayName("Towar")]
        public string? Towar { get; set; }

        [DisplayName("Ilość zlecenia konfekcji")]
        public int? IloscZlecenia { get; set; }

        [DisplayName("Ilość magazynowa")]
        public int? IloscMagazynowa { get; set; }

        [DisplayName("Suma sprzedaży")]
        public int? Suma_Sprzedazy { get; set; }

        [DisplayName("Min ilość")]
        public int? Min_Ilosc { get; set; }

        [DisplayName("Max ilość")]
        public int? Max_Ilosc { get; set; }

        [DisplayName("Akronim wykonawcy")]
        public string? AkronimWyk { get; set; }

        [DisplayName("Czas wykonania")]
        public TimeSpan Czas { get; set; }
    }
}

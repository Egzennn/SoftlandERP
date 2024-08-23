using System.ComponentModel;

namespace SoftlandERP.Data.Entities.Vocabularies.Forms.Spedycja
{
    public class SpedycjaCzynnosciMagazynowoSpedycyjneLog : BaseEntity
    {
        [DisplayName("Skrót standardu przygotowania dostawy")]
        public string? Akronim { get; set; }

        [DisplayName("Nazwa czynność")]
        public string? Czynnosc { get; set; }

        [DisplayName("Rodzaje prac")]
        public string? RodzajPrac { get; set; }

        [DisplayName("Opis")]
        public string? Opis { get; set; }

        [DisplayName("Odpowiedzialny")]
        public string? Odpowiedzialny { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SoftlandERP.Data.Entities.Forms.Stanowisko
{
    public class StanowiskoOdpowiedzialnosciForm : BaseEntity
    {
        [Display(Name = "Data rozpoczęcia")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DataRozpoczecia { get; set; }

        [Display(Name = "Data zakończenia")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DataZakonczenia { get; set; }

        [Display(Name = "Liczba interwałów")]
        public int LiczbaInterwalow { get; set; }

        [Display(Name = "Interwał")]
        public string Interwal { get; set; }

        [Display(Name = "Przesunięcie")]
        public int Przesuniecie { get; set; }

        [Display(Name = "Typ dni")]
        public string Dni { get; set; }

        [Display(Name = "Symbol")]
        public string? Symbol { get; set; }

        [Display(Name = "Klucz przydziału")]
        public string? KluczPrzydzialu { get; set; }

        [Display(Name = "Lokalizacja")]
        public string? Lokalizacja { get; set; }

        [Display(Name = "Dział")]
        public string? Dzial { get; set; }

        [Display(Name = "Akronim wykonawcy")]
        public string? AkronimWyk { get; set; }

        [Display(Name = "Cykliczność")]
        public string? Cyklicznosc { get; set; }
    }
}

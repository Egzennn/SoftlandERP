using System.ComponentModel.DataAnnotations;

namespace SoftlandERP.Data.Entities.Forms.Stanowisko
{
    public class StanowiskoListaWypForm : BaseEntity
    {
        [Display(Name = "Lokalizacja")]
        [Required(ErrorMessage = "Lokalizacja jest wymagana")]
        public string? Lokalizacja { get; set; }

        [Display(Name = "Pomieszczenie")]
        [Required(ErrorMessage = "Pomieszczenie jest wymagane")]
        public string? Pomieszczenie { get; set; }

        [Display(Name = "Rodzaj")]
        [Required(ErrorMessage = "Rodzaj jest wymagany")]
        public string? Rodzaj { get; set; }

        [Display(Name = "Priorytet")]
        [Required(ErrorMessage = "Priorytet jest wymagany")]
        public string? Priorytet { get; set; }

        [Display(Name = "IDI")]
        public string? IDI { get; set; }

        [Display(Name = "Grupa produktu")]
        public string? GrupaProduktu { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Kwota jest wymagane")]
        public string? Nazwa { get; set; }

        [Display(Name = "Jednostka miary")]
        public string? JednostkaMiary { get; set; }

        [Display(Name = "Ilość")]
        [Required(ErrorMessage = "Ilość jest wymagana")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Dozwolone tylko liczby")]
        public int? Ilosc { get; set; }
    }
}

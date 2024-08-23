using System.ComponentModel.DataAnnotations;

namespace SoftlandERP.Data.Entities.Forms.Stanowisko
{
    public class StanowiskoOprogramowanieForm : BaseEntity
    {
        [Display(Name = "Grupa")]
        public string? Grupa { get; set; }

        [Display(Name = "Nazwa")]
        public string? Nazwa { get; set; }

        [Display(Name = "Wersja")]
        public string? Wersja { get; set; }

        [Display(Name = "IDI")]
        [Required(ErrorMessage = "IDI jest wymagane")]
        public string? IDI { get; set; }

        [Display(Name = "Instalacja")]
        public string? Instalacja { get; set; }

        [Display(Name = "Rola")]
        public string? Rola { get; set; }

        [Display(Name = "Lokalizacja")]
        public string? Lokalizacja { get; set; }

        [Display(Name = "Opis")]
        public string? Opis { get; set; }
    }
}

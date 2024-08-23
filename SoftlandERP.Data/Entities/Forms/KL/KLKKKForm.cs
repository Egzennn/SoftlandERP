using System.ComponentModel.DataAnnotations;

namespace SoftlandERP.Data.Entities.Forms.KL
{
    public class KLKKKForm : BaseEntity
    {
        [Display(Name = "IDK")]
        public string? IDK { get; set; }

        [Display(Name = "Typ akcji")]
        public string? TypAkcji { get; set; }

        [Display(Name = "Rozmowca")]
        public string? Rozmowca { get; set; }

        [Display(Name = "Cel kontaktu")]
        public string? CelKontaktu { get; set; }

        [Display(Name = "Obszar")]
        public string? Obszar { get; set; }

        [Display(Name = "Brand")]
        public string? Brand { get; set; }

        [Display(Name = "Towar")]
        public string? Towar { get; set; }

        [Display(Name = "Opis")]
        public string? Opis { get; set; }

        [Display(Name = "Rekomendacja")]
        public string? Rekomendacja { get; set; }

        [Display(Name = "Waga")]
        public string? Waga { get; set; }

        [Display(Name = "Sprawy powiązane")]
        public string? SprawyPowiazane { get; set; }
    }
}

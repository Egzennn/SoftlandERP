using System.ComponentModel.DataAnnotations;

namespace SoftlandERP.Data.Entities.Forms.Stanowisko
{
    public class StanowiskoDOWForm : BaseEntity
    {
        [Display(Name = "Obiekt")]
        public string? Obiekt { get; set; }

        [Display(Name = "IDI")]
        [Required(ErrorMessage = "IDI jest wymagane")]
        public string? IDI { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public string? Nazwa { get; set; }

        [Display(Name = "Odpowiedzialny")]
        public string? Odpowiedzialny { get; set; }

        [Display(Name = "Zdarzenie")]
        [Required(ErrorMessage = "Zdarzenie jest wymagane")]
        public string? Zdarzenie { get; set; }

        [Display(Name = "Opis")]
        public string? Opis { get; set; }

        [Display(Name = "Termin")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Termin { get; set; }

        [Display(Name = "Obszar roboczy")]
        public string? ObszarRoboczy { get; set; }
    }
}

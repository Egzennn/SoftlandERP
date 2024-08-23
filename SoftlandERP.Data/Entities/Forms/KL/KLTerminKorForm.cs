using System.ComponentModel.DataAnnotations;

namespace SoftlandERP.Data.Entities.Forms.KL
{
    public class KLTerminKorForm : BaseEntity
    {
        [Display(Name = "IDK")]
        public string? IDK { get; set; }

        [Display(Name = "Dokument")]
        [Required(ErrorMessage = "Dokument jest wymagany")]
        public string? Dokument { get; set; }

        [Display(Name = "Kwota")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Dozwolone tylko liczby")]
        [Required(ErrorMessage = "Kwota jest wymagana")]
        public decimal? Kwota { get; set; }

        [Display(Name = "Waluta")]
        public string? Waluta { get; set; }

        [Display(Name = "Termin pierwotny")]
        [Required(ErrorMessage = "Termin pierwotny jest wymagany")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TerminPierwotny { get; set; }

        [Display(Name = "Termin skorygowany")]
        [Required(ErrorMessage = "Termin skorygowany jest wymagany")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TerminSkorygowany { get; set; }
    }
}

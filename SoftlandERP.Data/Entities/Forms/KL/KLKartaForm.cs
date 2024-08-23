using System.ComponentModel.DataAnnotations;

namespace SoftlandERP.Data.Entities.Forms.KL
{
    public class KLKartaForm : BaseEntity
    {
        [Display(Name = "IDK")]
        public string? IDK { get; set; }

        [Display(Name = "NIP")]
        [Required(ErrorMessage = "NIP jest wymagany")]
        public string? NIP { get; set; }

        [Display(Name = "Nazwa")]
        [RegularExpression("^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ-]+$", ErrorMessage = "Dozwolone tylko litery i znak \"-\"")]
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public string? Nazwa { get; set; }

        [Display(Name = "Rynek")]
        [Required(ErrorMessage = "Rynek jest wymagany")]
        public string? Rynek { get; set; }

        [Display(Name = "Grupa")]
        [Required(ErrorMessage = "Grupa jest wymagana")]
        public string? Grupa { get; set; }

        [Display(Name = "Kraj")]
        [Required(ErrorMessage = "Kraj jest wymagana")]
        public string? Kraj { get; set; }

        [Display(Name = "ABC")]
        [Required(ErrorMessage = "ABC jest wymagany")]
        [MaxLength(1)]
        public string? ABC { get; set; }

        [Display(Name = "Cykl obsługi")]
        [Required(ErrorMessage = "Cykl obsługi jest wymagany")]
        public int? CyklObslugi { get; set; }

        [Display(Name = "Opiekun")]
        [Required(ErrorMessage = "Opiekun jest wymagany")]
        public string? Opiekun { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Adres email jest niepoprawny")]
        public string? Email { get; set; }

        [Display(Name = "Uwagi")]
        public string? Uwagi { get; set; }
    }
}

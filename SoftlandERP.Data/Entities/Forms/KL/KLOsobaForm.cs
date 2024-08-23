using System.ComponentModel.DataAnnotations;

namespace SoftlandERP.Data.Entities.Forms.KL
{
    public class KLOsobaForm : BaseEntity
    {
        [Display(Name = "IDK")]
        public string? IDK { get; set; }

        [Display(Name = "Imię Nazwisko")]
        [RegularExpression("^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ-]+$", ErrorMessage = "Dozwolone tylko litery i znak \"-\"")]
        [Required(ErrorMessage = "Imię Nazwisko jest wymagane")]
        public string? ImieNazwisko { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Adres email jest niepoprawny")]
        public string? Email { get; set; }
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SoftlandERP.Data.Entities.Forms
{
    public class MPKForm : BaseEntity
    {
        [DisplayName("MPK")]
        public string MPKList { get; set; }

        [DisplayName("IDK")]
        public string IDK { get; set; }

        [DisplayName("Data wystawienia faktury")]
        public DateTime Data { get; set; } = DateTime.Now;

        [DisplayName("Rodzaj kosztu")]
        public string RodzajKosztu { get; set; }

        [DisplayName("IDS")]
        [Required(ErrorMessage = "IDS jest wymagany")]
        public string IDS { get; set; }

        [DisplayName("Nr dokumentu")]
        [Required(ErrorMessage = "Nr dokumentu jest wymagany")]
        [MaxLength(50)]
        public string NrDokumentu { get; set; }

        [DisplayName("Kwota netto")]
        [Required(ErrorMessage = "Kwota jest wymagana")]
        [RegularExpression(@"^[\d,]+$", ErrorMessage = "Dozwolone tylko liczby i przecinek")]
        public decimal Kwota { get; set; }

        [DisplayName("Waluta")]
        public string Waluta { get; set; }

        [DisplayName("Opis")]
        public string? Opis { get; set; }
    }
}
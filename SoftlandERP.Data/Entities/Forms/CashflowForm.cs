using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SoftlandERP.Data.Entities.Forms
{
    public class CashflowForm : BaseEntity
    {
        [DisplayName("Strona")]
        public string? Strona { get; set; }

        [DisplayName("Dokument")]
        public string? Dokument { get; set; }

        [DisplayName("DokumentObcy")]
        public string? DokumentObcy { get; set; }

        [DisplayName("Akronim")]
        public string? Akronim { get; set; }

        [DisplayName("Termin")]
        public DateTime Termin { get; set; } = DateTime.Now;

        [DisplayName("Kwota")]
        [RegularExpression(@"^[\d,]+$", ErrorMessage = "Dozwolone tylko liczby i przecinek")]
        public decimal Kwota { get; set; }

        [DisplayName("Pozostalo")]
        [RegularExpression(@"^[\d,]+$", ErrorMessage = "Dozwolone tylko liczby i przecinek")]
        public decimal Pozostalo { get; set; }

        [DisplayName("Waluta")]
        public string? Waluta { get; set; }

        [DisplayName("Priorytet")]
        public string? Priorytet { get; set; }

        [DisplayName("Uwagi")]
        public string? Uwagi { get; set; }
    }
}

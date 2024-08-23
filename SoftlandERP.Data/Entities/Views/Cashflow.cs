using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SoftlandERP.Data.Entities.Views
{
    [Table("Cashflow")]
    [Keyless]
    public class Cashflow
    {
        [Display(Name = "Strona")]
        public string Strona { get; set; }

        [Display(Name = "Dokument")]
        public string Dokument { get; set; }

        [Display(Name = "DokumentObcy")]
        public string DokumentObcy { get; set; }

        [Display(Name = "Akronim")]
        public string Akronim { get; set; }

        [Display(Name = "Termin")]
        public DateTime Termin { get; set; }

        [Display(Name = "Kwota")]
        public decimal Kwota { get; set; }

        [Display(Name = "Pozostalo")]
        public decimal Pozostalo { get; set; }

        [Display(Name = "Waluta")]
        public string Waluta { get; set; }
    }
}

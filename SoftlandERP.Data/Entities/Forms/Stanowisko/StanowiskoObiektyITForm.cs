using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftlandERP.Data.Entities.Forms.Stanowisko
{
    public class StanowiskoObiektyITForm : BaseEntity
    {
        [Display(Name = "Rola")]
        public string? Rola { get; set; }

        [Display(Name = "Obiekty")]
        public string? Obiekty { get; set; }

        [Display(Name = "Rodzaj obiektu")]
        public string? RodzajObiektu { get; set; }

        [Display(Name = "Grupa usług")]
        public string? GrupaUslug { get; set; }

        [Display(Name = "Nazwa usługi")]
        public string? NazwaUslugi { get; set; }

        [Display(Name = "Login")]
        public string? Login { get; set; }

        [Display(Name = "Opis")]
        public string? Opis { get; set; }
    }
}

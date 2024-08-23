using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftlandERP.Data.Entities.Forms.Stanowisko
{
    public class StanowiskoSprawyForm : BaseEntity
    {
        [Display(Name = "Rola")]
        public string? Rola { get; set; }

        [Display(Name = "IDS")]
        [Required(ErrorMessage = "IDS jest wymagany")]
        public string? IDS { get; set; }

        [Display(Name = "Odpowiedzialny")]
        [Required(ErrorMessage = "Odpowiedzialny jest wymagany")]
        public string? Odpowiedzialny { get; set; }

        [Display(Name = "Weryfikujący")]
        public string? Weryfikujacy { get; set; }

        [Display(Name = "Kategoria")]
        [Required(ErrorMessage = "Kategoria jest wymagana")]
        public string? Kategoria { get; set; }

        [Display(Name = "Temat")]
        [Required(ErrorMessage = "Temat jest wymagany")]
        public string? Temat { get; set; }

        [Display(Name = "Opis")]
        public string? Opis { get; set; }

        [Display(Name = "Data rozpoczęcia")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DataRozpoczecia { get; set; }

        [Display(Name = "Termin wykonania")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TerminWykonania { get; set; }

        [Display(Name = "Procent realizacji")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Dozwolone tylko liczby")]
        [Range(0, 100, ErrorMessage = "Liczba nie może przekraczać 100.")]
        [Required(ErrorMessage = "Realizacja jest wymagana")]
        public double? Realizacja { get; set; }

        [Display(Name = "Czas pracy")]
        public TimeSpan CzasPracy { get; set; }
    }
}

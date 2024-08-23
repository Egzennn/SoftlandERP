using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftlandERP.Data.Entities.Forms.Stanowisko
{
    public class StanowiskoOdbiorPrzesylekForm : BaseEntity
    {
        [Display(Name = "Data odbioru")]
        public DateTime? DataOdbioru { get; set; }

        [Display(Name = "Rodzaj przesyłki")]
        public string? RodzajPrzesylki { get; set; }

        [Display(Name = "Lokalizacja odbioru")]
        public string? LokalizacjaOdbioru { get; set; }

        [Display(Name = "Data nadania")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DataNadania { get; set; }

        [Display(Name = "IDK")]
        public string? IDK { get; set; }

        [Display(Name = "Uwagi")]
        public string? Uwagi { get; set; }

        [Display(Name = "IDS")]
        public string? IDS { get; set; }
    }
}

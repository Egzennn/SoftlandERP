using System.ComponentModel.DataAnnotations;

namespace SoftlandERP.Web.Areas.Administration.Models.Views.Organizacja
{
    public class StructuraViewModel
    {
        public List<Structura> StructuraList { get; set; }
    }

#pragma warning disable
    public class Structura
    {
        [Display(Name = " ")]
        public string First { get; set; }

        [Display(Name = "Departament")]
        public string Second { get; set; }

        [Display(Name = "Dział")]
        public string Third { get; set; }
    }
}
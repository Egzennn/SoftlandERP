using System.ComponentModel.DataAnnotations;

namespace SoftlandERP.Data.Entities.Forms.KL
{
    public class KLRozlForm : BaseEntity
    {
        [Display(Name = "IDK")]
        public string? IDK { get; set; }

        [Display(Name = "Ilość dni kredytu")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Dozwolone tylko liczby")]
        [Required(ErrorMessage = "ilosc dni kredyty jest wymagana")]
        public int? DniKredytowe { get; set; }

        [Display(Name = "Limit kredytowy")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Dozwolone tylko liczby")]
        [Required(ErrorMessage = "Kwota jest wymagane")]
        public int? LimitKredytowy { get; set; }

        [Display(Name = "Waluta")]
        public string? Waluta { get; set; }

        [Display(Name = "Blokada transakcji")]
        public string? BlokadaTransakcji { get; set; }

        [Display(Name = "Blokada zamówienia")]
        public string? BlokadaZamownia { get; set; }

        [Display(Name = "Data potwierdzenia")]
        public DateTime? DataPotwierdzenia { get; set; }
    }
}

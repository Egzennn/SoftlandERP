using System.ComponentModel.DataAnnotations;

namespace SoftlandERP.Data.Entities.Forms.KL
{
    public class KLXMLForm : BaseEntity
    {
        [Display(Name = "IDK")]
        public string? IDK { get; set; }

        [Display(Name = "Website")]
        [RegularExpression(@"^(https?://)?([a-zA-Z0-9\-]+\.)+[a-zA-Z]{2,}(\/\S*)?$", ErrorMessage = "Niepoprawny adres URL")]
        [Required(ErrorMessage = "Website jest wymagana")]
        public string? Website { get; set; }

        [Display(Name = "Opis")]
        public string? Opis { get; set; }
    }
}

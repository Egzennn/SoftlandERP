using System.ComponentModel.DataAnnotations;

namespace SoftlandERP.Data.Entities.Vocabularies.Staff
{
    public class UserAddress : BaseEntity
    {
        [Required]
        [Display(Name = "Kraj")]
        public string? Country { get; set; }

        public string? TwoLetterISOCountryName { get; set; }

        [Required]
        [Display(Name = "Miasto")]
        public string? City { get; set; }

        [Required]
        [Display(Name = "Street")]
        public string? Street { get; set; }

        public override string? ToString()
        {
            return string.Join(", ", new string[] { this.Country ?? string.Empty, this.City ?? string.Empty, this.Street ?? string.Empty }.Where(x => !string.IsNullOrEmpty(x)));
        }
    }
}
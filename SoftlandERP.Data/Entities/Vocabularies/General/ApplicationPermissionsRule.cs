using System.ComponentModel.DataAnnotations;

namespace SoftlandERP.Data.Entities.Vocabularies.General
{
    public class ApplicationPermissionsRule : BaseEntity
    {
        [Display(Name = "Nazwa modulu")]
        public string? Module { get; set; }

        [Display(Name = "Grupa Active Directory")]
        public string? ADGroupDisplayName { get; set; }

        [Display(Name = "Tylko przełożony")]
        public bool ManagerOnly { get; set; }
    }
}
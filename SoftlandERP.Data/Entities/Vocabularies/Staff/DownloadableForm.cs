using System.ComponentModel.DataAnnotations;

namespace SoftlandERP.Data.Entities.Vocabularies.Staff
{
    public class DownloadableForm : BaseEntity
    {
        [Required]
        [Display(Name = "Kategoria")]
        public string? Category { get; set; }

        [Required]
        [Display(Name = "Ścieżka")]
        public string? Path { get; set; }
    }
}
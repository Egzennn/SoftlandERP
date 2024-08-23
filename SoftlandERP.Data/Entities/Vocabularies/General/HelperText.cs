using System.ComponentModel.DataAnnotations;

namespace SoftlandERP.Data.Entities.Vocabularies.General
{
    public class HelperText : BaseEntity
    {
        [Display(Name = "Zmienna")]
        public string? Entity { get; set; }

        [Display(Name = "Nazwa właściwości")]
        public string? FieldName { get; set; }

        [Display(Name = "Nazwa wyświetlana właściwości")]
        public string? FieldDisplayName { get; set; }

        [Display(Name = "Text pomocniczy właściwości")]
        public string? FieldHelperText { get; set; }
    }
}
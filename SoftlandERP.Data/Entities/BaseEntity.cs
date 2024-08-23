using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SoftlandERP.Data.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            this.Id = Guid.NewGuid();
            this.Created = DateTime.Now;
        }

        [Key]
        [Display(Name = "ID")]
        public Guid Id { get; set; }

        [Display(Name = "Utworzono")]
        public DateTime Created { get; set; }

        [Display(Name = "Utworzono przez")]
        public string? CreatedBy { get; set; }

        [Display(Name = "Zmodyfikowano")]
        public DateTime? Updated { get; set; }

        [Display(Name = "Zmodyfikowano przez")]
        public string? UpdatedBy { get; set; }

        [DisplayName("Stan")]
        public string? Stan { get; set; }

        [DisplayName("Status")]
        public string? Status { get; set; }
    }
}
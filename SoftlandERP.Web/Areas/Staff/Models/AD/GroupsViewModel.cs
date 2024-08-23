using System.ComponentModel;
using SoftlandERP.Data.Entities.Staff.AD;

namespace SoftlandERP.Web.Areas.Staff.Models.AD
{
    public class GroupsViewModel
    {
        [DisplayName("Organizacje")]
        public List<ADGroup> Organizations { get; set; }

        [DisplayName("Departamenty")]
        public List<ADGroup> Departments { get; set; }

        [DisplayName("Działy")]
        public List<ADGroup> SubDepartments { get; set; }
    }
}
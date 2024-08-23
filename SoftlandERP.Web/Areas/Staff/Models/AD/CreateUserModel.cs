using SoftlandERP.Data.Entities.Staff.AD;
using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Data.Entities.Vocabularies.Staff;

namespace SoftlandERP.Web.Areas.Staff.Models.AD
{
    public class CreateUserModel
    {
        public ADUser ADUser { get; set; }

        public IEnumerable<UserAddress> UserAddresess { get; set; }

        public IEnumerable<HelperText> HelperTexts { get; set; }
    }
}
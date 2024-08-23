using SoftlandERP.Data.Entities.Vocabularies.General;

namespace SoftlandERP.Web.Areas.Administration.Models.Vocabularies.General
{
    public class ApplicationPermissionRuleCreateModel
    {
        public ApplicationPermissionsRule ApplicationPermissionRule { get; set; }

        public IEnumerable<string?> Modules { get; set; }

        public IEnumerable<string> Groups { get; set; }

        public IEnumerable<HelperText> HelperTexts { get; set; }
    }
}
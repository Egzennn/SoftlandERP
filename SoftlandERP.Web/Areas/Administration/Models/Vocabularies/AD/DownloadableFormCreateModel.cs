using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Data.Entities.Vocabularies.Staff;

namespace SoftlandERP.Web.Areas.Administration.Models.Vocabularies.AD
{
    public class DownloadableFormCreateModel
    {
        public DownloadableForm DownloadableForm { get; set; }

        public IEnumerable<HelperText> HelperTexts { get; set; }
    }
}
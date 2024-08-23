using System.Globalization;
using System.Net;
using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Data.Entities.Vocabularies.Staff;

namespace SoftlandERP.Web.Areas.Administration.Models.Vocabularies.AD
{
    public class UserAddressCreateModel
    {
        public UserAddressCreateModel()
        {
            this.CountryList = new List<string>();

            foreach (CultureInfo cInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                RegionInfo country = new (cInfo.Name);
                if (!this.CountryList.Contains(country.EnglishName))
                {
                    this.CountryList.Add(country.EnglishName);
                }
            }

            this.CountryList = this.CountryList.Order().ToList();
        }

        public UserAddress UserAddress { get; set; }

        public IList<string> CountryList { get; }

        public IEnumerable<HelperText> HelperTexts { get; set; }
    }
}
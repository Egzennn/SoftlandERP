using System.ComponentModel;

namespace SoftlandERP.Web.Areas.Administration.Models.Tools
{
    public class QuotaToolViewModel
    {
        [DisplayName("Status")]
        public string StatusString { get; set; }

        [DisplayName("Użytkownik")]
        public string User { get; set; }

        [DisplayName("Wykorzystana przestrzeń")]
        public string DiskSpaceUsed { get; set; }

        [DisplayName("Limit")]
        public string Limit { get; set; }

        [DisplayName("Poziom ostrzeżenia")]
        public string WarningLimit { get; set; }

        [DisplayName("Dysk")]
        public string QuotaVolume { get; set; }

        [DisplayName("Nazwa komputeru (zasobu)")]
        public string ComputerName { get; set; }
    }
}
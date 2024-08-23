using System.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Management.Infrastructure;
using Microsoft.Management.Infrastructure.Options;
using NToastNotify;
using SoftlandERP.Core.Helpers;
using SoftlandERP.Core.Repositories.Interfaces;
using SoftlandERP.Data.Configurations;
using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Web.Areas.Administration.Models.Tools;
using SoftlandERP.Web.Controllers;

namespace SoftlandERP.Web.Areas.Administration.Controllers.Tools
{
    [Area("Administration")]
    public class ToolsQuotaToolController : BaseController
    {
        public static readonly string ModuleName = "Wpisy przydziału dyskrowego";

        private readonly ADConfiguration adConfiguration;

        public ToolsQuotaToolController(IADRepository adRepository, IRepository<HelperText> helperTextRepository, IToastNotification toastNotification, ILogger<BaseController> logger, IOptions<ADConfiguration> adConfiguration)
            : base(adRepository, helperTextRepository, toastNotification, logger)
        {
            this.adConfiguration = adConfiguration.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            this.ViewBag.Title = ModuleName;

            var model = new List<QuotaToolViewModel>();

            IDictionary<string, string> computersList = new Dictionary<string, string> { { "ad.softland20.pl", "AD.SOFTLAND20.PL (10.10.0.2) - Zasoby plikowe" }, { "remotepl.softland20.pl", "RemotePL.SOFTLAND20.PL (10.10.0.7) - Zdalny" }, { "remoteua.softland20.pl", "RemoteUA.SOFTLAND20.PL (10.10.0.8) - Zdalny" } };
            SecureString password = new SecureString();

            foreach (char c in this.adConfiguration.Password ?? string.Empty)
            {
                password.AppendChar(c);
            }

            CimCredential credentials = new CimCredential(PasswordAuthenticationMechanism.Kerberos, "SOFTLAND20.PL", this.adConfiguration.Username, password);

            foreach (var computer in computersList)
            {
                model.AddRange(this.GetQuota(credentials, computer.Key, computer.Value));
            }

            return this.View(model.DistinctBy(x => new { x.ComputerName, x.User, x.QuotaVolume }));
        }

        [HttpGet]
        public IActionResult ExportExcel()
        {
            try
            {
                var model = new List<QuotaToolViewModel>();

                IDictionary<string, string> computersList = new Dictionary<string, string> { { "ad.softland20.pl", "AD.SOFTLAND20.PL (10.10.0.2) - Zasoby plikowe" }, { "remotepl.softland20.pl", "RemotePL.SOFTLAND20.PL (10.10.0.7) - Zdalny" }, { "remoteua.softland20.pl", "RemoteUA.SOFTLAND20.PL (10.10.0.8) - Zdalny" } };
                SecureString password = new SecureString();

                foreach (char c in this.adConfiguration.Password ?? string.Empty)
                {
                    password.AppendChar(c);
                }

                CimCredential credentials = new CimCredential(PasswordAuthenticationMechanism.Kerberos, "SOFTLAND20.PL", this.adConfiguration.Username, password);

                foreach (var computer in computersList)
                {
                    model.AddRange(this.GetQuota(credentials, computer.Key, computer.Value));
                }

                return ExcelExporter.Export(model, ModuleName);
            }
            catch
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z domeną");
                return this.RedirectToAction(nameof(this.Index));
            }
        }

        private List<QuotaToolViewModel> GetQuota(CimCredential credentials, string computerName, string label)
        {
            try
            {
                var result = new List<QuotaToolViewModel>();

                WSManSessionOptions sessionOptions = new WSManSessionOptions();
                sessionOptions.AddDestinationCredentials(credentials);

                CimSession session = CimSession.Create(computerName, sessionOptions);

                var output = session.QueryInstances(@"root\cimv2", "WQL", "SELECT * FROM Win32_DiskQuota");

                foreach (var item in output)
                {
                    if (Convert.ToUInt64(item.CimInstanceProperties["Limit"]?.Value) > 0)
                    {
                        result.Add(new QuotaToolViewModel
                        {
                            StatusString = Convert.ToUInt32(item.CimInstanceProperties["Status"]?.Value) == 0 ? "OK" : Convert.ToUInt32(item.CimInstanceProperties["Status"]?.Value) == 1 ? "Ostrzeżenie" : "Przekroczono",
                            User = ((CimInstance)item.CimInstanceProperties["User"].Value).CimInstanceProperties["Name"]?.Value.ToString() ?? string.Empty,
                            DiskSpaceUsed = this.FormatBytes(Convert.ToUInt64(item.CimInstanceProperties["DiskSpaceUsed"]?.Value)) ?? string.Empty,
                            Limit = this.FormatBytes(Convert.ToUInt64(item.CimInstanceProperties["Limit"]?.Value)) ?? string.Empty,
                            WarningLimit = this.FormatBytes(Convert.ToUInt64(item.CimInstanceProperties["WarningLimit"]?.Value)) ?? string.Empty,
                            QuotaVolume = ((CimInstance)item.CimInstanceProperties["QuotaVolume"].Value).CimInstanceProperties["DeviceID"]?.Value.ToString() ?? string.Empty,
                            ComputerName = label,
                        });
                    }
                }

                return result;
            }
            catch
            {
                return new List<QuotaToolViewModel>();
            }
        }

        private string FormatBytes(ulong bytes)
        {
            string[] suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return string.Format("{0:0.##} {1}", dblSByte, suffix[i]);
        }
    }
}
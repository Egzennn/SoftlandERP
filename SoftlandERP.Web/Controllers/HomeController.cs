using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SoftlandERP.Core.Helpers;
using SoftlandERP.Core.Repositories.Interfaces;
using SoftlandERP.Data.Entities.Vocabularies.General;

namespace SoftlandERP.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IADRepository adRepository, IRepository<HelperText> helperTextRepository, IToastNotification toastNotification, ILogger<BaseController> logger)
            : base(adRepository, helperTextRepository, toastNotification, logger)
        {
        }

        public IActionResult Index()
        {
            this.ViewBag.Title = "Strona głowna";

            try
            {
                this.ViewBag.UsersCount = this.adRepository.GetUsersCount();
                this.ViewBag.ComputersCount = this.adRepository.GetComputersCount();
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z domeną");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
            }

            return this.View();
        }
    }
}
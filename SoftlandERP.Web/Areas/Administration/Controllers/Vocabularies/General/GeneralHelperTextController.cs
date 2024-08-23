using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SoftlandERP.Core.Helpers;
using SoftlandERP.Core.Repositories.Interfaces;
using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Web.Controllers;

namespace SoftlandERP.Web.Areas.Administration.Controllers.Vocabularies.General
{
    [Area("Administration")]
    public class GeneralHelperTextController : BaseController
    {
        public static readonly string Module = "Słownik - ";
        public static readonly string Name = "Tekst pomocniczy";
        public static readonly string ModuleName = Module + Name;

        public GeneralHelperTextController(IADRepository adRepository, IRepository<HelperText> helperTextRepository, IToastNotification toastNotification, ILogger<BaseController> logger)
            : base(adRepository, helperTextRepository, toastNotification, logger)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                this.ViewBag.Title = ModuleName;
                this.ViewBag.IsAdmin = this.CheckPermission(ModuleName);

                var result = this.helperTextRepository.GetAllAsync().Result;

                if (result == null)
                {
                    return this.StatusCode(500);
                }

                return this.View(result);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                this.logger.LogError("ERROR: {Message}", ex.Message);
                return this.Redirect("~/");
            }
        }

        [HttpGet]
        public ActionResult Update(Guid id)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.StatusCode(403);
                }

                this.ViewBag.ModalTitle = "Modyfikacja tekstu pomocniczego";

                return this.PartialView("Modals/Update", this.helperTextRepository.GetByIdAsync(id).Result);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                this.logger.LogError("ERROR: {Message}", ex.Message);
                return this.RedirectToAction(nameof(this.Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(HelperText model)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.RedirectToAction(nameof(this.Index));
                }

                if (model != null)
                {
                    model.UpdatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name);
                    model.Updated = DateTime.Now;
                }

                if (this.helperTextRepository.UpdateAsync(model).Result)
                {
                    this.toastNotification.AddSuccessToastMessage("Powodzenie. Text pomocniczy został zapisane");
                }
                else
                {
                    this.toastNotification.AddErrorToastMessage("Bład przy próbie zapisania textu pomocniczego");
                }

                return this.RedirectToAction(nameof(this.Index));
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                this.logger.LogError("ERROR: {Message}", ex.Message);
                return this.RedirectToAction(nameof(this.Index));
            }
        }
    }
}
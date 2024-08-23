using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SoftlandERP.Core.Helpers;
using SoftlandERP.Core.Repositories.Interfaces;
using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Web.Areas.Administration.Models.Vocabularies.General;
using SoftlandERP.Web.Controllers;

namespace SoftlandERP.Web.Areas.Administration.Controllers.Vocabularies.General
{
    [Area("Administration")]
    public class GeneralApplicationPermissionsRuleController : BaseController
    {
        public static readonly string Module = "Słownik - ";
        public static readonly string Name = "Uprawnienia";
        public static readonly string ModuleName = Module + Name;

        private readonly IRepository<ApplicationPermissionsRule> applicationPermissionsRuleRepository;

        public GeneralApplicationPermissionsRuleController(IADRepository adRepository, IRepository<HelperText> helperTextRepository, IToastNotification toastNotification, ILogger<BaseController> logger, IRepository<ApplicationPermissionsRule> applicationPermissionsRuleRepository)
            : base(adRepository, helperTextRepository, toastNotification, logger)
        {
            this.applicationPermissionsRuleRepository = applicationPermissionsRuleRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                this.ViewBag.Title = Name;
                this.ViewBag.IsAdmin = this.CheckPermission(ModuleName);

                var result = this.applicationPermissionsRuleRepository.GetAllAsync().Result;

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
        public ActionResult Create()
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.StatusCode(403);
                }

                this.ViewBag.ModalTitle = "Dodanie uprawnień administracyjnych";

                ApplicationPermissionRuleCreateModel model = new ()
                {
                    ApplicationPermissionRule = new ApplicationPermissionsRule(),
                    Modules = ModulesList.GetMolulesName() ?? new List<string>(),
                    Groups = this.adRepository.GetAllADGroupsName()?.ToList() ?? new List<string>(),
                    HelperTexts = this.helperTextRepository.GetAllAsync().Result ?? new List<HelperText>(),
                };
                return this.PartialView("Modals/Create", model);
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
        public ActionResult Create(ApplicationPermissionRuleCreateModel model)
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
                    model.ApplicationPermissionRule.CreatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name);
                }

                if (this.applicationPermissionsRuleRepository.InsertAsync(model.ApplicationPermissionRule).Result)
                {
                    this.toastNotification.AddSuccessToastMessage("Powodzenie. Uprawnienia zostały zapisane");
                }
                else
                {
                    this.toastNotification.AddErrorToastMessage("Bład przy próbie zapisania uprawnień");
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

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.StatusCode(403);
                }

                this.ViewBag.ModalTitle = "Potwierdzenie";

                return this.PartialView("Modals/Delete", this.applicationPermissionsRuleRepository.GetByIdAsync(id).Result);
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
        public ActionResult Delete(ApplicationPermissionsRule model)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.RedirectToAction(nameof(this.Index));
                }

                if (this.applicationPermissionsRuleRepository.DeleteAsync(model.Id).Result)
                {
                    this.toastNotification.AddSuccessToastMessage("Powodzenie. Uprawnienia zostały usunięte");
                }
                else
                {
                    this.toastNotification.AddErrorToastMessage("Bład przy próbie usunięcia uprawnień");
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
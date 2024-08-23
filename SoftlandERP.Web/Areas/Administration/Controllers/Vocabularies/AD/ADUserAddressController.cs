using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using SoftlandERP.Core.Helpers;
using SoftlandERP.Core.Repositories.Interfaces;
using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Data.Entities.Vocabularies.Staff;
using SoftlandERP.Web.Areas.Administration.Models.Vocabularies.AD;
using SoftlandERP.Web.Controllers;

namespace SoftlandERP.Web.Areas.Administration.Controllers.Vocabularies.AD
{
    [Area("Administration")]
    public class ADUserAddressController : BaseController
    {
        public static readonly string Module = "Słownik - ";
        public static readonly string Name = "Adresy";
        public static readonly string ModuleName = Module + Name;

        private readonly IUserAddressRepository userAddressVocabularyRepository;

        public ADUserAddressController(IADRepository adRepository, IRepository<HelperText> helperTextVocabularyRepository, IToastNotification toastNotification, ILogger<BaseController> logger, IUserAddressRepository userAddressVocabularyRepository)
            : base(adRepository, helperTextVocabularyRepository, toastNotification, logger)
        {
            this.userAddressVocabularyRepository = userAddressVocabularyRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                this.ViewBag.Title = ModuleName;
                this.ViewBag.IsAdmin = this.CheckPermission(ModuleName);

                var result = this.userAddressVocabularyRepository.GetAllAsync().Result;

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
        public IActionResult Create(Guid? id)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.StatusCode(403);
                }

                this.ViewBag.ModalTitle = "Dodanie rekordu do słownika " + Name;

                UserAddressCreateModel model = new ()
                {
                    UserAddress = id != null ? this.userAddressVocabularyRepository.GetByIdAsync(id).Result ?? new UserAddress() : new UserAddress(),
                    HelperTexts = this.helperTextRepository.GetAllAsync().Result ?? new List<HelperText>()
                };
                return this.PartialView("Modals/Create", model);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                this.logger.LogError("ERROR: {Message}", ex.Message);
                return this.StatusCode(500);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserAddress model)
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
                    if (this.userAddressVocabularyRepository.GetByIdAsync(model.Id).Result != null)
                    {
                        model.Updated = DateTime.Now;
                        model.UpdatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name);
                        model.TwoLetterISOCountryName = new RegionInfo(CultureInfo.GetCultures(CultureTypes.SpecificCultures).Where(x => new RegionInfo(x.Name).EnglishName == model.Country).FirstOrDefault()?.Name ?? string.Empty).TwoLetterISORegionName;

                        if (this.userAddressVocabularyRepository.UpdateAsync(model).Result)
                        {
                            this.toastNotification.AddSuccessToastMessage("Powodzenie. Adres został zmodyfikowany");
                        }
                        else
                        {
                            this.toastNotification.AddErrorToastMessage("Bład przy próbie modyfikacji adresu");
                        }
                    }
                    else
                    {
                        model.CreatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name);
                        model.TwoLetterISOCountryName = new RegionInfo(CultureInfo.GetCultures(CultureTypes.SpecificCultures).Where(x => new RegionInfo(x.Name).EnglishName == model.Country).FirstOrDefault()?.Name ?? string.Empty).TwoLetterISORegionName;

                        if (this.userAddressVocabularyRepository.InsertAsync(model).Result)
                        {
                            this.toastNotification.AddSuccessToastMessage("Powodzenie. Adres został zmodyfikowany");
                        }
                        else
                        {
                            this.toastNotification.AddErrorToastMessage("Bład przy próbie modyfikacji adresu");
                        }
                    }
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
        public IActionResult Delete(Guid id)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.StatusCode(403);
                }

                this.ViewBag.ModalTitle = "Potwierdzenie";

                return this.PartialView("Modals/Delete", this.userAddressVocabularyRepository.GetByIdAsync(id).Result);
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
        public IActionResult Delete(UserAddress address)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.RedirectToAction(nameof(this.Index));
                }

                if (this.userAddressVocabularyRepository.DeleteAsync(address.Id).Result)
                {
                    this.toastNotification.AddSuccessToastMessage("Powodzenie. Adres został usunięty");
                }
                else
                {
                    this.toastNotification.AddErrorToastMessage("Bład przy próbie usunięcia adresu");
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
        public JsonResult GetCitiesByCountry(string country)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(country))
                {
                    IEnumerable<SelectListItem> cities = new SelectList(this.userAddressVocabularyRepository.FindCitiesByCountryAsync(country).Result?.Select(x => new SelectListItem { Value = x, Text = x }).ToList(), "Value", "Text");
                    return this.Json(cities);
                }

                return this.Json(string.Empty);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                this.logger.LogError("ERROR: {Message}", ex.Message);
                return this.Json(string.Empty);
            }
        }

        [HttpGet]
        public JsonResult GetStreetsByCity(string city)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(city))
                {
                    IEnumerable<SelectListItem> cities = new SelectList(this.userAddressVocabularyRepository.FindStreetsByCityAsync(city).Result?.Select(x => new SelectListItem { Value = x, Text = x }).ToList(), "Value", "Text");
                    return this.Json(cities);
                }

                return this.Json(string.Empty);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                this.logger.LogError("ERROR: {Message}", ex.Message);
                return this.Json(string.Empty);
            }
        }
    }
}
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using SoftlandERP.Core.Helpers;
using SoftlandERP.Core.Repositories.Interfaces;
using SoftlandERP.Data.DB;
using SoftlandERP.Data.Entities.Forms.KL;
using SoftlandERP.Data.Entities.Forms.Stanowisko;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Ogolne;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Stanowisko;
using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Web.Controllers;

namespace SoftlandERP.Web.Areas.Forms.Controllers.Stanowisko
{
    [Area("Forms")]
    public class StanowiskoDOWFormController : BaseController
    {
        public static readonly string Module = "Formularz - ";
        public static readonly string Name1 = "Stanowisko - ";
        public static readonly string Name2 = "DOW";
        public static readonly string Name = Name1 + Name2;
        public static readonly string ModuleName = Module + Name;

        private readonly IRepository<StanowiskoDOWForm> repository;
        private readonly IRepository<OgolneStan> stanRepository;
        private readonly IRepository<OgolneStatus> statusRepository;
        private readonly IRepository<StanowiskoObiekt> obiektRepository;
        private readonly IRepository<StanowiskoZdarzenie> zdarzenieRepository;
        private readonly XLContext xlContext;

        public StanowiskoDOWFormController(IADRepository adRepository, IRepository<HelperText> helperTextRepository, IToastNotification toastNotification, ILogger<BaseController> logger, IRepository<StanowiskoDOWForm> repository, IRepository<OgolneStan> stanRepository, IRepository<OgolneStatus> statusRepository, IRepository<StanowiskoObiekt> obiektRepository, IRepository<StanowiskoZdarzenie> zdarzenieRepository, XLContext xlContext)
            : base(adRepository, helperTextRepository, toastNotification, logger)
        {
            this.repository = repository;
            this.stanRepository = stanRepository;
            this.statusRepository = statusRepository;
            this.obiektRepository = obiektRepository;
            this.zdarzenieRepository = zdarzenieRepository;
            this.xlContext = xlContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                this.ViewBag.Title = ModuleName;
                this.ViewBag.IsAdmin = this.CheckPermission(ModuleName);
                this.ViewBag.IsIT = this.adRepository.CheckGroup(this.User?.Identity?.Name, new List<string> { "S_ADM_IT" });
                this.ViewBag.DisplayName = this.GetSignedInDisplayName(this.User?.Identity?.Name);
                this.ViewBag.Odpowiedzialny = new SelectList(this.adRepository.GetAllADUserAcronyms());

                var result = this.repository.GetAllAsync().Result;

                if (result == null)
                {
                    return this.StatusCode(500);
                }

                if (this.ViewBag.IsIT)
                {
                    var filteredResult = result.Where(x => x.Stan != "Archiwalny");

                    return this.View(filteredResult);
                }
                else
                {
                    var filteredResult = result.Where(x => x.CreatedBy == this.ViewBag.DisplayName).Where(x => x.Stan != "Archiwalny");

                    return this.View(filteredResult);
                }
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
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

                this.ViewBag.ModalTitle = "Dodanie wpisu do formularza " + Name;
                this.ViewBag.Odpowiedzialny = new SelectList(this.adRepository.GetAllADUserAcronyms());

                this.ViewBag.Obiekt = new SelectList(this.obiektRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                this.ViewBag.Zdarzenie = new SelectList(this.zdarzenieRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());

                if (id != null)
                {
                    this.ViewBag.Title = "Edycja wpisu formularza " + Name;
                    this.ViewBag.Stany = new SelectList(this.stanRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                    this.ViewBag.Statusy = new SelectList(this.statusRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                }

                return this.PartialView("Modals/Create", this.repository.GetByIdAsync(id).Result ?? new StanowiskoDOWForm());
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.StatusCode(500);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StanowiskoDOWForm model)
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
                    if (this.repository.GetByIdAsync(model.Id).Result != null)
                    {
                        model.Updated = DateTime.Now;
                        model.UpdatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name);
                        if (model.Stan == "Archiwalny")
                        {
                            if (this.repository.UpdateAsync(model).Result)
                            {
                                this.toastNotification.AddSuccessToastMessage("Powodzenie. Wpis został zmodyfikowany");
                            }
                            else
                            {
                                this.toastNotification.AddErrorToastMessage("Bład przy próbie modyfikacji wpisu");
                            }
                        }
                        else
                        {
                            StanowiskoDOWForm? newmodel = this.repository.GetByIdAsync(model.Id).Result ?? throw new Exception("Karta not found in db");

                            newmodel.Id = Guid.NewGuid();
                            newmodel.Updated = DateTime.Now;
                            newmodel.UpdatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name);
                            newmodel.Stan = "Archiwalny";

                            if (this.repository.UpdateAsync(model).Result && this.repository.InsertAsync(newmodel).Result)
                            {
                                this.toastNotification.AddSuccessToastMessage("Powodzenie. Wpis został zmodyfikowany");
                            }
                            else
                            {
                                this.toastNotification.AddErrorToastMessage("Bład przy próbie modyfikacji wpisu");
                            }
                        }
                    }
                    else
                    {
                        model.CreatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name);
                        model.Stan = "Aktywny";
                        model.Status = "Akceptacja";

                        if (this.repository.InsertAsync(model).Result)
                        {
                            this.toastNotification.AddSuccessToastMessage("Powodzenie. Wpis został dodany");
                        }
                        else
                        {
                            this.toastNotification.AddErrorToastMessage("Bład przy próbie dodawania wpisu");
                        }
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
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

                return this.PartialView("Modals/Delete", this.repository.GetByIdAsync(id).Result);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.RedirectToAction(nameof(this.Index));
            }
        }

        public IActionResult Clone(Guid? id)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.StatusCode(403);
                }

                this.ViewBag.ModalTitle = "Kopiowanie wpisu formularza " + Name;
                this.ViewBag.Stany = new SelectList(this.stanRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                this.ViewBag.Statusy = new SelectList(this.statusRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                this.ViewBag.Odpowiedzialny = new SelectList(this.adRepository.GetAllADUserAcronyms());

                this.ViewBag.Obiekt = new SelectList(this.obiektRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                this.ViewBag.Zdarzenie = new SelectList(this.zdarzenieRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());

                var oldValue = this.repository.GetByIdAsync(id).Result ?? new StanowiskoDOWForm();

                this.ModelState.Clear();
                var newValue = new StanowiskoDOWForm()
                {
                    Obiekt = oldValue.Obiekt,
                    IDI = oldValue.IDI,
                    Nazwa = oldValue.Nazwa,
                    Odpowiedzialny = oldValue.Odpowiedzialny,
                    Zdarzenie = oldValue.Zdarzenie,
                    Opis = oldValue.Opis,
                    Termin = oldValue.Termin,
                    ObszarRoboczy = oldValue.ObszarRoboczy,
                    CreatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name),
                    Stan = oldValue.Stan,
                    Status = oldValue.Status
                };

                return this.PartialView("Modals/Create", newValue);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.StatusCode(500);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(StanowiskoDOWForm model)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.RedirectToAction(nameof(this.Index));
                }

                if (this.repository.DeleteAsync(model.Id).Result)
                {
                    this.toastNotification.AddSuccessToastMessage("Powodzenie. Rekord został usunięty");
                }
                else
                {
                    this.toastNotification.AddErrorToastMessage("Bład przy próbie usunięcia rekordu");
                }

                return this.RedirectToAction(nameof(this.Index));
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.RedirectToAction(nameof(this.Index));
            }
        }

        [HttpGet]
        public IActionResult ExportExcel()
        {
            try
            {
                return ExcelExporter.Export(this.repository.GetAllAsync().Result?.ToList() ?? new List<StanowiskoDOWForm>(), Name);
            }
            catch
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                return this.RedirectToAction(nameof(this.Index));
            }
        }
    }
}

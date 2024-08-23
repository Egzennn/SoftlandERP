﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using SoftlandERP.Core.Helpers;
using SoftlandERP.Core.Repositories.Interfaces;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Dokumenty;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Ogolne;
using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Web.Controllers;

namespace SoftlandERP.Web.Areas.Administration.Controllers.Vocabularies.Forms.Dokumenty
{
    [Area("Administration")]
    public class FormsDokumentyRodzajKosztuController : BaseController
    {
        public static readonly string Module = "Słownik - ";
        public static readonly string Name = "Rodzaj kosztu";
        public static readonly string ModuleName = Module + Name;

        private readonly IRepository<DokumentyRodzajKosztu> repository;
        private readonly IRepository<OgolneStan> stanRepository;

        public FormsDokumentyRodzajKosztuController(IADRepository adRepository, IRepository<HelperText> helperTextVocabularyRepository, IToastNotification toastNotification, ILogger<BaseController> logger, IRepository<DokumentyRodzajKosztu> repository, IRepository<OgolneStan> stanRepository)
            : base(adRepository, helperTextVocabularyRepository, toastNotification, logger)
        {
            this.repository = repository;
            this.stanRepository = stanRepository;
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
                this.ViewBag.Odpowiedzialny = new SelectList(this.adRepository.GetAllADUserAcronyms());

                if (id != null)
                {
                    this.ViewBag.Stany = new SelectList(this.stanRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList());
                }

                return this.PartialView("Modals/Create", this.repository.GetByIdAsync(id).Result ?? new DokumentyRodzajKosztu());
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
        public IActionResult Create(DokumentyRodzajKosztu model)
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

                        if (this.repository.UpdateAsync(model).Result)
                        {
                            this.toastNotification.AddSuccessToastMessage("Powodzenie. Rekord został zmodyfikowany");
                        }
                        else
                        {
                            this.toastNotification.AddErrorToastMessage("Bład przy próbie modyfikacji rekord");
                        }
                    }
                    else
                    {
                        model.CreatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name);
                        model.Stan = "Plan";

                        if (this.repository.InsertAsync(model).Result)
                        {
                            this.toastNotification.AddSuccessToastMessage("Powodzenie. Rekord został zmodyfikowany");
                        }
                        else
                        {
                            this.toastNotification.AddErrorToastMessage("Bład przy próbie modyfikacji rekord");
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

                return this.PartialView("Modals/Delete", this.repository.GetByIdAsync(id).Result);
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
        public IActionResult Delete(DokumentyRodzajKosztu model)
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
                this.logger.LogError("ERROR: {Message}", ex.Message);
                return this.RedirectToAction(nameof(this.Index));
            }
        }

        [HttpGet]
        public IActionResult ExportExcel()
        {
            try
            {
                return ExcelExporter.Export(this.repository.GetAllAsync().Result?.ToList() ?? new List<DokumentyRodzajKosztu>(), ModuleName);
            }
            catch
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                return this.RedirectToAction(nameof(this.Index));
            }
        }

        public async Task<ActionResult> AktualizujOdpowiedzialnego(string odpowiedzialnySelectList)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.RedirectToAction(nameof(this.Index));
                }

                var values = this.repository.GetAllAsync().Result;

                if (values?.Any() == true)
                {
                    foreach (var value in values)
                    {
                        value.Updated = DateTime.Now;
                        value.UpdatedBy = this.User?.Identity?.Name;
                        value.Odpowiedzialny = odpowiedzialnySelectList;
                        await this.repository.UpdateAsync(value);
                    }

                    this.toastNotification.AddSuccessToastMessage("Powodzenie. Rekord został zmodyfikowany");
                }
                else
                {
                    this.toastNotification.AddErrorToastMessage("Bład przy próbie modyfikacji rekordu");
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

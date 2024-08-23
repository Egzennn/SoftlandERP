using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using SoftlandERP.Core.Helpers;
using SoftlandERP.Core.Repositories.Interfaces;
using SoftlandERP.Core.Repositories.Interfaces.Spedycja;
using SoftlandERP.Data.DB;
using SoftlandERP.Data.Entities.Forms.Spedycja;
using SoftlandERP.Data.Entities.Views;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Ogolne;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Spedycja;
using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Web.Areas.Forms.Models.Spedycja;
using SoftlandERP.Web.Controllers;
using static SoftlandERP.Core.Repositories.Spedycja.SekcjaTowarRepository;
using static SoftlandERP.Core.Repositories.Spedycja.StandardyPrzygotowaniaRepository;

namespace SoftlandERP.Web.Areas.Forms.Controllers.Spedycja
{
    [Area("Forms")]
    public class SpedycjaKonfekcjaFormController : BaseController
    {
        public static readonly string Module = "Formularz - ";
        public static readonly string Name = "Spedycja - Konfekcja";
        public static readonly string ModuleName = Module + Name;

        private readonly IRepository<SpedycjaKonfekcjaForm> repository;
        private readonly IRepository<OgolneStan> stanRepository;
        private readonly IRepository<OgolneStatus> statusRepository;
        private readonly IRepository<SpedycjaStandardyPrzygotowania> standardyprzygotowaniaRepository;
        private readonly IRepository<SpedycjaCzynnosciMagazynowoSpedycyjneMag> spedycjaCzynnosciMagazynowoSpedycyjneMagRepository;
        private readonly ISekcjaTowarRepository sekcjaTowarRepository;
        private readonly IStandardyPrzygotowaniaRepository standardyPrzygotowaniaRepository;
        private readonly XLContext xlContext;
        private readonly MainContext mainContext;
        private readonly DbSet<SpedycjaKonfekcjaSekcje> spedycjaKonfekcjaSekcjeModel;

        public SpedycjaKonfekcjaFormController(IADRepository adRepository, IRepository<HelperText> helperTextRepository, IToastNotification toastNotification, ILogger<BaseController> logger, IRepository<SpedycjaKonfekcjaForm> repository, IRepository<OgolneStan> stanRepository, IRepository<OgolneStatus> statusRepository, IRepository<SpedycjaStandardyPrzygotowania> standardyprzygotowaniaRepository, IRepository<SpedycjaCzynnosciMagazynowoSpedycyjneMag> spedycjaCzynnosciMagazynowoSpedycyjneMagRepository, ISekcjaTowarRepository sekcjaTowarRepository, IStandardyPrzygotowaniaRepository standardyPrzygotowaniaRepository, XLContext xlContext, MainContext mainContext)
            : base(adRepository, helperTextRepository, toastNotification, logger)
        {
            this.repository = repository;
            this.stanRepository = stanRepository;
            this.statusRepository = statusRepository;
            this.standardyprzygotowaniaRepository = standardyprzygotowaniaRepository;
            this.spedycjaCzynnosciMagazynowoSpedycyjneMagRepository = spedycjaCzynnosciMagazynowoSpedycyjneMagRepository;
            this.sekcjaTowarRepository = sekcjaTowarRepository;
            this.standardyPrzygotowaniaRepository = standardyPrzygotowaniaRepository;
            this.xlContext = xlContext;
            this.mainContext = mainContext;
            this.spedycjaKonfekcjaSekcjeModel = mainContext.Set<SpedycjaKonfekcjaSekcje>();
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
                this.ViewBag.Stany = new SelectList(this.stanRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());

                var result = this.repository.GetAllAsync().Result;

                if (result == null)
                {
                    return this.StatusCode(500);
                }

                var filteredResult = result.Where(x => x.Stan != "Archiwalny");

                return this.View(filteredResult);
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

                var model = new CreateSpedycjaKonfekcjaModel();

                this.ViewBag.Title = "Dodanie wpisu do formularza " + Name;
                this.ViewBag.Odpowiedzialny = new SelectList(this.adRepository.GetAllADUserAcronyms());
                string displayName = this.GetSignedInDisplayName(this.User?.Identity?.Name);
                this.ViewBag.DisplayName = displayName;

                this.ViewBag.AkronimLog = new SelectList(this.standardyprzygotowaniaRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.AkronimLog).Select(x => x.AkronimLog).Distinct().ToList() ?? new List<string?>());
                this.ViewBag.Sekcja = new SelectList(this.spedycjaKonfekcjaSekcjeModel.Where(x => x.Odpowiedzialny == displayName).OrderBy(x => x.Sekcja).Select(x => x.Sekcja).Distinct().ToList());
                this.ViewBag.Towar = new SelectList(this.spedycjaKonfekcjaSekcjeModel.Where(x => x.Odpowiedzialny == displayName).OrderBy(x => x.Twr_Kod).Select(x => x.Twr_Kod).Distinct().ToList());

                if (id != null)
                {
                    this.ViewBag.Title = "Edycja wpisu formularza " + Name;
                    this.ViewBag.AkronimWyk = this.ViewBag.Odpowiedzialny;
                    this.ViewBag.Stany = new SelectList(this.stanRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                    this.ViewBag.Statusy = new SelectList(this.statusRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                }

                model.SpedycjaKonfekcjaForm = this.repository.GetByIdAsync(id).Result ?? new SpedycjaKonfekcjaForm();

                return this.View("Create", model);
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
        public IActionResult Create(CreateSpedycjaKonfekcjaModel model)
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
                    if (this.repository.GetByIdAsync(model.SpedycjaKonfekcjaForm.Id).Result != null)
                    {
                        model.SpedycjaKonfekcjaForm.Updated = DateTime.Now;
                        model.SpedycjaKonfekcjaForm.UpdatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name);

                        if (this.repository.UpdateAsync(model.SpedycjaKonfekcjaForm).Result)
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
                        model.SpedycjaKonfekcjaForm.CreatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name);
                        model.SpedycjaKonfekcjaForm.Stan = "Plan";
                        model.SpedycjaKonfekcjaForm.Status = "Akceptacja";

                        if (this.repository.InsertAsync(model.SpedycjaKonfekcjaForm).Result)
                        {
                            this.toastNotification.AddSuccessToastMessage("Powodzenie. Wpis został zmodyfikowany");
                        }
                        else
                        {
                            this.toastNotification.AddErrorToastMessage("Bład przy próbie modyfikacji wpisu");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(SpedycjaKonfekcjaForm model)
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
                return ExcelExporter.Export(this.repository.GetAllAsync().Result?.ToList() ?? new List<SpedycjaKonfekcjaForm>(), Name);
            }
            catch
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                return this.RedirectToAction(nameof(this.Index));
            }
        }

        [HttpGet]
        public JsonResult GetTowarBySekcja(string sekcja)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(sekcja))
                {
                    IEnumerable<SelectListItem> towar = new SelectList(this.sekcjaTowarRepository.FindTowarBySekcjeAsync(sekcja).Result?.Select(x => new SelectListItem { Value = x, Text = x }).ToList(), "Value", "Text");
                    return this.Json(towar);
                }

                return this.Json(string.Empty);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.Json(string.Empty);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetIloscByTowar(string towar)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(towar))
                {
                    IloscSummary? iloscSummary = await this.sekcjaTowarRepository.FindIloscByTowarAsync(towar);
                    return this.Json(iloscSummary);
                }

                return this.Json(string.Empty);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.Json(string.Empty);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetMagByLog(string log)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(log))
                {
                    IEnumerable<SpedycjaStandardyPrzygotowania?>? magSummary = await this.standardyPrzygotowaniaRepository.GetByColumnAsync(log);
                    List<MagOpis> akronimMagList = new ();

                    foreach (var summary in magSummary)
                    {
                        if (summary != null && !string.IsNullOrEmpty(summary.AkronimMag))
                        {
                            MagOpis? magOpis = await this.standardyPrzygotowaniaRepository.FindOpisByAkronim(summary.AkronimMag);
                            akronimMagList.Add(magOpis);
                        }
                    }

                    return this.Json(akronimMagList);
                }

                return this.Json(string.Empty);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.Json(string.Empty);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetTimeByLog(string? log)
        {
            try
            {
                TimeSpan? time = await this.standardyPrzygotowaniaRepository.GetTimeByAkronimLog(log);

                return this.Json(time);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.Json(string.Empty);
            }
        }

        public async Task<ActionResult> AktualizujStan(string stanSelectList, List<Guid?> selectedIds)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.RedirectToAction(nameof(this.Index));
                }

                if (selectedIds == null || !selectedIds.Any())
                {
                    this.toastNotification.AddErrorToastMessage("Nie wybrano rekordów do aktualizacji");
                    return this.RedirectToAction(nameof(this.Index));
                }

                var selectedValues = await this.repository.GetByListIdsAsync(selectedIds);

                if (selectedValues?.Any() == true)
                {
                    var signedInUserName = this.GetSignedInDisplayName(this.User?.Identity?.Name);
                    var currentTime = DateTime.Now;

                    foreach (var value in selectedValues)
                    {
                        // Aktualizuj tylko pola, które chcesz zmienić
                        value.Updated = currentTime;
                        value.UpdatedBy = signedInUserName;
                        value.Stan = stanSelectList;
                    }

                    await this.repository.UpdateRecordsAsync(selectedValues);

                    this.toastNotification.AddSuccessToastMessage("Powodzenie. Rekordy zostały zmodyfikowane");
                }
                else
                {
                    this.toastNotification.AddErrorToastMessage("Błąd przy próbie modyfikacji rekordów");
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

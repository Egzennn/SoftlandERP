using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using SoftlandERP.Core.Helpers;
using SoftlandERP.Core.Repositories.Interfaces;
using SoftlandERP.Core.Repositories.Interfaces.Spedycja;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Ogolne;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Spedycja;
using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Web.Areas.Administration.Models.Vocabularies.Forms.Spedycja;
using SoftlandERP.Web.Controllers;
using static SoftlandERP.Core.Repositories.Spedycja.CzynnosciMagazynowoSpedycyjneLogRepository;
using static SoftlandERP.Core.Repositories.Spedycja.CzynnosciMagazynowoSpedycyjneMagRepository;

namespace SoftlandERP.Web.Areas.Administration.Controllers.Vocabularies.Forms.Spedycja
{
    [Area("Administration")]
    public class FormsSpedycjaStandardyPrzygotowaniaController : BaseController
    {
        public static readonly string Module = "Słownik - ";
        public static readonly string Name = "Spedycja - Standardy przygotowania";
        public static readonly string ModuleName = Module + Name;

        private readonly IRepository<SpedycjaStandardyPrzygotowania> repository;
        private readonly IRepository<OgolneStan> stanRepository;
        private readonly IRepository<SpedycjaCzynnosciMagazynowoSpedycyjneLog> czynnoscimagazynowospedycyjnelogRepository;
        private readonly IRepository<SpedycjaCzynnosciMagazynowoSpedycyjneMag> czynnoscimagazynowospedycyjnemagRepository;
        private readonly ICzynnosciMagazynowoSpedycyjneLogRepository iczynnoscimagazynowospedycyjnelogRepository;
        private readonly ICzynnosciMagazynowoSpedycyjneMagRepository iczynnoscimagazynowospedycyjnemagRepository;

        public FormsSpedycjaStandardyPrzygotowaniaController(IADRepository adRepository, IRepository<HelperText> helperTextVocabularyRepository, IToastNotification toastNotification, ILogger<BaseController> logger, IRepository<SpedycjaStandardyPrzygotowania> repository, IRepository<OgolneStan> stanRepository, IRepository<SpedycjaCzynnosciMagazynowoSpedycyjneLog> czynnoscimagazynowospedycyjnelogRepository, IRepository<SpedycjaCzynnosciMagazynowoSpedycyjneMag> czynnoscimagazynowospedycyjnemagRepository, ICzynnosciMagazynowoSpedycyjneLogRepository iczynnoscimagazynowospedycyjnelogRepository, ICzynnosciMagazynowoSpedycyjneMagRepository iczynnoscimagazynowospedycyjnemagRepository)
            : base(adRepository, helperTextVocabularyRepository, toastNotification, logger)
        {
            this.repository = repository;
            this.stanRepository = stanRepository;
            this.czynnoscimagazynowospedycyjnelogRepository = czynnoscimagazynowospedycyjnelogRepository;
            this.czynnoscimagazynowospedycyjnemagRepository = czynnoscimagazynowospedycyjnemagRepository;
            this.iczynnoscimagazynowospedycyjnelogRepository = iczynnoscimagazynowospedycyjnelogRepository;
            this.iczynnoscimagazynowospedycyjnemagRepository = iczynnoscimagazynowospedycyjnemagRepository;
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

                var model = new CreateSpedycjaStandardyPrzygotowaniaModel();

                this.ViewBag.Title = "Dodanie rekordu do słownika " + Name;
                this.ViewBag.Odpowiedzialny = new SelectList(this.adRepository.GetAllADUserAcronyms());
                this.ViewBag.CzynnosciMagazynowoSpedycyjneLog = new SelectList(this.czynnoscimagazynowospedycyjnelogRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Akronim).Select(x => x.Akronim).ToList() ?? new List<string?>());
                this.ViewBag.CzynnosciMagazynowoSpedycyjneMag = new SelectList(this.czynnoscimagazynowospedycyjnemagRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Akronim).Select(x => x.Akronim).ToList() ?? new List<string?>());

                if (id != null)
                {
                    this.ViewBag.Stany = new SelectList(this.stanRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList());
                }

                model.SpedycjaStandardyPrzygotowania = this.repository.GetByIdAsync(id).Result ?? new SpedycjaStandardyPrzygotowania();

                return this.View("Create", model);
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
        public IActionResult Create(CreateSpedycjaStandardyPrzygotowaniaModel model)
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
                    if (this.repository.GetByIdAsync(model.SpedycjaStandardyPrzygotowania.Id).Result != null)
                    {
                        model.SpedycjaStandardyPrzygotowania.Updated = DateTime.Now;
                        model.SpedycjaStandardyPrzygotowania.UpdatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name);

                        if (this.repository.UpdateAsync(model.SpedycjaStandardyPrzygotowania).Result)
                        {
                            this.toastNotification.AddSuccessToastMessage("Powodzenie. Rekord został zmodyfikowany");
                        }
                        else
                        {
                            this.toastNotification.AddErrorToastMessage("Bład przy próbie modyfikacji rekordu");
                        }
                    }
                    else
                    {
                        model.SpedycjaStandardyPrzygotowania.CreatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name);
                        model.SpedycjaStandardyPrzygotowania.Stan = "Plan";

                        if (this.repository.InsertAsync(model.SpedycjaStandardyPrzygotowania).Result)
                        {
                            this.toastNotification.AddSuccessToastMessage("Powodzenie. Rekord został dodany");
                        }
                        else
                        {
                            this.toastNotification.AddErrorToastMessage("Bład przy próbie modyfikacji rekordu");
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
        public IActionResult Delete(SpedycjaStandardyPrzygotowania model)
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
                return ExcelExporter.Export(this.repository.GetAllAsync().Result?.ToList() ?? new List<SpedycjaStandardyPrzygotowania>(), ModuleName);
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
                        value.UpdatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name);
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

        [HttpGet]
        public async Task<JsonResult> GetLogByAkronim(string akronim)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(akronim))
                {
                    IEnumerable<SpedycjaCzynnosciMagazynowoSpedycyjneLog?>? logSummary = await this.iczynnoscimagazynowospedycyjnelogRepository.GetByColumnAsync(akronim);
                    List<CzynnosciLog> akronimLogList = new ();
                    foreach (var summary in logSummary)
                    {
                        if (summary != null && !string.IsNullOrEmpty(summary.Akronim))
                        {
                            CzynnosciLog? logOpis = await this.iczynnoscimagazynowospedycyjnelogRepository.FindOpisByAkronimLog(summary.Akronim);
                            akronimLogList.Add(logOpis);
                        }
                    }

                    return this.Json(akronimLogList);
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
        public async Task<JsonResult> GetMagByAkronim(string akronim)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(akronim))
                {
                    IEnumerable<SpedycjaCzynnosciMagazynowoSpedycyjneMag?>? magSummary = await this.iczynnoscimagazynowospedycyjnemagRepository.GetByColumnAsync(akronim);
                    List<CzynnosciMag> akronimMagList = new ();
                    foreach (var summary in magSummary)
                    {
                        if (summary != null && !string.IsNullOrEmpty(summary.Akronim))
                        {
                            CzynnosciMag? magOpis = await this.iczynnoscimagazynowospedycyjnemagRepository.FindOpisByAkronimMag(summary.Akronim);
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
                this.logger.LogError("ERROR: {Message}", ex.Message);
                return this.Json(string.Empty);
            }
        }
    }
}

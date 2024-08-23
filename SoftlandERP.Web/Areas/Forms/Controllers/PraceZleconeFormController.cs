using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using SoftlandERP.Core.Helpers;
using SoftlandERP.Core.Repositories.Interfaces;
using SoftlandERP.Core.Repositories.Interfaces.Ogolne;
using SoftlandERP.Data.DB;
using SoftlandERP.Data.Entities.Forms;
using SoftlandERP.Data.Entities.Views;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Ogolne;
using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Web.Controllers;

namespace SoftlandERP.Web.Areas.Forms.Controllers
{
    [Area("Forms")]
    public class PraceZleconeFormController : BaseController
    {
        public static readonly string Module = "Formularz - ";
        public static readonly string Name = "Prace zlecone";
        public static readonly string ModuleName = Module + Name;

        private readonly IRepository<PraceZleconeForm> repository;
        private readonly IRepository<OgolneKategoriaPracy> kategoriaPracyRepository;
        private readonly IRepository<OgolneRodzajPracy> rodzajPracyRepository;
        private readonly IRepository<OgolneRecepturyNormWykonawczych> nazwaRecepturyRepository;
        private readonly IRepository<OgolneStan> stanRepository;
        private readonly IRepository<OgolneStatus> statusRepository;
        private readonly IRecepturyNormWykonawczychRepository recepturyRepository;
        private readonly XLContext xlContext;
        private readonly DbSet<PraceZleconeDokumenty> praceZleconeDokumentyModel;

        public PraceZleconeFormController(IADRepository adRepository, IRepository<HelperText> helperTextRepository, IToastNotification toastNotification, ILogger<BaseController> logger, IRepository<PraceZleconeForm> repository, IRepository<OgolneStan> stanRepository, IRepository<OgolneStatus> statusRepository, IRepository<OgolneKategoriaPracy> kategoriaPracyRepository, IRepository<OgolneRodzajPracy> rodzajPracyRepository, IRepository<OgolneRecepturyNormWykonawczych> nazwaRecepturyRepository, IRecepturyNormWykonawczychRepository recepturyRepository, XLContext xlContext, MainContext mainContext)
            : base(adRepository, helperTextRepository, toastNotification, logger)
        {
            this.repository = repository;
            this.kategoriaPracyRepository = kategoriaPracyRepository;
            this.rodzajPracyRepository = rodzajPracyRepository;
            this.nazwaRecepturyRepository = nazwaRecepturyRepository;
            this.stanRepository = stanRepository;
            this.statusRepository = statusRepository;
            this.recepturyRepository = recepturyRepository;
            this.xlContext = xlContext;
            this.praceZleconeDokumentyModel = xlContext.Set<PraceZleconeDokumenty>();
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

                this.ViewBag.Title = "Dodanie wpisu do formularza " + Name;
                this.ViewBag.Odpowiedzialny = new SelectList(this.adRepository.GetAllADUserAcronyms());

                this.ViewBag.Dzial = new SelectList(this.xlContext.Database.SqlQuery<string>($"SELECT * FROM UDBS_Slownik.dbo.ZatrudnieniDzialVocabulary").ToList() ?? new List<string>());
                this.ViewBag.Dokument = new SelectList(this.praceZleconeDokumentyModel.OrderBy(x => x.Dokument).Select(x => x.Dokument).Distinct().ToList() ?? new List<string?>());
                this.ViewBag.KategoriaPracy = new SelectList(this.kategoriaPracyRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                this.ViewBag.RodzajPracy = new SelectList(this.rodzajPracyRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                this.ViewBag.NazwaReceptury = new SelectList(this.nazwaRecepturyRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.NazwaReceptury).Select(x => x.NazwaReceptury).ToList() ?? new List<string?>());
                var result = this.adRepository.GetAllADUserAcronyms()?.ToList();
                result?.Add("Inny");
                this.ViewBag.AkronimWyk = new SelectList(result);

                if (id != null)
                {
                    this.ViewBag.Title = "Edycja wpisu formularza " + Name;
                    this.ViewBag.Stany = new SelectList(this.stanRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                    this.ViewBag.Statusy = new SelectList(this.statusRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                }

                return this.PartialView("Create", this.repository.GetByIdAsync(id).Result ?? new PraceZleconeForm());
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
        public IActionResult Create(PraceZleconeForm model)
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
                            PraceZleconeForm? newmodel = this.repository.GetByIdAsync(model.Id).Result ?? throw new Exception("MPK not found in db");

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
                        model.Stan = "Realizacja";
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(PraceZleconeForm model)
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
        public IActionResult Clone(Guid id)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.StatusCode(403);
                }

                this.ViewBag.Title = "Kopiowanie wpisu do formularza " + Name;
                this.ViewBag.Odpowiedzialny = new SelectList(this.adRepository.GetAllADUserAcronyms());

                this.ViewBag.Dzial = new SelectList(this.xlContext.Database.SqlQuery<string>($"SELECT * FROM UDBS_Slownik.dbo.ZatrudnieniDzialVocabulary").ToList() ?? new List<string>());
                this.ViewBag.Dokument = new SelectList(this.praceZleconeDokumentyModel.OrderBy(x => x.Dokument).Select(x => x.Dokument).Distinct().ToList() ?? new List<string?>());
                this.ViewBag.KategoriaPracy = new SelectList(this.kategoriaPracyRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                this.ViewBag.RodzajPracy = new SelectList(this.rodzajPracyRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                this.ViewBag.NazwaReceptury = new SelectList(this.nazwaRecepturyRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.NazwaReceptury).Select(x => x.NazwaReceptury).ToList() ?? new List<string?>());
                var result = this.adRepository.GetAllADUserAcronyms()?.ToList();
                result?.Add("Inny");
                this.ViewBag.AkronimWyk = new SelectList(result);
                this.ViewBag.Stany = new SelectList(this.stanRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                this.ViewBag.Statusy = new SelectList(this.statusRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());

                var oldValue = this.repository.GetByIdAsync(id).Result ?? new PraceZleconeForm();

                this.ModelState.Clear();
                var newValue = new PraceZleconeForm()
                {
                    Dzial = oldValue.Dzial,
                    IDS = oldValue.IDS,
                    Dokument = oldValue.Dokument,
                    KategoriaPracy = oldValue.KategoriaPracy,
                    RodzajPracy = oldValue.RodzajPracy,
                    NazwaReceptury = oldValue.NazwaReceptury,
                    IloscPobrana = oldValue.IloscPobrana,
                    IloscWykonana = oldValue.IloscWykonana,
                    AkronimWyk = oldValue.AkronimWyk,
                    WydanieZlecenia = oldValue.WydanieZlecenia,
                    PrzyjecieZlecenia = oldValue.PrzyjecieZlecenia,
                    CzasWykonania = oldValue.CzasWykonania,
                    WartoscPracyZleconej = oldValue.WartoscPracyZleconej,
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

        [HttpGet]
        public IActionResult ExportExcel()
        {
            try
            {
                return ExcelExporter.Export(this.repository.GetAllAsync().Result?.ToList() ?? new List<PraceZleconeForm>(), Name);
            }
            catch
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                return this.RedirectToAction(nameof(this.Index));
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

        [HttpGet]
        public JsonResult GetNRByRP(string rp)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(rp))
                {
                    IEnumerable<SelectListItem> receptura = new SelectList(this.recepturyRepository.FindNRByRPAsync(rp).Result?.Select(x => new SelectListItem { Value = x, Text = x }).ToList(), "Value", "Text");
                    return this.Json(receptura);
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
        public async Task<JsonResult> GetTimeByNR(string? nr)
        {
            try
            {
                TimeSpan? time = await this.recepturyRepository.GetTimeByNR(nr);

                return this.Json(time);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.Json(string.Empty);
            }
        }

        [HttpGet]
        public JsonResult GetFirstLastNameByAcronym(string? acronym)
        {
            try
            {
                string? firstlastname = this.GetSignedInFirstLastName(acronym);
                return this.Json(firstlastname);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.Json(string.Empty);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetKwotaZleceniaByNR(string? nr)
        {
            try
            {
                decimal? kwota = await this.recepturyRepository.GetKwotaByNR(nr);
                return this.Json(kwota);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.Json(string.Empty);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using SoftlandERP.Core.Helpers;
using SoftlandERP.Core.Repositories.Interfaces;
using SoftlandERP.Data.DB;
using SoftlandERP.Data.Entities.Forms;
using SoftlandERP.Data.Entities.Forms.Stanowisko;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Ogolne;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Stanowisko;
using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Web.Controllers;
using System.Globalization;
using static SoftlandERP.Core.Repositories.Spedycja.SekcjaTowarRepository;

namespace SoftlandERP.Web.Areas.Forms.Controllers.Stanowisko
{
    [Area("Forms")]
    public class StanowiskoOdpowiedzialnosciFormController : BaseController
    {
        public static readonly string Module = "Formularz - ";
        public static readonly string Name1 = "Stanowisko - ";
        public static readonly string Name2 = "Odpowiedzialności";
        public static readonly string Name = Name1 + Name2;
        public static readonly string ModuleName = Module + Name;

        private readonly IRepository<StanowiskoOdpowiedzialnosciForm> repository;
        private readonly IRepository<StanowiskoNazwaOdpowiedzialnosci> odpowiedzialnoscRepository;
        private readonly IRepository<OgolneStan> stanRepository;
        private readonly IRepository<OgolneStatus> statusRepository;
        private readonly XLContext xlContext;
        private readonly MainContext mainContext;

        public StanowiskoOdpowiedzialnosciFormController(IADRepository adRepository, IRepository<HelperText> helperTextRepository, IToastNotification toastNotification, ILogger<BaseController> logger, IRepository<StanowiskoOdpowiedzialnosciForm> repository, IRepository<StanowiskoNazwaOdpowiedzialnosci> odpowiedzialnoscRepository, IRepository<OgolneStan> stanRepository, IRepository<OgolneStatus> statusRepository, XLContext xlContext, MainContext mainContext)
            : base(adRepository, helperTextRepository, toastNotification, logger)
        {
            this.repository = repository;
            this.odpowiedzialnoscRepository = odpowiedzialnoscRepository;
            this.stanRepository = stanRepository;
            this.statusRepository = statusRepository;
            this.xlContext = xlContext;
            this.mainContext = mainContext;
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
                var result1 = this.adRepository.GetAllADGroups()?.SelectMany(x => x.Name).ToList();
                var result2 = this.adRepository.GetAllADGroups()?.SelectMany(x => x.Members).ToList();

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
                this.ViewBag.Interwal = new SelectList(new List<string> { "dzień", "tydzień", "miesiąc", "kwartał", "rok" });
                this.ViewBag.Dni = new SelectList(new List<string> { "o. roboczy", "p. roboczy", "robocze", "wszystkie" });
                this.ViewBag.Symbol = new SelectList(this.odpowiedzialnoscRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Typ).ToList() ?? new List<string?>());
                this.ViewBag.AkronimWyk = new SelectList(this.adRepository.GetAllADUserAcronyms());
                this.ViewBag.KluczPrzydzialu = new SelectList(new List<string> { "Działowy", "Lokalizacja" });
                this.ViewBag.Lokalizacja = new SelectList(this.xlContext.Database.SqlQuery<string>($"SELECT * FROM UDBS_Slownik.dbo.OrganizacjaLokalizacjeVocabulary").ToList() ?? new List<string>());
                var result = this.adRepository.GetAllADGroups()?.Where(x => x.Name.StartsWith("SW_"))
                                                              .Where(x => x.Name.Split('_').Length > 1)
                                                              .Select(x => x.Name.Split('_')[1])
                                                              .ToList();

                var result2 = this.adRepository.GetAllADGroups()?.Where(x => x.Name.StartsWith("S_"))
                                                                .Where(x => x.Name.Split('_').Length > 2) // Zmiana na 2, aby wziąć pod uwagę drugie wystąpienie podkreślenia
                                                                .Where(x => !x.Name.Equals("S_HANDEL_" + x.Name.Split('_')[2])) // Wyklucz "S_HANDEL"
                                                                .Select(x => x.Name.Split('_')[2]) // Zmiana na 2, aby pobrać tekst po drugim podkreśleniu
                                                                .ToList();
                var combinedResult = result.Concat(result2).OrderBy(x => x).Distinct().ToList();
                this.ViewBag.Dzial = new SelectList(combinedResult);

                if (id != null)
                {
                    this.ViewBag.Title = "Edycja wpisu formularza " + Name;
                    this.ViewBag.Stany = new SelectList(this.stanRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                    this.ViewBag.Statusy = new SelectList(this.statusRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                }

                return this.PartialView("Modals/Create", this.repository.GetByIdAsync(id).Result ?? new StanowiskoOdpowiedzialnosciForm());
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
        public IActionResult Create(StanowiskoOdpowiedzialnosciForm model)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.RedirectToAction(nameof(this.Index));
                }

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
                        StanowiskoOdpowiedzialnosciForm? newmodel = this.repository.GetByIdAsync(model.Id).Result ?? throw new Exception("MPK not found in db");

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
        public IActionResult Delete(StanowiskoOdpowiedzialnosciForm model)
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

                this.ViewBag.ModalTitle = "Kopiowanie wpisu do formularza " + Name;
                this.ViewBag.Odpowiedzialny = new SelectList(this.adRepository.GetAllADUserAcronyms());

                this.ViewBag.Interwal = new SelectList(new List<string> { "dzień", "tydzień", "miesiąc", "kwartał", "rok" });
                this.ViewBag.Dni = new SelectList(new List<string> { "o. roboczy", "p. roboczy", "robocze", "wszystkie" });
                this.ViewBag.Symbol = new SelectList(this.odpowiedzialnoscRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Typ).ToList() ?? new List<string?>());
                this.ViewBag.AkronimWyk = new SelectList(this.adRepository.GetAllADUserAcronyms());
                this.ViewBag.KluczPrzydzialu = new SelectList(new List<string> { "Działowy", "Lokalizacja" });
                this.ViewBag.Lokalizacja = new SelectList(this.xlContext.Database.SqlQuery<string>($"SELECT * FROM UDBS_Slownik.dbo.OrganizacjaLokalizacjeVocabulary").ToList() ?? new List<string>());
                var result = this.adRepository.GetAllADGroups()?.Where(x => x.Name.StartsWith("SW_"))
                                                              .Where(x => x.Name.Split('_').Length > 1)
                                                              .Select(x => x.Name.Split('_')[1])
                                                              .ToList();

                var result2 = this.adRepository.GetAllADGroups()?.Where(x => x.Name.StartsWith("S_"))
                                                                .Where(x => x.Name.Split('_').Length > 2) // Zmiana na 2, aby wziąć pod uwagę drugie wystąpienie podkreślenia
                                                                .Where(x => !x.Name.Equals("S_HANDEL_" + x.Name.Split('_')[2])) // Wyklucz "S_HANDEL"
                                                                .Select(x => x.Name.Split('_')[2]) // Zmiana na 2, aby pobrać tekst po drugim podkreśleniu
                                                                .ToList();
                var combinedResult = result.Concat(result2).OrderBy(x => x).Distinct().ToList();
                this.ViewBag.Dzial = new SelectList(combinedResult);
                this.ViewBag.Stany = new SelectList(this.stanRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                this.ViewBag.Statusy = new SelectList(this.statusRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());

                var oldValue = this.repository.GetByIdAsync(id).Result ?? new StanowiskoOdpowiedzialnosciForm();

                this.ModelState.Clear();
                var newValue = new StanowiskoOdpowiedzialnosciForm()
                {
                    DataRozpoczecia = oldValue.DataRozpoczecia,
                    DataZakonczenia = oldValue.DataZakonczenia,
                    LiczbaInterwalow = oldValue.LiczbaInterwalow,
                    Interwal = oldValue.Interwal,
                    Przesuniecie = oldValue.Przesuniecie,
                    Dni = oldValue.Dni,
                    Symbol = oldValue.Symbol,
                    KluczPrzydzialu = oldValue.KluczPrzydzialu,
                    Lokalizacja = oldValue.Lokalizacja,
                    Dzial = oldValue.Dzial,
                    AkronimWyk = oldValue.AkronimWyk,
                    Cyklicznosc = oldValue.Cyklicznosc,
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
                return ExcelExporter.Export(this.repository.GetAllAsync().Result?.ToList() ?? new List<StanowiskoOdpowiedzialnosciForm>(), Name);
            }
            catch
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                return this.RedirectToAction(nameof(this.Index));
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
        public async Task<JsonResult> GetNazwaBySymbol(string? symbol)
        {
            try
            {
                var nazwa = await this.mainContext.StanowiskoNazwaOdpowiedzialnosciVocabulary
                    .Where(x => (x.Typ ?? string.Empty) == (symbol ?? string.Empty))
                    .Select(x => x.Wartosc)
                    .FirstOrDefaultAsync();

                return this.Json(nazwa);
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

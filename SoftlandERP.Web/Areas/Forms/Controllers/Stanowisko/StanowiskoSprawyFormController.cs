using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using SoftlandERP.Core.Helpers;
using SoftlandERP.Core.Repositories;
using SoftlandERP.Core.Repositories.Interfaces;
using SoftlandERP.Data.DB;
using SoftlandERP.Data.Entities.Forms;
using SoftlandERP.Data.Entities.Forms.Stanowisko;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Dokumenty;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Ogolne;
using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Web.Controllers;

namespace SoftlandERP.Web.Areas.Forms.Controllers.Stanowisko
{
    [Area("Forms")]
    public class StanowiskoSprawyFormController : BaseController
    {
        public static readonly string Module = "Formularz - ";
        public static readonly string Name1 = "Stanowisko - ";
        public static readonly string Name2 = "Sprawy";
        public static readonly string Name = Name1 + Name2;
        public static readonly string ModuleName = Module + Name;

        private readonly IRepository<StanowiskoSprawyForm> repository;
        private readonly IRepository<OgolneStan> stanRepository;
        private readonly IRepository<OgolneStatus> statusRepository;
        private readonly IRepository<OgolneRola> rolaRepository;
        private readonly XLContext xlContext;

        public StanowiskoSprawyFormController(IADRepository adRepository, IRepository<HelperText> helperTextRepository, IToastNotification toastNotification, ILogger<BaseController> logger, IRepository<StanowiskoSprawyForm> repository, IRepository<OgolneStan> stanRepository, IRepository<OgolneStatus> statusRepository, IRepository<OgolneRola> rolaRepository, XLContext xlContext)
            : base(adRepository, helperTextRepository, toastNotification, logger)
        {
            this.repository = repository;
            this.stanRepository = stanRepository;
            this.statusRepository = statusRepository;
            this.rolaRepository = rolaRepository;
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

                this.ViewBag.Rola = new SelectList(this.rolaRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                var result = this.xlContext.Database.SqlQuery<string>($"SELECT DISTINCT * FROM UDBS_Slownik.dbo.KontrahentAkronimVocabulary").ToList() ?? new List<string>();
                result?.Add(item: string.Empty);
                this.ViewBag.Weryfikujacy = new SelectList(result);

                if (id != null)
                {
                    this.ViewBag.Title = "Edycja wpisu formularza " + Name;
                    this.ViewBag.Stany = new SelectList(this.stanRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                    this.ViewBag.Statusy = new SelectList(this.statusRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                }

                return this.PartialView("Modals/Create", this.repository.GetByIdAsync(id).Result ?? new StanowiskoSprawyForm());
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
        public IActionResult Create(StanowiskoSprawyForm model)
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
                            StanowiskoSprawyForm? newmodel = this.repository.GetByIdAsync(model.Id).Result ?? throw new Exception("Karta not found in db");

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
        public IActionResult Clone(Guid id)
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

                this.ViewBag.Rola = new SelectList(this.rolaRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                var result = this.xlContext.Database.SqlQuery<string>($"SELECT DISTINCT * FROM UDBS_Slownik.dbo.KontrahentAkronimVocabulary").ToList() ?? new List<string>();
                result?.Add(item: string.Empty);
                this.ViewBag.Weryfikujacy = new SelectList(result);

                var oldValue = this.repository.GetByIdAsync(id).Result ?? new StanowiskoSprawyForm();

                this.ModelState.Clear();
                var newValue = new StanowiskoSprawyForm()
                {
                    Rola = oldValue.Rola,
                    IDS = oldValue.IDS,
                    Odpowiedzialny = oldValue.Odpowiedzialny,
                    Weryfikujacy = oldValue.Weryfikujacy,
                    Kategoria = oldValue.Kategoria,
                    Temat = oldValue.Temat,
                    Opis = oldValue.Opis,
                    DataRozpoczecia = oldValue.DataRozpoczecia,
                    TerminWykonania = oldValue.TerminWykonania,
                    Realizacja = oldValue.Realizacja,
                    CzasPracy = oldValue.CzasPracy,
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
        public IActionResult Delete(StanowiskoSprawyForm model)
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
                return ExcelExporter.Export(this.repository.GetAllAsync().Result?.ToList() ?? new List<StanowiskoSprawyForm>(), Name);
            }
            catch
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                return this.RedirectToAction(nameof(this.Index));
            }
        }
    }
}

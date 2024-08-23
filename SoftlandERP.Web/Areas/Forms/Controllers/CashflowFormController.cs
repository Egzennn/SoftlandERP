using System.Globalization;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using SoftlandERP.Core.Helpers;
using SoftlandERP.Core.Repositories.Interfaces;
using SoftlandERP.Data.DB;
using SoftlandERP.Data.Entities.Forms;
using SoftlandERP.Data.Entities.Views;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Ogolne;
using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Web.Controllers;

namespace SoftlandERP.Web.Areas.Forms.Controllers
{
    [Area("Forms")]
    public class CashflowFormController : BaseController
    {
        public static readonly string Module = "Formularz - ";
        public static readonly string Name = "Cashflow";
        public static readonly string ModuleName = Module + Name;

        private readonly IRepository<CashflowForm> repository;
        private readonly IRepository<OgolneStan> stanRepository;
        private readonly XLContext xlContext;
        private readonly MainContext mainContext;
        private readonly DbSet<Cashflow> cashflow;

        public CashflowFormController(IADRepository adRepository, IRepository<HelperText> helperTextRepository, IToastNotification toastNotification, ILogger<BaseController> logger, IRepository<CashflowForm> repository, IRepository<OgolneStan> stanRepository, XLContext xlContext, MainContext mainContext)
            : base(adRepository, helperTextRepository, toastNotification, logger)
        {
            this.repository = repository;
            this.stanRepository = stanRepository;
            this.xlContext = xlContext;
            this.mainContext = mainContext;
            this.cashflow = xlContext.Set<Cashflow>();
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

                //if (this.ViewBag.IsIT)
                //{
                var filteredResult = result.Where(x => x.Stan != "Archiwalny");

                return this.View(filteredResult);
                //}
                //else
                //{
                //    var filteredResult = result.Where(x => x.CreatedBy == this.ViewBag.DisplayName).Where(x => x.Stan != "Archiwalny");

                //    return this.View(filteredResult);
                //}
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.Redirect("~/");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            this.ViewBag.IsIT = this.adRepository.CheckGroup(this.User?.Identity?.Name, new List<string> { "S_ADM_IT" });
            this.ViewBag.IsBKR = this.adRepository.CheckSAM(this.User?.Identity?.Name, "BKR");

            try
            {
                if (this.ViewBag.IsIT || this.ViewBag.IsBKR)
                {
                    this.ViewBag.ModalTitle = "Dodanie wpisu do formularza " + Name;

                    this.ViewBag.Cashflow = this.cashflow.ToList();

                    return this.PartialView("Modals/Create");
                }
                else
                {
                    return this.View();
                }
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
        public async Task<IActionResult> Create(Guid? id)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.RedirectToAction(nameof(this.Index));
                }

                bool status = true;
                int count = 0;
                int countN = 0; // Initialize countN
                int countZ = 0;
                string lastErrorDocument = string.Empty;

                foreach (var cashflow in this.cashflow)
                {
                    CashflowForm model = new()
                    {
                        Id = Guid.NewGuid(),
                        Created = DateTime.Now,
                        CreatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name),
                        Stan = "Aktywny",
                        Strona = cashflow.Strona,
                        Dokument = cashflow.Dokument,
                        DokumentObcy = cashflow.DokumentObcy,
                        Akronim = cashflow.Akronim,
                        Termin = cashflow.Termin,
                        Kwota = cashflow.Kwota,
                        Pozostalo = cashflow.Pozostalo,
                        Waluta = cashflow.Waluta
                    };

                    if (!await this.repository.InsertAsync(model))
                    {
                        status = false;
                        lastErrorDocument = cashflow.Dokument.ToString();
                        break;
                    }

                    if (cashflow.Strona == "Należność") // Check if the value is "Należność"
                    {
                        countN++; // Increment countN
                    }
                    else
                    {
                        countZ++;
                    }

                    count++;
                }

                if (!status)
                {
                    this.toastNotification.AddErrorToastMessage("Błąd przy próbie dodawania wpisu " + lastErrorDocument);
                }

                this.toastNotification.AddSuccessToastMessage($"Powodzenie! Dodano {count} wpisów w tym {countN} Należności i {countZ} Zaległości");

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
        public IActionResult Edit(Guid? id)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.StatusCode(403);
                }

                this.ViewBag.ModalTitle = "Dodawanie wpisu formularza " + Name;

                this.ViewBag.TypDokumentu = new SelectList(new[] { new { Value = "??", Text = "??" }, new { Value = "??", Text = "??" } }, "Value", "Text");
                this.ViewBag.Currency = new SelectList(CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(ci => new RegionInfo(ci.Name).ISOCurrencySymbol).Distinct().OrderBy(s => s), "PLN");
                this.ViewBag.Akronimy = new SelectList(this.xlContext.Database.SqlQuery<string>($"SELECT DISTINCT * FROM UDBS_Slownik.dbo.KontrahentAkronimVocabulary").ToList() ?? new List<string>());
                this.ViewBag.Strona = new SelectList(new[] { new { Value = "Należność", Text = "Należność" }, new { Value = "Proforma", Text = "Proforma" }, new { Value = "Zobowiązanie", Text = "Zobowiązanie" } }, "Value", "Text");
                this.ViewBag.Stany = new SelectList(this.stanRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());

                if (id != null)
                {
                    this.ViewBag.Title = "Edycja wpisu formularza " + Name;
                }

                return this.PartialView("Modals/Edit", this.repository.GetByIdAsync(id).Result ?? new CashflowForm());
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
        public IActionResult Edit(CashflowForm model)
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
                                this.toastNotification.AddSuccessToastMessage("Powodzenie. Rekord został zmodyfikowany");
                            }
                            else
                            {
                                this.toastNotification.AddErrorToastMessage("Bład przy próbie modyfikacji rekordu");
                            }
                        }
                        else
                        {
                            CashflowForm? newmodel = this.repository.GetByIdAsync(model.Id).Result ?? throw new Exception("Cashflow not found in db");

                            newmodel.Id = Guid.NewGuid();
                            newmodel.Updated = DateTime.Now;
                            newmodel.UpdatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name);
                            newmodel.Stan = "Archiwalny";

                            if (this.repository.UpdateAsync(model).Result && this.repository.InsertAsync(newmodel).Result)
                            {
                                this.toastNotification.AddSuccessToastMessage("Powodzenie. Rekord został zmodyfikowany");
                            }
                            else
                            {
                                this.toastNotification.AddErrorToastMessage("Bład przy próbie modyfikacji rekordu");
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
                            this.toastNotification.AddSuccessToastMessage("Powodzenie. Rekord został dodany");
                        }
                        else
                        {
                            this.toastNotification.AddErrorToastMessage("Bład przy próbie dodawania rekordu");
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
        public IActionResult Delete(CashflowForm model)
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

                this.ViewBag.Currency = new SelectList(CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(ci => new RegionInfo(ci.Name).ISOCurrencySymbol).Distinct().OrderBy(s => s), "PLN");
                this.ViewBag.Akronimy = new SelectList(this.xlContext.Database.SqlQuery<string>($"SELECT DISTINCT * FROM UDBS_Slownik.dbo.KontrahentAkronimVocabulary").ToList() ?? new List<string>());
                this.ViewBag.Strona = new SelectList(new[] { new { Value = "Należność", Text = "Należność" }, new { Value = "Zobowiązanie", Text = "Zobowiązanie" } }, "Value", "Text");
                this.ViewBag.Stany = new SelectList(this.stanRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());

                var oldValue = this.repository.GetByIdAsync(id).Result ?? new CashflowForm();

                this.ModelState.Clear();
                var newValue = new CashflowForm()
                {
                    Strona = oldValue.Strona,
                    Dokument = oldValue.Dokument,
                    DokumentObcy = oldValue.DokumentObcy,
                    Akronim = oldValue.Akronim,
                    Termin = oldValue.Termin,
                    Kwota = oldValue.Kwota,
                    Pozostalo = oldValue.Pozostalo,
                    Waluta = oldValue.Waluta,
                    Priorytet = oldValue.Priorytet,
                    Uwagi = oldValue.Uwagi,
                    CreatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name),
                    Stan = oldValue.Stan,
                    Status = oldValue.Status
                };

                return this.PartialView("Modals/Edit", newValue);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.StatusCode(500);
            }
        }

        public IActionResult ChangeStan(Guid id)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.StatusCode(403);
                }

                this.ViewBag.ModalTitle = "Potwierdzenie";

                return this.PartialView("Modals/ChangeStan", this.repository.GetByIdAsync(id).Result);
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
        public IActionResult ChangeStan(CashflowForm model)
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
                        CashflowForm? newmodel = this.repository.GetByIdAsync(model.Id).Result ?? throw new Exception("Cashflow not found in db");

                        newmodel.Updated = DateTime.Now;
                        newmodel.UpdatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name);
                        newmodel.Stan = "Archiwalny";

                        if (this.repository.UpdateAsync(model).Result)
                        {
                            this.toastNotification.AddSuccessToastMessage("Powodzenie. Rekord został zmodyfikowany");
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
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.RedirectToAction(nameof(this.Index));
            }
        }

        [HttpGet]
        public IActionResult ExportExcel()
        {
            try
            {
                return ExcelExporter.Export(this.repository.GetAllAsync().Result?.ToList() ?? new List<CashflowForm>(), Name);
            }
            catch
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                return this.RedirectToAction(nameof(this.Index));
            }
        }

        public async Task<ActionResult> UpdateStanByDate()
        {
            try
            {
                int count = 0;

                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.RedirectToAction(nameof(this.Index));
                }

                var signedInUserDisplayName = this.GetSignedInDisplayName(this.User?.Identity?.Name);
                var values = this.repository.GetAllAsync().Result?
                    .Where(x => x.CreatedBy == signedInUserDisplayName)
                    .Where(x => x.Created != DateTime.Now)
                    .Where(x => x.Stan == "Aktywny");

                if (values?.Any() == true)
                {
                    foreach (var value in values)
                    {
                        value.Updated = DateTime.Now;
                        value.UpdatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name);
                        value.Stan = "Archiwalny";
                        await this.repository.UpdateAsync(value);
                        count++;
                    }

                    this.toastNotification.AddSuccessToastMessage($"Powodzenie. Ostatnio dodane dane Cashflow ({count}) zostały zmienione na Archiwalne");
                }
                else
                {
                    this.toastNotification.AddErrorToastMessage("Bład przy próbie modyfikacji rekordów Cashflow");
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
    }
}

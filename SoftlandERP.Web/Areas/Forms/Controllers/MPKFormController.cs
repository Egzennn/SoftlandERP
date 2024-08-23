using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using SoftlandERP.Core.Helpers;
using SoftlandERP.Core.Repositories.Interfaces;
using SoftlandERP.Data.DB;
using SoftlandERP.Data.Entities.Forms;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Dokumenty;
using SoftlandERP.Data.Entities.Vocabularies.Forms.Ogolne;
using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Web.Controllers;

namespace SoftlandERP.Web.Areas.Forms.Controllers
{
    [Area("Forms")]
    public class MPKFormController : BaseController
    {
        public static readonly string Module = "Formularz - ";
        public static readonly string Name = "MPK";
        public static readonly string ModuleName = Module + Name;

        private readonly IRepository<MPKForm> repository;
        private readonly IRepository<DokumentyKosztObslugi> kosztObslugiRepository;
        private readonly IRepository<DokumentyRodzajKosztu> rodzajKosztuRepository;
        private readonly IRepository<OgolneStan> stanRepository;
        private readonly IRepository<OgolneStatus> statusRepository;
        private readonly XLContext xlContext;

        public MPKFormController(IADRepository adRepository, IRepository<HelperText> helperTextRepository, IToastNotification toastNotification, ILogger<BaseController> logger, IRepository<MPKForm> repository, IRepository<DokumentyKosztObslugi> kosztObslugiRepository, IRepository<DokumentyRodzajKosztu> rodzajKosztuRepository, IRepository<OgolneStan> stanRepository, IRepository<OgolneStatus> statusRepository, XLContext xlContext)
            : base(adRepository, helperTextRepository, toastNotification, logger)
        {
            this.repository = repository;
            this.kosztObslugiRepository = kosztObslugiRepository;
            this.rodzajKosztuRepository = rodzajKosztuRepository;
            this.stanRepository = stanRepository;
            this.statusRepository = statusRepository;
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
                this.ViewBag.Stany = new SelectList(this.stanRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());

                var result = this.repository.GetAllAsync().Result;
                var t = this.adRepository.GetAllADGroupsByUser(this.GetSignedInDisplayName(this.User?.Identity?.Name));

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

        //[HttpGet]
        //public IActionResult Create(Guid? id)
        //{
        //    try
        //    {
        //        //if (!this.CheckPermission(ModuleName))
        //        //{
        //        //    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
        //        //    return this.StatusCode(403);
        //        //}

        //        this.ViewBag.Title = "Dodanie wpisu do formularza " + Name;
        //        this.ViewBag.Odpowiedzialny = new SelectList(this.adRepository.GetAllADUserAcronyms());

        //        this.ViewBag.Currency = new SelectList(CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(ci => new RegionInfo(ci.Name).ISOCurrencySymbol).Distinct().OrderBy(s => s), "PLN");
        //        this.ViewBag.RodzajKosztu = new SelectList(this.rodzajKosztuRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
        //        this.ViewBag.KosztObslugi = new SelectList(this.kosztObslugiRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
        //        var akronimy = this.xlContext.Database.SqlQuery<string>($"SELECT DISTINCT * FROM UDBS_Slownik.dbo.KontrahentAkronimVocabulary").ToList();
        //        akronimy?.Add("RW");
        //        this.ViewBag.Akronimy = new SelectList(akronimy);
        //        this.ViewBag.LastFiveMPKs = this.repository.GetAllAsync().Result?.Where(m => m.Stan == "Aktywny").Where(m => m.CreatedBy == this.GetSignedInDisplayName(this.User?.Identity?.Name)).OrderByDescending(m => m.Data).Take(5).ToList();

        //        if (id != null)
        //        {
        //            this.ViewBag.Title = "Edycja wpisu formularza " + Name;
        //            this.ViewBag.Stany = new SelectList(this.stanRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
        //            this.ViewBag.Statusy = new SelectList(this.statusRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
        //        }

        //        return this.PartialView("Create", this.repository.GetByIdAsync(id).Result ?? new MPKForm());
        //    }
        //    catch (Exception ex)
        //    {
        //        this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
        //        LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
        //        return this.StatusCode(500);
        //    }
        //}

        [HttpGet]
        public IActionResult Clone(Guid id)
        {
            try
            {
                //if (!this.CheckPermission(ModuleName))
                //{
                //    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                //    return this.StatusCode(403);
                //}

                this.ViewBag.ModalTitle = "Kopiowanie wpisu do formularza " + Name;
                this.ViewBag.Odpowiedzialny = new SelectList(this.adRepository.GetAllADUserAcronyms());

                this.ViewBag.Currency = new SelectList(CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(ci => new RegionInfo(ci.Name).ISOCurrencySymbol).Distinct().OrderBy(s => s), "PLN");
                this.ViewBag.RodzajKosztu = new SelectList(this.rodzajKosztuRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                this.ViewBag.KosztObslugi = new SelectList(this.kosztObslugiRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                var akronimy = this.xlContext.Database.SqlQuery<string>($"SELECT DISTINCT * FROM UDBS_Slownik.dbo.KontrahentAkronimVocabulary").ToList();
                akronimy?.Add("RW");
                this.ViewBag.Akronimy = new SelectList(akronimy);
                this.ViewBag.Stany = new SelectList(this.stanRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                this.ViewBag.Statusy = new SelectList(this.statusRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                this.ViewBag.LastFiveMPKs = this.repository.GetAllAsync().Result?.Where(m => m.Stan == "Aktywny").Where(m => m.CreatedBy == this.GetSignedInDisplayName(this.User?.Identity?.Name)).OrderByDescending(m => m.Data).Take(5).ToList();

                var oldValue = this.repository.GetByIdAsync(id).Result ?? new MPKForm();

                this.ModelState.Clear();
                var newValue = new MPKForm()
                {
                    MPKList = oldValue.MPKList,
                    IDK = oldValue.IDK,
                    Data = oldValue.Data,
                    RodzajKosztu = oldValue.RodzajKosztu,
                    IDS = oldValue.IDS,
                    NrDokumentu = oldValue.NrDokumentu,
                    Kwota = oldValue.Kwota,
                    Waluta = oldValue.Waluta,
                    Opis = oldValue.Opis,
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(MPKForm model)
        //{
        //    try
        //    {
        //        //if (!this.CheckPermission(ModuleName))
        //        //{
        //        //    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
        //        //    return this.RedirectToAction(nameof(this.Index));
        //        //}

        //        if (this.repository.GetByIdAsync(model.Id).Result != null)
        //        {
        //            model.Updated = DateTime.Now;
        //            model.UpdatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name);
        //            if (model.Stan == "Archiwalny")
        //            {
        //                if (this.repository.UpdateAsync(model).Result)
        //                {
        //                    this.toastNotification.AddSuccessToastMessage("Powodzenie. Wpis został zmodyfikowany");
        //                }
        //                else
        //                {
        //                    this.toastNotification.AddErrorToastMessage("Bład przy próbie modyfikacji wpisu");
        //                }
        //            }
        //            else
        //            {
        //                MPKForm? newmodel = this.repository.GetByIdAsync(model.Id).Result ?? throw new Exception("MPK not found in db");

        //                newmodel.Id = Guid.NewGuid();
        //                newmodel.Updated = DateTime.Now;
        //                newmodel.UpdatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name);
        //                newmodel.Stan = "Archiwalny";

        //                if (this.repository.UpdateAsync(model).Result && this.repository.InsertAsync(newmodel).Result)
        //                {
        //                    this.toastNotification.AddSuccessToastMessage("Powodzenie. Wpis został zmodyfikowany");
        //                }
        //                else
        //                {
        //                    this.toastNotification.AddErrorToastMessage("Bład przy próbie modyfikacji wpisu");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            model.CreatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name);
        //            model.Stan = "Aktywny";
        //            model.Status = "Akceptacja";

        //            if (this.repository.InsertAsync(model).Result)
        //            {
        //                this.toastNotification.AddSuccessToastMessage("Powodzenie. Wpis został dodany");
        //            }
        //            else
        //            {
        //                this.toastNotification.AddErrorToastMessage("Bład przy próbie dodawania wpisu");
        //            }
        //        }

        //        return this.RedirectToAction(nameof(this.Index));
        //    }
        //    catch (Exception ex)
        //    {
        //        this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
        //        LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
        //        return this.RedirectToAction(nameof(this.Index));
        //    }
        //}

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

                this.ViewBag.Currency = new SelectList(CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(ci => new RegionInfo(ci.Name).ISOCurrencySymbol).Distinct().OrderBy(s => s), "PLN");
                this.ViewBag.RodzajKosztu = new SelectList(this.rodzajKosztuRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                this.ViewBag.KosztObslugi = new SelectList(this.kosztObslugiRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                var akronimy = this.xlContext.Database.SqlQuery<string>($"SELECT DISTINCT * FROM UDBS_Slownik.dbo.KontrahentAkronimVocabulary").ToList();
                akronimy?.Add("RW");
                this.ViewBag.Akronimy = new SelectList(akronimy);

                if (id != null)
                {
                    this.ViewBag.Title = "Edycja wpisu formularza " + Name;
                    this.ViewBag.Stany = new SelectList(this.stanRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                    this.ViewBag.Statusy = new SelectList(this.statusRepository.GetAllAsync().Result?.Where(x => x.Stan == "Aktywny").OrderBy(x => x.Wartosc).Select(x => x.Wartosc).ToList() ?? new List<string?>());
                }

                return this.PartialView("Modals/Edit", this.repository.GetByIdAsync(id).Result ?? new MPKForm());
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
        public async Task<IActionResult> Edit(MPKForm model)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return RedirectToAction(nameof(Index));
                }

                var existingModel = await this.repository.GetByIdAsync(model.Id);

                if (existingModel != null)
                {
                    model.Updated = DateTime.Now;
                    model.UpdatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name);

                    if (model.Stan == "Archiwalny")
                    {
                        var updateResult = await this.repository.UpdateAsync(model);
                        if (updateResult)
                        {
                            this.toastNotification.AddSuccessToastMessage("Powodzenie. Wpis został zmodyfikowany");
                        }
                        else
                        {
                            this.toastNotification.AddErrorToastMessage("Błąd przy próbie modyfikacji wpisu");
                        }
                    }
                    else
                    {
                        // Tworzymy nową instancję na bazie istniejącego modelu
                        var newModel = new MPKForm
                        {
                            Id = Guid.NewGuid(),
                            Updated = DateTime.Now,
                            UpdatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name),
                            Stan = "Archiwalny",
                            // Skopiuj inne potrzebne właściwości z existingModel do newModel
                        };

                        // Update obecnego modelu i dodanie nowego
                        var updateResult = await this.repository.UpdateAsync(model);
                        var insertResult = await this.repository.InsertAsync(newModel);

                        if (updateResult && insertResult)
                        {
                            this.toastNotification.AddSuccessToastMessage("Powodzenie. Wpis został zmodyfikowany");
                        }
                        else
                        {
                            this.toastNotification.AddErrorToastMessage("Błąd przy próbie modyfikacji wpisu");
                        }
                    }
                }
                else
                {
                    // Nowy wpis
                    model.CreatedBy = this.GetSignedInDisplayName(this.User?.Identity?.Name);
                    model.Stan = "Aktywny";
                    model.Status = "Akceptacja";

                    var insertResult = await this.repository.InsertAsync(model);
                    if (insertResult)
                    {
                        this.toastNotification.AddSuccessToastMessage("Powodzenie. Wpis został dodany");
                    }
                    else
                    {
                        this.toastNotification.AddErrorToastMessage("Błąd przy próbie dodawania wpisu");
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z bazą danych");
                this.logger.LogError("ERROR: {Message}", ex.Message);
                return RedirectToAction(nameof(Index));
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
        public IActionResult Delete(MPKForm model)
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
                return ExcelExporter.Export(this.repository.GetAllAsync().Result?.ToList() ?? new List<MPKForm>(), Name);
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
    }
}
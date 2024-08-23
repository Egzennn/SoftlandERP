using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NToastNotify;
using SoftlandERP.Core.Helpers;
using SoftlandERP.Core.Repositories.Interfaces;
using SoftlandERP.Data.Configurations;
using SoftlandERP.Data.DB;
using SoftlandERP.Data.Entities.Staff.AD;
using SoftlandERP.Data.Entities.Vocabularies.General;
using SoftlandERP.Data.Entities.Vocabularies.Staff;
using SoftlandERP.Web.Areas.Staff.Models.AD;
using SoftlandERP.Web.Controllers;

namespace SoftlandERP.Web.Areas.Staff.Controllers
{
    [Area("Staff")]
    public class ADController : BaseController
    {
        public static readonly string ModuleName = "Użytkownicy domeny SOFTLAND20";

        private readonly ADConfiguration adConfiguration;
        private readonly OptimaContext optimaContext;
        private readonly IUserAddressRepository userAddressVocabularyRepository;
        private readonly IRepository<DownloadableForm> downloadFormVocabularyRepository;
        private readonly IRepository<ApplicationPermissionsRule> permissionsRepository;

        public ADController(IADRepository adRepository, IRepository<HelperText> helperTextVocabularyRepository, IToastNotification toastNotification, ILogger<BaseController> logger, IOptions<ADConfiguration> adConfiguration, OptimaContext optimaContext, IUserAddressRepository userAddressVocabularyRepository, IRepository<DownloadableForm> downloadFormVocabularyRepository, IRepository<ApplicationPermissionsRule> permissionsRepository)
            : base(adRepository, helperTextVocabularyRepository, toastNotification, logger)
        {
            this.adConfiguration = adConfiguration.Value;
            this.optimaContext = optimaContext;
            this.userAddressVocabularyRepository = userAddressVocabularyRepository;
            this.downloadFormVocabularyRepository = downloadFormVocabularyRepository;
            this.permissionsRepository = permissionsRepository;
        }

        [HttpGet]
        public IActionResult Users()
        {
            try
            {
                this.ViewBag.Title = "Użytkownicy domeny SOFTLAND20";
                this.ViewBag.IsAdmin = this.CheckPermission(ModuleName);

                var result = this.adRepository.GetAllADUsers();

                if (result == null)
                {
                    return this.StatusCode(500);
                }

                return this.View(result);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z domeną");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.Redirect("~/");
            }
        }

        [HttpGet]
        public IActionResult CreateUser(string option, Guid? id)
        {
            try
            {
                CreateUserModel model = new () { UserAddresess = this.userAddressVocabularyRepository.GetAllAsync().Result ?? new List<UserAddress>(), HelperTexts = this.helperTextRepository.GetAllAsync().Result ?? new List<HelperText>() };

                if (option == "show")
                {
                    if (id == null)
                    {
                        this.toastNotification.AddErrorToastMessage("Błąd przy wyświetleniu konta użytownika");
                        this.RedirectToAction(nameof(this.Users));
                    }

                    this.ViewBag.Title = "Podgłąd konta użytkownika";
                    this.ViewBag.IsShow = true;
                    model.ADUser = this.adRepository.GetADUsersById(id ?? default) ?? throw new Exception("User not found");
                }
                else
                {
                    if (!this.CheckPermission(ModuleName))
                    {
                        this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                        return this.StatusCode(403);
                    }

                    this.ViewBag.Logins = this.adRepository.GetAllADUserLogins();
                    this.ViewBag.IsShow = false;

                    if (id == null)
                    {
                        this.ViewBag.Title = "Dodawanie nowego konta użytkownika";

                        model.ADUser = new ADUser { Address = new UserAddress() };
                    }
                    else
                    {
                        this.ViewBag.Title = "Modyfikacja konta użytkownika";

                        model.ADUser = this.adRepository.GetADUsersById(id.Value) ?? throw new ArgumentNullException("ADUser is null", nameof(model.ADUser));
                    }
                }

                return this.View(model);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z domeną");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.RedirectToAction(nameof(this.Users));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(ADUser adUser)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.RedirectToAction(nameof(this.Users));
                }

                if (this.adRepository.GetADUsersById(adUser?.Id) != null)
                {
                    if (adUser == null)
                    {
                        throw new Exception("User creation error");
                    }

                    adUser.Address = this.userAddressVocabularyRepository.FindAddressByCountryAsync(adUser.Address?.Country, adUser.Address?.City, adUser.Address?.Street).Result ?? new UserAddress();

                    if (this.adRepository.UpdateUser(adUser))
                    {
                        this.toastNotification.AddSuccessToastMessage("Powodzenie. Konto użytkownika zostało zmodyfikowane");
                    }
                    else
                    {
                        this.toastNotification.AddErrorToastMessage("Bład przy próbie modyfikacji konta");
                    }
                }
                else
                {
                    if (adUser == null)
                    {
                        throw new Exception("User creation error");
                    }

                    adUser.Address = this.userAddressVocabularyRepository.FindAddressByCountryAsync(adUser.Address?.Country, adUser.Address?.City, adUser.Address?.Street).Result ?? new UserAddress();

                    if (this.adRepository.CreateUser(adUser))
                    {
                        this.toastNotification.AddSuccessToastMessage("Powodzenie. Konto użytkownika zostało zapisane");
                    }
                    else
                    {
                        this.toastNotification.AddErrorToastMessage("Bład przy próbie zapisania konta");
                    }
                }

                return this.RedirectToAction(nameof(this.Users));
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z domeną");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.RedirectToAction(nameof(this.Users));
            }
        }

        [HttpGet]
        public IActionResult ResetPassword(Guid id)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.StatusCode(403);
                }

                var user = this.adRepository.GetADUsersById(id);

                this.ViewBag.ModalTitle = "Reset hasła";
                this.ViewBag.ModalText = "Czy na pewno chcesz zresetować hasło dla użytkownika: " + user?.FirstName + " " + user?.LastName + " na standardowe?";
                this.ViewBag.DefaultPassword = "Hasło standardowe: " + this.adConfiguration.DefaultPassword;

                return this.PartialView("Modals/ResetPassword", user);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z domeną");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.StatusCode(500);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(ADUser adUser)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.RedirectToAction(nameof(this.Users));
                }

                if (this.adRepository.ResetPassword(adUser))
                {
                    this.toastNotification.AddSuccessToastMessage("Powodzenie. Hasło zostało zresetowane");
                }
                else
                {
                    this.toastNotification.AddErrorToastMessage("Bład podczas resetowania hasła");
                }

                return this.RedirectToAction(nameof(this.Users));
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z domeną");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.RedirectToAction(nameof(this.Users));
            }
        }

        [HttpGet]
        public IActionResult ChangeStatus(Guid id)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.StatusCode(403);
                }

                var user = this.adRepository.GetADUsersById(id) ?? throw new Exception("User change status error");

                if (user.Enabled)
                {
                    this.ViewBag.ModalTitle = "Dezaktywacja konta " + user.FirstName + " " + user.LastName;
                    this.ViewBag.ModalText = "Czy na pewno chcesz dezaktywować konto dla użytkownika: " + user.FirstName + " " + user.LastName + "?";
                }
                else
                {
                    this.ViewBag.ModalTitle = "Aktywacja konta " + user.FirstName + " " + user.LastName;
                    this.ViewBag.ModalText = "Czy na pewno chcesz aktywować konto dla użytkownika: " + user.FirstName + " " + user.LastName + "?";
                }

                return this.PartialView("Modals/ChangeStatus", user);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z domeną");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.StatusCode(500);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeStatus(ADUser adUser)
        {
            try
            {
                if (!this.CheckPermission(ModuleName))
                {
                    this.toastNotification.AddErrorToastMessage("Brak uprawnień administracyjnych");
                    return this.RedirectToAction(nameof(this.Users));
                }

                bool status = this.adRepository.ChangeStatus(adUser);

                if (adUser?.Enabled == true && status)
                {
                    this.toastNotification.AddSuccessToastMessage("Powodzenie. Konto użytkownika zostało dezaktywowane");
                }
                else if (adUser?.Enabled == false && status)
                {
                    this.toastNotification.AddSuccessToastMessage("Powodzenie. Konto użytkownika zostało aktywowane");
                }
                else
                {
                    this.toastNotification.AddErrorToastMessage("Bład podczas zmiany statusu dla konta");
                }

                return this.RedirectToAction(nameof(this.Users));
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z domeną");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.RedirectToAction(nameof(this.Users));
            }
        }

        [HttpGet]
        public IActionResult Forms()
        {
            try
            {
                this.ViewBag.ModalTitle = "Dodatkowe formularze";
                this.ViewBag.ModalText = "W celu zgłoszenia zmian nałeży wypełnić odpowiedni formularz, umieścić go na sieci w odpowiednim miejscu i wysłać w postaci linku do pliku do przyłożonego i osoby odpowiedzialnej.";

                var model = new List<FormsModel>();
                var adUserForms = this.downloadFormVocabularyRepository.GetAllAsync().Result ?? throw new Exception("Foms get error");

                foreach (var form in adUserForms)
                {
                    if (!Directory.Exists("wwwroot/files"))
                    {
                        Directory.CreateDirectory("wwwroot/files");
                    }

                    if (System.IO.File.Exists(form.Path))
                    {
                        string fileName = Path.GetFileName(form.Path).Replace(" ", "_", StringComparison.InvariantCulture);
                        System.IO.File.Copy(form.Path, "wwwroot/files/" + fileName, true);
                        model.Add(new FormsModel { Category = form.Category ?? string.Empty, Path = string.Format(@"files\{0}", fileName), Name = fileName.Replace("_", " ", StringComparison.InvariantCulture) });
                    }
                }

                return this.PartialView("Modals/Forms", model);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z domeną");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult ExportExcel()
        {
            try
            {
                return ExcelExporter.Export(this.adRepository.GetAllADUsers()?.ToList() ?? new List<ADUser>(), ModuleName);
            }
            catch
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z domeną");
                return this.RedirectToAction(nameof(this.Users));
            }
        }

        [HttpGet]
        public bool CheckLogin([Bind(Prefix = "ADUser.Login")] string login)
        {
            try
            {
                return this.adRepository.CheckLogin(login);
            }
            catch
            {
                return false;
            }
        }

        [HttpGet]
        public JsonResult GetInformationFromOptima(string login)
        {
            List<string> optimaInformation = new ();

            try
            {
                List<string> userDepartments = new ();

                var userCode = this.optimaContext.Database.SqlQuery<string>($"SELECT [PRI_Kod] AS Value FROM [CDN].[Pracidx] WHERE [PRI_Opis] = {login}").FirstOrDefault() ?? string.Empty;
                var userDepartmentCode = this.optimaContext.Database.SqlQuery<string>($"SELECT [PRI_AdresDzialu] AS Value FROM [CDN].[Pracidx] WHERE [PRI_Opis] = {login}").FirstOrDefault()?.Replace(" ", string.Empty, StringComparison.InvariantCulture) ?? string.Empty;

                while (!string.IsNullOrEmpty(userDepartmentCode))
                {
                    userDepartments.Add(this.optimaContext.Database.SqlQuery<string>($"SELECT [DZL_Nazwa] AS Value FROM [CDN].[Dzialy] WHERE REPLACE(DZL_AdresWezla, ' ', '') = {userDepartmentCode}").FirstOrDefault() ?? string.Empty);

                    if (userDepartmentCode == "1.7" || userDepartmentCode == "1.8")
                    {
                        break;
                    }

                    userDepartmentCode = userDepartmentCode.Length >= 2 ? userDepartmentCode.Remove(userDepartmentCode.Length - 2) : userDepartmentCode.Remove(userDepartmentCode.Length - 1);
                }

                var userJobTitleCode = this.optimaContext.Database.SqlQuery<int?>($"SELECT TOP 1 [PRE_ETADkmIdStanowisko] AS Value FROM [CDN].[PracEtaty] WHERE [PRE_Kod] = {userCode} Order By [PRE_DataOd] DESC").FirstOrDefault();
                var userJobTitle = this.optimaContext.Database.SqlQuery<string>($"SELECT [DKM_Nazwa] AS Value FROM [CDN_SOFTLAND_2011].[CDN].[DaneKadMod] WHERE [DKM_DkmId] = {userJobTitleCode}").FirstOrDefault();

                optimaInformation.Add(userDepartments.Last());
                userDepartments.Remove(userDepartments.Last());
                userDepartments.Reverse();

                optimaInformation.Add(string.Join(" - ", userDepartments).Replace("Wydział ", string.Empty, StringComparison.InvariantCulture));
                optimaInformation.Add(userJobTitle ?? string.Empty);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Błąd. Nie znałeziono akronimu: " + login);
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
            }

            return this.Json(optimaInformation);
        }

        [HttpGet]
        public IActionResult Groups()
        {
            try
            {
                this.ViewBag.Title = "Grupy zabezpieczeń domeny SOFTLAND20";

                var result = this.adRepository.GetAllADGroups();

                if (result == null)
                {
                    return this.StatusCode(500);
                }

                var model = new GroupsViewModel
                {
                    Organizations = result.Where(x => x.Name.StartsWith("O_")).ToList(),
                    Departments = result.Where(x => x.Name.StartsWith("SW_")).ToList(),
                    SubDepartments = result.Where(x => x.Name.StartsWith("S_")).ToList()
                };

                return this.View(model);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z domeną");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.Redirect("~/");
            }
        }

        [HttpGet]
        public IActionResult Permissions()
        {
            try
            {
                this.ViewBag.Title = "Uprawnienia do modułów";

                var groups = this.adRepository.GetAllADGroupsByUser(this.GetSignedInDisplayName(this.User?.Identity?.Name));
                var permissions = this.permissionsRepository.GetAllAsync().Result;

                var model = new PermissionsViewModel();

                if (permissions != null)
                {
                    model.FormularzPermissions = permissions
                        .Where(x => groups.Contains(x.ADGroupDisplayName) && x.Module.StartsWith("Formularz"))
                        .OrderBy(x => x.Module)
                        .Select(x => x.Module.Substring(x.Module.IndexOf('-') + 1))
                        .Distinct()
                        .ToList();

                    model.SlownikPermissions = permissions
                        .Where(x => groups.Contains(x.ADGroupDisplayName) && x.Module.StartsWith("Słownik"))
                        .OrderBy(x => x.Module)
                        .Select(x => x.Module.Substring(x.Module.IndexOf('-') + 1))
                        .Distinct()
                        .ToList();

                    model.InnePermissions = permissions
                        .Where(x => !(!groups.Contains(x.ADGroupDisplayName) || x.Module.StartsWith("Formularz") || x.Module.StartsWith("Słownik")))
                        .OrderBy(x => x.Module)
                        .Select(x => x.Module)
                        .Distinct()
                        .ToList();
                }
                else
                {
                    model.FormularzPermissions = new List<string?>();
                    model.SlownikPermissions = new List<string?>();
                    model.InnePermissions = new List<string?>();
                }

                return this.View(model);
            }
            catch (Exception ex)
            {
                this.toastNotification.AddErrorToastMessage("Brak połączenia z domeną");
                LoggerExtensions.LogError(this.logger, "ERROR: {Message}", ex.Message);
                return this.Redirect("~/");
            }
        }
    }
}
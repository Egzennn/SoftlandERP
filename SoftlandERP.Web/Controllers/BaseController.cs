using System.Net;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SoftlandERP.Core.Helpers;
using SoftlandERP.Core.Repositories.Interfaces;
using SoftlandERP.Data.Entities.Vocabularies.General;

namespace SoftlandERP.Web.Controllers
{
    [ERPAuthorize("SOFTLAND20\\O_PL")]
    public class BaseController : Controller
    {
        protected readonly IADRepository adRepository;
        protected readonly IRepository<HelperText> helperTextRepository;
        protected readonly IToastNotification toastNotification;
        protected readonly ILogger<BaseController> logger;

        public BaseController(IADRepository adRepository, IRepository<HelperText> helperTextRepository, IToastNotification toastNotification, ILogger<BaseController> logger)
            : base()
        {
            this.adRepository = adRepository;
            this.helperTextRepository = helperTextRepository;
            this.toastNotification = toastNotification;
            this.logger = logger;
        }

        public static void ClearCookiesOnExit()
        {
            // Tworzenie kontenera na pliki cookie
            CookieContainer cookieContainer = new ();

            // Tutaj możesz dodać kod do obsługi plików cookie w trakcie działania aplikacji
            // Na przykład, możesz dodać nowe pliki cookie lub pobierać istniejące.

            // Teraz, po zakończeniu aplikacji, możesz wyczyścić wszystkie pliki cookie
            var cookies = cookieContainer.GetCookies(new Uri("http://erp.softland20.pl/"));

            foreach (Cookie cookie in cookies)
            {
                cookie.Expired = true;
            }
        }

        protected bool CheckPermission(string module)
        {
#if DEBUG
            return true;
#else
            ClearCookiesOnExit();
            return this.adRepository.CheckMembership(this.User?.Identity?.Name ?? string.Empty, module);
#endif
        }

        protected string GetSignedInDisplayName(string? username)
        {
            if (username == null)
            {
                return string.Empty;
            }

            return this.adRepository.GetUserAcronym(username);
        }

        protected string GetSignedInFirstLastName(string? username)
        {
            if (username == null)
            {
                return string.Empty;
            }

            return this.adRepository.GetUserFirstLastName(username);
        }
    }
}
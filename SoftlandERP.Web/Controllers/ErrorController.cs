using Microsoft.AspNetCore.Mvc;

namespace SoftlandERP.Web.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        [Route("/Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            this.ViewBag.Title = "Błąd";
            this.ViewBag.ErrorCode = statusCode;

            this.ViewBag.ErrorDescription = statusCode switch
            {
                403 => "Błąd autoryzacji. Aplikacja jest dostępna tylko i wyłącznie z domeny SOFTLAND20",
                500 => "Błąd aplikacji. Skontaktuj się z administratorem",
                404 => "Strona której szukasz nie istnieje",
                _ => "Nie znany błąd. Skontaktuj się z administratorem",
            };
            return this.View();
        }

        public void Exception()
        {
            this.Index(500);
        }
    }
}
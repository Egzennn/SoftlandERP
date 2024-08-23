using Microsoft.AspNetCore.Mvc.Rendering;

namespace SoftlandERP.Core.Helpers
{
    public static class HtmlHelpers
    {
        public static string IsActive(this IHtmlHelper htmlHelper, string controller, string action)
        {
            return controller == htmlHelper?.ViewContext?.RouteData?.Values["controller"]?.ToString() && action == htmlHelper?.ViewContext?.RouteData?.Values["action"]?.ToString() ? "nav-active" : string.Empty;
        }

        public static string IsActiveParentArea(this IHtmlHelper htmlHelper, string area)
        {
            return area == htmlHelper?.ViewContext?.RouteData?.Values["area"]?.ToString() ? "nav-expanded nav-active" : string.Empty;
        }

        public static string IsActiveParentController(this IHtmlHelper htmlHelper, string controller)
        {
            return controller == htmlHelper?.ViewContext?.RouteData?.Values["controller"]?.ToString() ? "nav-expanded nav-active" : string.Empty;
        }
    }
}
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace SoftlandERP.Core.Helpers
{
    public static class ModulesList
    {
        public static IEnumerable<string?> GetMolulesName()
        {
            Assembly? assembly = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName?.Contains("SoftlandERP.Web") == true)?.FirstOrDefault();
            if (assembly == null)
            {
                return Enumerable.Empty<string>();
            }

            return assembly.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type)).Select(type => type.GetField("ModuleName")?.GetValue(null)?.ToString()).Where(x => x != null).ToList();
        }
    }
}
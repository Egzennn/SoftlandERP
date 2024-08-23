using Microsoft.AspNetCore.Authorization;

namespace SoftlandERP.Core.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public sealed class ERPAuthorize : AuthorizeAttribute
    {
        public ERPAuthorize(string roles)
        {
#if !DEBUG
            this.Roles = roles;
#endif
        }
    }
}
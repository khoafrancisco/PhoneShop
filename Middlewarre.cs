using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuthorizeAdminAttribute : TypeFilterAttribute
{
    public AuthorizeAdminAttribute() : base(typeof(AuthorizeAdminFilter)) { }

    private class AuthorizeAdminFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity?.IsAuthenticated ?? true)
            {
                // Redirect to Admin login if not authenticated
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "area", "Admin" },
                    { "controller", "User" },
                    { "action", "Login" }
                });
            }
            else if (!context.HttpContext.User.IsInRole("Admin"))
            {
                // Redirect to Access Denied if not an admin
                context.Result = new ForbidResult("AdminCookie");
            }
        }
    }
}

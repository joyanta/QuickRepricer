using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QuickRepricer.Core.Models;
using System;

namespace QuickRepricer.Filters
{
    /// <summary>
    /// in Startup.cs services.AddSingleton<AuthorizeSubscriber>() to be added
    /// Not used at prosent;
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class AuthorizeSubscriber : ActionFilterAttribute, IAuthorizationFilter
    {
        private UserManager<ApplicationUser> _userManager;

        public AuthorizeSubscriber(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = _userManager.FindByNameAsync(context.HttpContext.User.Identity.Name).Result;

            if (user == null || DateTime.Now > user.ActiveUntil)
            {
                //context.Result = new UnauthorizedResult();
                context.Result = new RedirectToActionResult("Index", "Subscription", null);
            }
        }
    }
}

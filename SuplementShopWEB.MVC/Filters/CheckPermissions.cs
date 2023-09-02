using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace SuplementShopWEB.MVC.Filters
{
    public class CheckPermissions : Attribute, IAuthorizationFilter
    { // In progress
        private readonly string _permission;
        public CheckPermissions(string permission)
        {
            _permission= permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuthorized = CheckUserPermision(context.HttpContext.User, _permission);
        }

        private bool CheckUserPermision(ClaimsPrincipal user, string permission)
        {
            throw new NotImplementedException();
        }
    }
}

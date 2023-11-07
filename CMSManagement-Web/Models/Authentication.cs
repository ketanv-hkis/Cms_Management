using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace CMSManagement_Web.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class Authentication : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext actionContext)
        {
            string token = actionContext.HttpContext.Session.GetString("Token");

            var handler = new JwtSecurityTokenHandler();
            if (token != null)
            {
                var JWTtoken = handler.ReadToken(token);
                if (JWTtoken != null)
                {
                    actionContext.HttpContext.Response.Headers.Add("AuthorizeToken", token);
                    actionContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");
                    actionContext.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");
                    return;
                }
                else
                {
                    actionContext.Result = new RedirectToActionResult("Login", "Home", null);
                }
            }
            else
            {
                actionContext.Result = new RedirectToActionResult("Login", "Home", null);
            }
        }
    }
}

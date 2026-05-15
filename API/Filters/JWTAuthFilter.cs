using BLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace API.Filters
{
    public class JWTAuthFilter:AuthorizationFilterAttribute
    {
        private readonly string[] allowedRoles;
        public JWTAuthFilter(params string[] roles)
        {
            allowedRoles = roles;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader == null || authHeader.Scheme != "Bearer")
            {
                Deny(actionContext, "Authorization header missing or invalid.");
                return;
            }

            string token = authHeader.Parameter;
            var principal = JWTHelper.ValidateAccessToken(token);

            if(principal == null)
            {
                Deny(actionContext, "Invalid or expired token.");
                return;
            }

            if(allowedRoles !=null && allowedRoles.Length > 0)
            {
                var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
                if (roleClaim == null || !allowedRoles.Contains(roleClaim))
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(
                        HttpStatusCode.Forbidden, "You do not have permission.");
                    return;
                }
            }
            actionContext.RequestContext.Principal = principal;
        }

        private void Deny(HttpActionContext context, string message)
        {
            context.Response = context.Request.CreateErrorResponse(
                HttpStatusCode.Unauthorized, message);
        }
    }
}
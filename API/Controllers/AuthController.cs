using API.Filters;
using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login (LoginDTO obj)
        {
            try
            {
                if (obj==null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data.");
                var result = AuthService.Login(obj);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
        }

        [HttpPost]
        [Route("refresh")]
        public HttpResponseMessage Refresh(RefreshTokenDTO obj)
        {
            try
            {
                if (obj == null || string.IsNullOrWhiteSpace(obj.RefreshToken))
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Refresh token is required.");

                var result = AuthService.RefreshAccessToken(obj.RefreshToken);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
        }

        [HttpPost]
        [Route("logout")]
        [JWTAuthFilter]
        public HttpResponseMessage Logout()
        {
            try
            {
                var principal = RequestContext.Principal as ClaimsPrincipal;

                if (principal == null)
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid token.");

                var userIdClaim = principal.Claims
                    .FirstOrDefault(c => c.Type == "userId");

                if (userIdClaim == null)
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid token.");

                int userId = int.Parse(userIdClaim.Value);
                AuthService.Logout(userId);

                return Request.CreateResponse(HttpStatusCode.OK, "Logged out successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("change-password")]
        [JWTAuthFilter]
        public HttpResponseMessage ChangePassword(ChangePasswordDTO obj)
        {
            try
            {
                if (obj == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Data is required.");

                bool result = AuthService.ChangePassword(obj);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to change password.");

                return Request.CreateResponse(HttpStatusCode.OK, "Password changed successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("assign-role")]
        [JWTAuthFilter("Admin")]
        public HttpResponseMessage AssignRole(AssignRoleDTO obj)
        {
            try
            {
                if (obj == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Data is required.");

                bool result = AuthService.AssignRole(obj);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to assign role.");

                return Request.CreateResponse(HttpStatusCode.OK, "Role assigned successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

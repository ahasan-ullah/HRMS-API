using API.Filters;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace API.Controllers
{
    [RoutePrefix("api/user")]
    [JWTAuthFilter("Admin")]
    public class UserController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = UserService.GetAll();
                if (data == null || data.Count == 0)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No users found.");

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                var data = UserService.GetById(id);
                if (data == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not found.");

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("by-username/{username}")]
        public HttpResponseMessage GetByUsername(string username)
        {
            try
            {
                var data = UserService.GetByUsername(username);
                if (data == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not found.");

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("toggle-active/{id:int}")]
        public HttpResponseMessage ToggleActive(int id)
        {
            try
            {
                bool result = UserService.ToggleActive(id);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to update user status.");

                return Request.CreateResponse(HttpStatusCode.OK, "User status updated successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

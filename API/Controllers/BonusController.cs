using API.Filters;
using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/bonus")]
    [JWTAuthFilter("Admin", "HR")]
    public class BonusController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = BonusService.GetAll();
                if (data == null || data.Count == 0)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No bonuses found.");

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
                var data = BonusService.Get(id);
                if (data == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Bonus not found.");

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("employee/{employeeId:int}")]
        public HttpResponseMessage GetByEmployee(int employeeId)
        {
            try
            {
                var data = BonusService.GetByEmployeeId(employeeId);
                if (data == null || data.Count == 0)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No bonuses found.");

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(BonusDTO obj)
        {
            try
            {
                if (obj == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bonus data is required.");

                bool result = BonusService.Create(obj);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to create bonus.");

                return Request.CreateResponse(HttpStatusCode.Created, "Bonus created successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update(BonusDTO obj)
        {
            try
            {
                if (obj == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bonus data is required.");
                bool result = BonusService.Update(obj);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to update bonus.");

                return Request.CreateResponse(HttpStatusCode.OK, "Bonus updated successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                bool result = BonusService.Delete(id);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to delete bonus.");

                return Request.CreateResponse(HttpStatusCode.OK, "Bonus deleted successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

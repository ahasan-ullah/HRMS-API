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
    [RoutePrefix("api/salary")]
    [JWTAuthFilter("Admin", "HR")]
    public class SalaryController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = SalaryService.GetAll();
                if (data == null || data.Count == 0)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No salary records found.");

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
                var data = SalaryService.GetById(id);
                if (data == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Salary record not found.");

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("employee/{employeeId:int}")]
        public HttpResponseMessage GetActiveByEmployee(int employeeId)
        {
            try
            {
                var data = SalaryService.GetActiveSalaryByEmployeeId(employeeId);
                if (data == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No active salary found.");

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("revisions/{employeeId:int}")]
        public HttpResponseMessage GetRevisionHistory(int employeeId)
        {
            try
            {
                var data = SalaryService.GetSalaryRevisionsByEmployeeId(employeeId);
                if (data == null || data.Count == 0)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No revision history found.");

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(SalaryDTO obj)
        {
            try
            {
                if (obj == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Salary data is required.");

                bool result = SalaryService.Create(obj);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to create salary.");

                return Request.CreateResponse(HttpStatusCode.Created, "Salary created successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update(SalaryDTO dto)
        {
            try
            {
                if (dto == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Salary data is required.");

                bool result = SalaryService.Update(dto);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to update salary.");

                return Request.CreateResponse(HttpStatusCode.OK, "Salary updated successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("revise")]
        public HttpResponseMessage Revise(SalaryRevisionDTO obj)
        {
            try
            {
                if (obj == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Revision data is required.");

                bool result = SalaryService.ReviseSalary(obj);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to revise salary.");

                return Request.CreateResponse(HttpStatusCode.OK, "Salary revised successfully.");
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
                bool result = SalaryService.Delete(id);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to delete salary.");

                return Request.CreateResponse(HttpStatusCode.OK, "Salary deleted successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

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
    [RoutePrefix("api/deduction")]
    [JWTAuthFilter("Admin", "HR")]
    public class DeductionController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = DeductionService.GetAll();
                if(data==null && data.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No deductions found.");
                }
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
                var data = DeductionService.GetById(id);
                if (data == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Deduction not found.");

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
                var data = DeductionService.GetByEmployeeId(employeeId);
                if (data == null || data.Count == 0)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No deductions found.");

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("employee/{employeeId:int}/{month:int}/{year:int}")]
        public HttpResponseMessage GetByEmployeeAndPeriod(int employeeId, int month, int year)
        {
            try
            {
                var data = DeductionService.GetByEmployeeAndPeriod(employeeId, month, year);
                if (data == null || data.Count == 0)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No deductions found.");

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(DeductionDTO obj)
        {
            try
            {
                if (obj == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Deduction data is required.");

                bool result = DeductionService.Create(obj);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to create deduction.");

                return Request.CreateResponse(HttpStatusCode.Created, "Deduction created successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update(DeductionDTO dto)
        {
            try
            {
                if (dto == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Deduction data is required.");

                bool result = DeductionService.Update(dto);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to update deduction.");

                return Request.CreateResponse(HttpStatusCode.OK, "Deduction updated successfully.");
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
                bool result = DeductionService.Delete(id);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to delete deduction.");

                return Request.CreateResponse(HttpStatusCode.OK, "Deduction deleted successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

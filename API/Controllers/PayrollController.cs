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
    [RoutePrefix("api/payroll")]
    [JWTAuthFilter("Admin", "HR")]
    public class PayrollController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = PayrollService.GetAll();
                if (data == null || data.Count == 0)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No payrolls found.");

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
                var data = PayrollService.Get(id);
                if (data == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Payroll not found.");

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}/details")]
        public HttpResponseMessage GetDetails(int id)
        {
            try
            {
                var data = PayrollService.GetDetails(id);
                if (data == null || data.Count == 0)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No payroll details found.");

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("generate")]
        public HttpResponseMessage Generate(GeneratePayrollDTO obj)
        {
            try
            {
                bool result = PayrollService.Generate(obj.Month, obj.Year, obj.GeneratedBy);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to generate payroll.");

                return Request.CreateResponse(HttpStatusCode.Created, "Payroll generated successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("approve/{id:int}")]
        [JWTAuthFilter("Admin")]
        public HttpResponseMessage Approve(int id)
        {
            try
            {
                bool result = PayrollService.Approve(id);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to approve payroll.");

                return Request.CreateResponse(HttpStatusCode.OK, "Payroll approved successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("markpaid/{id:int}")]
        [JWTAuthFilter("Admin")]
        public HttpResponseMessage MarkAsPaid(int id)
        {
            try
            {
                bool result = PayrollService.MarkAsPaid(id);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to mark payroll as paid.");

                return Request.CreateResponse(HttpStatusCode.OK, "Payroll marked as paid.");
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
                bool result = PayrollService.Delete(id);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to delete payroll.");

                return Request.CreateResponse(HttpStatusCode.OK, "Payroll deleted successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

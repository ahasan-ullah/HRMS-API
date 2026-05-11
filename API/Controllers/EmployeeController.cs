using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace API.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var employees = EmployeeService.GetAll();
                if (employees == null || employees.Count == 0)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No employees found.");

                return Request.CreateResponse(HttpStatusCode.OK, employees);
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
                var employee = EmployeeService.GetById(id);
                if (employee == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee not found.");
                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("search")]
        public HttpResponseMessage Search(string keyword="")
        {
            try
            {
                var employees = EmployeeService.Search(keyword);
                if (employees == null || employees.Count == 0)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No employees found.");

                return Request.CreateResponse(HttpStatusCode.OK, employees);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("filter/department/{departmentId:int}")]
        public HttpResponseMessage FilterByDepartment(int departmentId)
        {
            try
            {
                var employees = EmployeeService.FilterByDepartment(departmentId);
                if (employees == null || employees.Count == 0)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No employees found.");

                return Request.CreateResponse(HttpStatusCode.OK, employees);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(EmployeeDTO obj)
        {
            try
            {
                if (obj == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Employee data is required.");

                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                bool result = EmployeeService.Create(obj);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to create employee.");

                return Request.CreateResponse(HttpStatusCode.Created, "Employee created successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update(EmployeeDTO obj)
        {
            try
            {
                if (obj == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Employee data is required.");

                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                bool result = EmployeeService.Update(obj);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to update employee.");

                return Request.CreateResponse(HttpStatusCode.OK, "Employee updated successfully.");
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
                bool result = EmployeeService.Delete(id);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to delete employee.");

                return Request.CreateResponse(HttpStatusCode.OK, "Employee deleted successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

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
    [RoutePrefix("api/department")]
    [JWTAuthFilter("Admin", "HR")]
    public class DepartmentController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var departments = DepartmentService.GetAll();
                if (departments == null || departments.Count == 0)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No departments found.");

                return Request.CreateResponse(HttpStatusCode.OK, departments);
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
                var department = DepartmentService.GetById(id);
                if (department == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Department not found.");

                return Request.CreateResponse(HttpStatusCode.OK, department);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(DepartmentDTO dto)
        {
            try
            {
                if (dto == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Department data is required.");

                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                bool result = DepartmentService.Create(dto);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to create department.");

                return Request.CreateResponse(HttpStatusCode.Created, "Department created successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update(DepartmentDTO dto)
        {
            try
            {
                if (dto == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Department data is required.");

                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                bool result = DepartmentService.Update(dto);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to update department.");

                return Request.CreateResponse(HttpStatusCode.OK, "Department updated successfully.");
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
                bool result = DepartmentService.Delete(id);
                if (!result)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to delete department.");

                return Request.CreateResponse(HttpStatusCode.OK, "Department deleted successfully.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

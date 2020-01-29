using Jal.Monads;
using Beauty.Barry.Application;
using Beauty.Barry.Application.Dto.Department;
using Microsoft.AspNetCore.Mvc;
using System;
using Beauty.Barry.Domain.Department.Specifications;
using Microsoft.AspNetCore.JsonPatch;
using Beauty.Dick.Helpers.Impl;

namespace Beauty.Barry.Api.Controllers
{
    [Route("api/departments")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentApplication _departmentApplication;

        public DepartmentController(IDepartmentApplication departmentApplication)
        {
            _departmentApplication = departmentApplication;
        }

        [HttpGet]
        [Route("departmentId")]
        public IActionResult Get(Guid departmentId)
        {
            return _departmentApplication.Fetch(new FetchDepartmentByIdSpec(departmentId))
                .Return(result => base.Ok(result), (string[] errors) => base.StatusCode(500, ErrorMessageHelper.BuildSystemErrorMessage(errors)));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return _departmentApplication.FetchAll(new FetchDepartmentAllSpec("Active"))
                .Return(result => base.Ok(result), (string[] errors) => base.StatusCode(500, ErrorMessageHelper.BuildSystemErrorMessage(errors)));
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] DepartmentCreateDto departmentDto)
        {
            return _departmentApplication.Create(departmentDto)
                .Return(result => Ok(result), (string[] errors) => StatusCode(500, ErrorMessageHelper.BuildSystemErrorMessage(errors)));
        }

        [HttpPatch]
        public IActionResult Patch(Guid departmentId, [FromBody] JsonPatchDocument<DepartmentUpdateDto> patchDocument)
        {
            return _departmentApplication.Patch(departmentId, patchDocument)
                    .Return(result => Ok(result), (string[] errors) => StatusCode(500, ErrorMessageHelper.BuildSystemErrorMessage(errors)));
        }

        [HttpPut]
        public IActionResult Put(Guid departmentId, [FromBody] DepartmentUpdateDto department)
        {
            return _departmentApplication.Update(departmentId, department)
                    .Return(result => Ok(result), (string[] errors) => StatusCode(500, ErrorMessageHelper.BuildSystemErrorMessage(errors)));
        }

        [HttpDelete]
        [Route("departmentId")]
        public IActionResult Delete(Guid departmentId)
        {
            return _departmentApplication.Delete(new DeleteDepartmentSpec(departmentId))
                    .Return(result => Ok(result), (string[] errors) => StatusCode(500, ErrorMessageHelper.BuildSystemErrorMessage(errors)));
        }
    }
}

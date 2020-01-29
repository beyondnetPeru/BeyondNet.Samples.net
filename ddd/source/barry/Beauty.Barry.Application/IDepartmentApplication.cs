using Beauty.Barry.Application.Dto.Department;
using Beauty.Barry.Domain.Department.Specifications;
using Beauty.Dick.Domain.Model;
using Jal.Monads;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;

namespace Beauty.Barry.Application
{
    public interface IDepartmentApplication
    {
        Result<DepartmentDto> Fetch(FetchDepartmentByIdSpec spec);
        Result<IEnumerable<DepartmentDto>> FetchAll(FetchDepartmentAllSpec spec);
        Result<bool> Exists(ExistsDepartmentSpec spec);
        Result<ApplicationResult<DepartmentDto>> Create(DepartmentCreateDto departmentDto);        
        Result<ApplicationResult<DepartmentDto>> Patch(Guid departmentId, JsonPatchDocument<DepartmentUpdateDto> operation);
        Result<ApplicationResult<DepartmentDto>> Update(Guid departmentId, DepartmentUpdateDto departmentDto);
        Result<ApplicationResult<DepartmentDto>> Delete(DeleteDepartmentSpec spec);
    }
}

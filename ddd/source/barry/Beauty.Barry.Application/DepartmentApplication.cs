using AutoMapper;
using Beauty.Barry.Application.Dto.Department;
using Beauty.Barry.Domain.Department;
using Beauty.Barry.Domain.Department.Specifications;
using Beauty.Dick.Domain.Impl;
using Beauty.Dick.Domain.Model;
using Beauty.Dick.Helpers.Builders.Interface;
using Common.Logging;
using Jal.Monads;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;

namespace Beauty.Barry.Application
{
    public class DepartmentApplication : AbstractApplicationService, IDepartmentApplication
    {
        private readonly ICodeBuilder _codeBuilder;

        private readonly IDepartmentRepository _departmentRepository;


        public DepartmentApplication(ILog log,
                                     ICodeBuilder codeBuilder,
                                     IDepartmentRepository departmentRepository)
            : base(log)
        {
            _codeBuilder = codeBuilder;

            _departmentRepository = departmentRepository;
        }


        public Result<DepartmentDto> Fetch(FetchDepartmentByIdSpec spec)
        {
            return _departmentRepository.Fetch(spec)
                .OnSuccess(resultDepartment =>
                {
                    return Mapper.Map<DepartmentDto>(resultDepartment).ToResult();
                });
        }

        public Result<bool> Exists(ExistsDepartmentSpec spec)
        {
            return _departmentRepository.Fetch(spec)
                .OnSuccess(resultDepartment =>
                {
                    return (resultDepartment != null).ToResult();
                });
        }

        public Result<IEnumerable<DepartmentDto>> FetchAll(FetchDepartmentAllSpec spec)
        {
           return _departmentRepository.FetchAll(spec)
                .OnSuccess(resultDepartments =>
                {
                    return Mapper.Map<IEnumerable<DepartmentDto>>(resultDepartments).ToResult();
                });
        }

        public Result<ApplicationResult<DepartmentDto>> Create(DepartmentCreateDto departmentDto)
        {
            var applicationResult = new ApplicationResult<DepartmentDto>();

            _codeBuilder.Build("DE1", "DE")
            .OnSuccess(resultCode => DepartmentEdit.Create(resultCode, departmentDto.Name, departmentDto.Description))
            .OnSuccess(resultDepartment => {
               if (resultDepartment.IsValid)
               {
                    _departmentRepository.Create(resultDepartment)
                        .OnSuccess(() =>
                         {
                             var dto = Mapper.Map<DepartmentDto>(resultDepartment);

                             applicationResult.IsValid = true;
                             applicationResult.Entity = dto;
                         });
                   }
                   else
                   {
                        applicationResult.IsValid = false;
                        foreach (var error in resultDepartment.BrokenRules.Errors)
                        {
                            applicationResult.ErrorMessages.Add(new ErrorMessage { Code = error.ErrorCode, Message = error.ErrorMessage });
                        }
                   }
               });

            return applicationResult.ToResult();
        }

        public Result<ApplicationResult<DepartmentDto>> Update(Guid departmentId, DepartmentUpdateDto departmentUpdateDto)
        {
            var applicationResult = new ApplicationResult<DepartmentDto>();

            Fetch(new FetchDepartmentByIdSpec(departmentId))
                .OnSuccess(resultDepartmentDto => {

                    var editToUpdate = Mapper.Map<DepartmentEdit>(resultDepartmentDto);

                    return UpdateModel(editToUpdate, departmentUpdateDto);
                    
                });           

            return applicationResult.ToResult();
        }

        public Result<ApplicationResult<DepartmentDto>> Patch(Guid departmentId, JsonPatchDocument<DepartmentUpdateDto> patchDocument)
        {
            return Fetch(new FetchDepartmentByIdSpec(departmentId))
                    .OnSuccess(resultDepartmentDto => {
                        var editToUpdate = Mapper.Map<DepartmentEdit>(resultDepartmentDto);

                        var dtoToPatch = Mapper.Map<DepartmentUpdateDto>(resultDepartmentDto);
                        patchDocument.ApplyTo(dtoToPatch);

                        return UpdateModel(editToUpdate, dtoToPatch);
                    });
        }

        private Result<ApplicationResult<DepartmentDto>> UpdateModel(DepartmentEdit departmentEdit, DepartmentUpdateDto departmentUpdateDto) {

            var applicationResult = new ApplicationResult<DepartmentDto>();

            DepartmentEdit.Update(departmentEdit, departmentUpdateDto.Name, departmentUpdateDto.Description)
                       .OnSuccess(resultDepartment => {
                           if (resultDepartment.IsValid)
                           {
                               _departmentRepository.Update(resultDepartment)
                                   .OnSuccess(() => {
                                       var dto = Mapper.Map<DepartmentDto>(resultDepartment);

                                       applicationResult.IsValid = true;
                                       applicationResult.Entity = dto;
                                   });
                           }
                           else
                           {
                               applicationResult.IsValid = false;
                               foreach (var error in resultDepartment.BrokenRules.Errors)
                               {
                                   applicationResult.ErrorMessages.Add(new ErrorMessage { Code = error.ErrorCode, Message = error.ErrorMessage });
                               }
                           }
                       });

            return applicationResult.ToResult();
        }

        public Result<ApplicationResult<DepartmentDto>> Delete(DeleteDepartmentSpec spec)
        {
            var applicationResult = new ApplicationResult<DepartmentDto>();

            _departmentRepository.Delete(spec)
              .OnSuccess(resultDepartment => {
                  var dto = Mapper.Map<DepartmentDto>(resultDepartment);
                  applicationResult.IsValid = true;
                  applicationResult.Entity = dto;
              });

            return applicationResult.ToResult();
        }
    }
}

using Beauty.Barry.Domain.Department;
using Beauty.Dick.Domain.Interface;
using Jal.Monads;
using System;
using System.Collections.Generic;

namespace Beauty.Barry.Infrastructure.DaoEF.Department
{
    public class DepartmentDao : IDepartmentRepository
    {
        public Result<DepartmentEdit> Create(DepartmentEdit entity)
        {
            throw new NotImplementedException();
        }

        public Result<DepartmentEdit> Delete(ISpecification<DepartmentEdit> spec)
        {
            throw new NotImplementedException();
        }

        public Result<bool> Exists(ISpecification<DepartmentEdit> spec)
        {
            throw new NotImplementedException();
        }

        public Result<DepartmentEdit> Fetch(ISpecification<DepartmentEdit> spec)
        {
            throw new NotImplementedException();
        }

        public Result<IEnumerable<DepartmentEdit>> FetchAll(ISpecification<DepartmentEdit> spec)
        {
            throw new NotImplementedException();
        }

        public Result<DepartmentEdit> Update(DepartmentEdit entity)
        {
            throw new NotImplementedException();
        }
    }
}

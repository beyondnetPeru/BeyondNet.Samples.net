using Beauty.Dick.Domain.Impl.Specifications;
using System;
using System.Linq.Expressions;

namespace Beauty.Barry.Domain.Department.Specifications
{
    public class FetchDepartmentAllSpec : SpecificationBase<DepartmentEdit>
    {
        private readonly string _status;

        public FetchDepartmentAllSpec(string status)
        {
            _status = status;
        }

        public override Expression<Func<DepartmentEdit, bool>> SpecExpression
        {
            get
            {
                return x => x.Status == _status;
            }
        }
    }
}

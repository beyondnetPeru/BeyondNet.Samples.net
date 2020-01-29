using Beauty.Dick.Domain.Impl.Specifications;
using System;
using System.Linq.Expressions;

namespace Beauty.Barry.Domain.Department.Specifications
{
    public class DeleteDepartmentSpec : SpecificationBase<DepartmentEdit>
    {
        private readonly Guid _id;

        public DeleteDepartmentSpec(Guid id)
        {
            _id = id;
        }
        public override Expression<Func<DepartmentEdit, bool>> SpecExpression
        {
            get
            {
                return x => x.Id == _id;
            }
        }
    }
}

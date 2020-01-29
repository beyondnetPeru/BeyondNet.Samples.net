using Beauty.Dick.Domain.Impl.Specifications;
using System;
using System.Linq.Expressions;

namespace Beauty.Barry.Domain.Department.Specifications
{
    public class ExistsDepartmentSpec : SpecificationBase<DepartmentEdit>
    {
        private readonly string _name;

        public ExistsDepartmentSpec(string name)
        {
            _name = name;
        }

        public override Expression<Func<DepartmentEdit, bool>> SpecExpression
        {
            get
            {
                return x => x.Name.ToLower() == _name.ToLower();
            }
        }
    }
}

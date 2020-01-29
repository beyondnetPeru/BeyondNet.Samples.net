using System;
using System.Linq.Expressions;
using BeyondNet.App.Ums.Domain.Common.Impl.Specifications;

namespace BeyondNet.App.Ums.Domain.Owner.Specifications
{
    public class OwnerExistsByNameSpec : SpecificationBase<OwnerEdit>
    {
        private readonly string _name;

        public OwnerExistsByNameSpec(string name)
        {
            _name = name;
        }

        public override Expression<Func<OwnerEdit, bool>> SpecExpression
        {
            get
            {
                return user => String.Equals(user.Name, _name, StringComparison.CurrentCultureIgnoreCase);
            }
        }
    }
}

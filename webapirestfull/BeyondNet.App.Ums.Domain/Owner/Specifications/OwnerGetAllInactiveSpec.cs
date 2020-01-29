using System;
using System.Linq.Expressions;
using BeyondNet.App.Ums.Domain.Common.Impl.Specifications;

namespace BeyondNet.App.Ums.Domain.Owner.Specifications
{
    public class OwnerGetAllInactiveSpec : SpecificationBase<OwnerEdit>
    {
        public override Expression<Func<OwnerEdit, bool>> SpecExpression
        {
            get { return user => user.Status == EOwnerStatus.Inactive; }
        }
    }
}

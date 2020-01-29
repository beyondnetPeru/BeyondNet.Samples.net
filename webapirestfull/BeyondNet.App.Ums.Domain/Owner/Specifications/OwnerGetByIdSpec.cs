using System;
using System.Linq.Expressions;
using BeyondNet.App.Ums.Domain.Common.Impl.Specifications;

namespace BeyondNet.App.Ums.Domain.Owner.Specifications
{
    public class OwnerGetByIdSpec : SpecificationBase<OwnerEdit>
    {
        readonly Guid _id;

        public OwnerGetByIdSpec(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<OwnerEdit, bool>> SpecExpression
        {
            get { return user => user.Id == _id; }
        }
    }
}

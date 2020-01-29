using System;
using System.Linq.Expressions;
using BeyondNet.App.Ums.Domain.Common.Impl.Specifications;

namespace BeyondNet.App.Ums.Domain.User.Specifications
{
    public class UserGetByIdSpec : SpecificationBase<UserEdit>
    {
        readonly Guid _id;

        public UserGetByIdSpec(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<UserEdit, bool>> SpecExpression
        {
            get { return user => user.Id == _id; }
        }
    }
}

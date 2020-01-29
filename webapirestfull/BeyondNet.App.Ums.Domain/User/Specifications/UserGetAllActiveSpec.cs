using System;
using System.Linq.Expressions;
using BeyondNet.App.Ums.Domain.Common.Impl.Specifications;

namespace BeyondNet.App.Ums.Domain.User.Specifications
{
    public class UserGetAllActiveSpec : SpecificationBase<UserEdit>
    {
        public override Expression<Func<UserEdit, bool>> SpecExpression
        {
            get { return user => user.Status == 1; }
        }
    }
}

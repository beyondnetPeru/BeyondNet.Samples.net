using System;
using System.Linq.Expressions;
using BeyondNet.App.Ums.Domain.Common.Impl.Specifications;

namespace BeyondNet.App.Ums.Domain.User.Specifications
{
    public class UserGetByUserNameSpec : SpecificationBase<UserEdit>
    {
        private readonly string _userName;

        public UserGetByUserNameSpec(string userName)
        {
            _userName = userName;
        }

        public override Expression<Func<UserEdit, bool>> SpecExpression
        {
            get { return user => string.Equals(user.UserName, _userName, StringComparison.CurrentCultureIgnoreCase); }
        }
    }
}

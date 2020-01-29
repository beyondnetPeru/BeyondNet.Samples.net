using System;
using System.Linq.Expressions;
using BeyondNet.App.Ums.Domain.Common.Impl.Specifications;

namespace BeyondNet.App.Ums.Domain.User.Specifications
{
    public class UserExistsByUserNameSpec : SpecificationBase<UserEdit>
    {
        readonly string _userName;

        public UserExistsByUserNameSpec(string userName)
        {
            _userName = userName;
        }

        public override Expression<Func<UserEdit, bool>> SpecExpression
        {
            get
            {
                return user => string.Equals(user.UserName.Trim(), _userName.Trim(), StringComparison.CurrentCultureIgnoreCase);
            }
        }
    }
}

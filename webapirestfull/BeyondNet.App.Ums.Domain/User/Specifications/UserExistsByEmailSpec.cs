using System;
using System.Linq.Expressions;
using BeyondNet.App.Ums.Domain.Common.Impl.Specifications;

namespace BeyondNet.App.Ums.Domain.User.Specifications
{
    public class UserExistsByEmailSpec : SpecificationBase<UserEdit>
    {
        readonly string _email;

        public UserExistsByEmailSpec(string email)
        {
            _email = email;
        }

        public override Expression<Func<UserEdit, bool>> SpecExpression
        {
            get
            {
                return user => user.Email == _email;
            }
        }
    }
}

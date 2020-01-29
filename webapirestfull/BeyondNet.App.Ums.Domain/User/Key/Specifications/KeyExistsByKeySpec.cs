using System;
using System.Linq.Expressions;
using BeyondNet.App.Ums.Domain.Common.Impl.Specifications;

namespace BeyondNet.App.Ums.Domain.User.Key.Specifications
{
    public class KeyExistsByKeySpec : SpecificationBase<KeyEdit>
    {
        readonly KeyEdit _key;

        public KeyExistsByKeySpec(KeyEdit key)
        {
            _key = key;
        }

        public override Expression<Func<KeyEdit, bool>> SpecExpression
        {
            get { return key => key.Id == _key.Id; }
        }
    }
}

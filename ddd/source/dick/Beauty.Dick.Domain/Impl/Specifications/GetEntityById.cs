using Beauty.Dick.Domain.Interface;
using System;
using System.Linq.Expressions;

namespace Beauty.Dick.Domain.Impl.Specifications
{
    public class GetEntityByIdSpec<TOut> : SpecificationBase<TOut> where TOut : IAggregateRoot
    {
        readonly Guid _identifier;

        public GetEntityByIdSpec(Guid identifier)
        {
            _identifier = identifier;
        }

        public override Expression<Func<TOut, bool>> SpecExpression
        {
            get { return obj => obj.Id == _identifier; }
        }

    }
}

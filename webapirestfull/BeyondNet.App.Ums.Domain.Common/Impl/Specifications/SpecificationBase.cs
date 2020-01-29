using System;
using System.Linq.Expressions;
using BeyondNet.App.Ums.Domain.Common.Interface;

namespace BeyondNet.App.Ums.Domain.Common.Impl.Specifications
{
    public abstract class SpecificationBase<T> : ISpecification<T>
    {
        private Func<T, bool> _compiledExpression;
 
        private Func<T, bool> CompiledExpression => _compiledExpression ?? (_compiledExpression = SpecExpression.Compile());

        public abstract Expression<Func<T, bool>> SpecExpression { get; }

        public bool IsSatisfiedBy(T obj)
        {
            return CompiledExpression(obj);
        }
    }
}

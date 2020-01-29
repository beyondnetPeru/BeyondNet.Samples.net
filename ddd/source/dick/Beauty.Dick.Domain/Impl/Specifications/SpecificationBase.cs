using Beauty.Dick.Domain.Interface;
using Jal.Monads;
using System;
using System.Linq.Expressions;

namespace Beauty.Dick.Domain.Impl.Specifications
{
    public abstract class SpecificationBase<T> : ISpecification<T>
    {
        private Func<T, bool> _compiledExpression;

        private Func<T, bool> CompiledExpression => _compiledExpression ?? (_compiledExpression = SpecExpression.Compile());

        public abstract Expression<Func<T, bool>> SpecExpression { get; }

        Result<bool> ISpecification<T>.IsSatisfiedBy(T obj)
        {
            return CompiledExpression(obj);
        }
    }
}

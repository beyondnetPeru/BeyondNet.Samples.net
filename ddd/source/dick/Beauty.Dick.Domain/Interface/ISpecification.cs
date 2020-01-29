using Jal.Monads;
using System;
using System.Linq.Expressions;

namespace Beauty.Dick.Domain.Interface
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> SpecExpression { get; }

        Result<bool> IsSatisfiedBy(T obj);
    }
}

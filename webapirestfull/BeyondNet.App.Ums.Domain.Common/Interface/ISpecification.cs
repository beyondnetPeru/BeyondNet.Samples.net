using System;
using System.Linq.Expressions;

namespace BeyondNet.App.Ums.Domain.Common.Interface
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> SpecExpression { get; }
        bool IsSatisfiedBy(T obj);
    }
}

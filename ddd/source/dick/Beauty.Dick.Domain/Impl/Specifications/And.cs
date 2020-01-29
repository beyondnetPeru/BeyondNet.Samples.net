using Beauty.Dick.Domain.Interface;
using System;
using System.Linq.Expressions;

namespace Beauty.Dick.Domain.Impl.Specifications
{
    public class And<T> : SpecificationBase<T>
    {
        readonly ISpecification<T> _left;
        readonly ISpecification<T> _right;

        public And(
            ISpecification<T> left,
            ISpecification<T> right)
        {
            this._left = left;
            this._right = right;
        }

        // AndSpecification
        public override Expression<Func<T, bool>> SpecExpression
        {
            get
            {
                var objParam = Expression.Parameter(typeof(T), "obj");

                var newExpr = Expression.Lambda<Func<T, bool>>(
                    Expression.AndAlso(
                        Expression.Invoke(_left.SpecExpression, objParam),
                        Expression.Invoke(_right.SpecExpression, objParam)
                    ),
                    objParam
                );

                return newExpr;
            }
        }
    }
}

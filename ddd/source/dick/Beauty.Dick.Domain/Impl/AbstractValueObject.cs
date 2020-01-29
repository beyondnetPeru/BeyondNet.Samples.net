using FluentValidation.Results;
using Jal.Monads;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Dick.Domain.Impl
{
    public abstract class AbstractValueObject
    {
        #region Business Rules

        public ValidationResult BrokenRules { get; private set; }

        protected abstract Result<ValidationResult> Validate();

        public Result<ValidationResult> GetBrokenValidationRules()
        {
            return Validate();
        }

        protected void AddBrokenValidationRule(ValidationFailure businessRule)
        {
            BrokenRules.Errors.Add(businessRule);
        }

        #endregion

        #region Comparable

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(null, obj)) return false;
            if (GetType() != obj.GetType()) return false;
            var vo = obj as AbstractValueObject;
            return GetEqualityComponents().SequenceEqual(vo.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return HashCodeHelper.CombineHashCodes(GetEqualityComponents());
        }

        #endregion
    }
}

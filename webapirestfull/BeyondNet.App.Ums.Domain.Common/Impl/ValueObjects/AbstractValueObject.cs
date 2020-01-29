using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace BeyondNet.App.Ums.Domain.Common.Impl.ValueObjects
{
    public abstract class AbstractValueObject
    {
        #region Validation Rules

        private readonly ValidationResult _validationBrokenRules = new ValidationResult();

        protected void AddBrokenValidationRule(ValidationFailure businessRule)
        {
            _validationBrokenRules.Errors.Add(businessRule);
        }

        public ValidationResult GetBrokenValidationRules()
        {
            _validationBrokenRules.Errors.Clear();

            Validate();

            return _validationBrokenRules;
        }

        protected abstract void Validate();

        #endregion

        #region Equal Pattern

        /// <summary>
        /// When overriden in a derived class, returns all components of a value objects which constitute its identity.
        /// </summary>
        /// <returns>An ordered list of equality components.</returns>
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

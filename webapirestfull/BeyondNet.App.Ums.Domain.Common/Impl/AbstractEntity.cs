using System;
using BeyondNet.App.Ums.Domain.Common.Interface;
using FluentValidation.Results;

namespace BeyondNet.App.Ums.Domain.Common.Impl
{
    public abstract class AbstractEntity<TEntity> : IAggregateRoot
    {
        #region Common Properties

        public virtual Guid Id { get; set; }
        
        #endregion

        #region Tracking

        public static bool IsNew { get; private set; }
        public static bool IsDirty { get; private set; }

        public static void MarkAsNew()
        {
            if (!IsDirty)
            {
                IsNew = true;
            }
        }

        public static void MarkAsDirty()
        {
            if (!IsNew)
            {
                IsDirty = true;
            }
        }

        #endregion

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
        #endregion
    }
}

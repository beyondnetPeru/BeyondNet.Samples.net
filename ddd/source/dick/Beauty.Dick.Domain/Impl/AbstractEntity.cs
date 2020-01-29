using Beauty.Dick.Domain.Interface;
using FluentValidation.Results;
using Jal.Monads;
using System;

namespace Beauty.Dick.Domain.Impl
{
    public abstract class AbstractEntity<TEntity> : IAggregateRoot
    {
        protected AbstractEntity()
        {
            MarkAsNew();

            MarkAsValid();
        }

        #region Status Tracking

        public virtual Guid Id { get; set; }

        public bool IsNew { get; private set; }

        public void MarkAsNew()
        {
            if (!IsDirty)
            {
                IsNew = true;
            }
        }

        public bool IsDirty { get; private set; }

        public void MarkAsDirty()
        {
            if (!IsNew)
            {
                IsDirty = true;
            }
        }

        public bool IsValid { get; private set; }
        
        public void MarkAsInvalid()
        {
            IsValid = false;
        }

        public void MarkAsValid()
        {
            IsValid = true;
        }

        #endregion

        #region Business Rules

        public ValidationResult BrokenRules { get; private set; }

        protected abstract Result<ValidationResult> Validate();

        public void GetBrokenValidationRules()
        {
            Validate()
                .OnSuccess(resultBrokenRules =>
                {
                    if (!resultBrokenRules.IsValid)
                    {
                        BrokenRules = resultBrokenRules;

                        MarkAsInvalid();
                    }
                    else
                    {
                        MarkAsValid();
                    }
                });
        }
        protected void AddBrokenValidationRule(ValidationFailure businessRule)
        {
            BrokenRules.Errors.Add(businessRule);
        }

        #endregion
    }
}

using FluentValidation;

namespace BeyondNet.App.Ums.Domain.Owner.ValidationRules
{
    public class OwnerRuleValidator : AbstractValidator<OwnerEdit>
    {
        public OwnerRuleValidator()
        {
            RuleFor(owner => owner.Name).NotEmpty().NotNull();
        }
    }
}

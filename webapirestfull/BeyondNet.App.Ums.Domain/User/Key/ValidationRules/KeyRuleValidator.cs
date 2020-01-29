using FluentValidation;

namespace BeyondNet.App.Ums.Domain.User.Key.ValidationRules
{
    public class KeyRuleValidator : AbstractValidator<KeyEdit>
    {
        public KeyRuleValidator()
        {
            RuleFor(key => key.User).NotNull();
            RuleFor(key => key.Password).NotNull().NotEmpty();
        }
    }
}

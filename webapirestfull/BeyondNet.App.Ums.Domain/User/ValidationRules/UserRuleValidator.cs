using FluentValidation;

namespace BeyondNet.App.Ums.Domain.User.ValidationRules
{
    public class UserRuleValidator : AbstractValidator<UserEdit>
    {
        public UserRuleValidator()
        {
            RuleFor(user => user.UserName).NotEmpty().NotNull();
            RuleFor(user => user.FullName).NotEmpty().NotNull();
            RuleFor(user => user.Email).NotEmpty().NotNull().EmailAddress();
        }
    }
}

using FluentValidation;
using FluentValidation.Results;

namespace BeyondNet.Demo.Jal.AppConsole
{
    public class FooValidator : AbstractValidator<FooClass>
    {
        public FooValidator()
        {
            RuleFor(x => x.Foo1).NotEmpty();
            RuleFor(x => x.Foo2).NotEmpty();
        }
    }

    public class FooClass
    {
        private ValidationResult _brokenRules = new ValidationResult();

        public string Foo1 { get; set; }
        public string Foo2 { get; set; }
    }
}

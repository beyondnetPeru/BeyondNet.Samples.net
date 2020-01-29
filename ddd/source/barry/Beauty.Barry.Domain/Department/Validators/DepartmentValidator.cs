using FluentValidation;

namespace Beauty.Barry.Domain.Department.Validators
{
    public class DepartmentValidator : AbstractValidator<DepartmentEdit>
    {
        public DepartmentValidator()
        {
            RuleFor(department => department.Name).NotNull();
        }
    }
}

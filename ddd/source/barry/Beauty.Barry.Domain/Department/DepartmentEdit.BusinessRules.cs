using Beauty.Barry.Domain.Department.Validators;
using FluentValidation.Results;
using Jal.Monads;

namespace Beauty.Barry.Domain.Department
{
    public partial class DepartmentEdit
    {
        protected override Result<ValidationResult> Validate()
        {
            return new DepartmentValidator().Validate(this);
        }
    }
}

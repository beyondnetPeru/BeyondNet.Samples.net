using Beauty.Dick.Domain.Impl;
using Beauty.Dick.Helpers.Domain.Impl;
using Jal.Monads;
using System;

namespace Beauty.Barry.Domain.Department
{
    public partial class DepartmentEdit : AbstractEntity<DepartmentEdit>
    {
        public string Code { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Status { get; private set; }

        public Audit Audit { get; private set; }


        public static Result<DepartmentEdit> Create(string code, string name, string description)
        {
            return Create(Guid.NewGuid(), code, name, description, "Active");
        }

        public static Result<DepartmentEdit> Create(Guid id, string code, string name, string description, string status)
        {
            var department = new DepartmentEdit()
            {
                Id = id,
                Code = code,
                Name = name,
                Description = description,
                Status = status,
                Audit = Audit.Create("UserName", Environment.MachineName, DateTime.UtcNow),
            };

            department.GetBrokenValidationRules();

            return department.ToResult();               
        }

        public static Result<DepartmentEdit> Update(DepartmentEdit departmentEdit, string name, string description)
        {
            departmentEdit.Name = name;
            departmentEdit.Description = description;

            Audit.Update(departmentEdit.Audit, "UserName", Environment.MachineName, DateTime.UtcNow);

            departmentEdit.GetBrokenValidationRules();

            return departmentEdit.ToResult();
        }
    }
}

using Beauty.Dick.Domain.Model.Audit;
using System;

namespace Beauty.Barry.Application.Dto.Department
{
    public class DepartmentDto : AbstractDepartmentDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public AuditDto Audit { get; set; }
    }
}

using Beauty.Barry.Application.Dto.Department;
using System;

namespace Beauty.Barry.Application.Service
{
    public class ServiceDto
    {
        public Guid Id { get; set; }
        public DepartmentDto Department { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public string Status { get; set; }
    }
}

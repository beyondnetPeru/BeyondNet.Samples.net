using System;

namespace Beauty.Dick.Domain.Model.Audit
{
    public class AuditDto
    {
        public string CreateUser { get; set; }

        public string CreateDevice { get; set; }

        public DateTime CreateDate { get; set; }

        public string UpdateUser { get; set; }

        public string UpdateDevice { get; set; }

        public DateTime UpdateDate { get; set; }

        public Int64 TimeSpan { get; set; }
    }
}

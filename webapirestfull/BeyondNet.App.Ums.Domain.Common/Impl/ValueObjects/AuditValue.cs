using System;
using System.Collections.Generic;

namespace BeyondNet.App.Ums.Domain.Common.Impl.ValueObjects
{
  
    public class AuditValue : AbstractValueObject
    {
        public string AuditCreateUser { get; private set; }
        public string AuditCreateDevice { get; private set; }
        public DateTime AuditCreateDate { get; private set; }
        public string AuditUpdateUser { get; private set; }
        public string AuditUpdateDevice { get; private set; }
        public DateTime? AuditUpdateDate { get; private set; }
        public Int64 AuditTimeSpan { get; private set; }

        protected override void Validate()
        {
     
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AuditTimeSpan;

        }

        public void Create(string auditCreateUser, string auditCreateDevice, DateTime auditCreateDate)
        {
            AuditCreateUser = auditCreateUser;
            AuditCreateDevice = auditCreateDevice;
            AuditCreateDate = auditCreateDate;
            //AuditTimeSpan = AuditHelper.TimeSpan;
        }

        public void Update(string auditUpdateUser, string auditUpdateDevice, DateTime auditUpdateDate)
        {
            AuditUpdateUser = auditUpdateUser;
            AuditUpdateDevice = auditUpdateDevice;
            AuditUpdateDate = auditUpdateDate;
            //AuditTimeSpan = AuditHelper.TimeSpan;
        }
    }
}

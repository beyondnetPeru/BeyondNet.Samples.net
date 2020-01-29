using BeyondNet.App.Ums.Domain.Common.Impl.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeyondNet.App.Ums.DataAccess.EF.Helpers
{
    public static class AuditMapHelper<TEntity> where TEntity:class 
    {
        public static void MapAudit(ReferenceOwnershipBuilder<TEntity, AuditValue> referenceOwnershipBuilder)
        {
            referenceOwnershipBuilder.Property(p => p.AuditCreateUser).HasColumnName("AuditUserCreate");
            referenceOwnershipBuilder.Property(p => p.AuditCreateDevice).HasColumnName("AuditDeviceCreate");
            referenceOwnershipBuilder.Property(p => p.AuditCreateDate).HasColumnName("AuditDateCreate");
            referenceOwnershipBuilder.Property(p => p.AuditUpdateUser).HasColumnName("AuditUserUpdate");
            referenceOwnershipBuilder.Property(p => p.AuditUpdateDevice).HasColumnName("AuditDeviceUpdate");
            referenceOwnershipBuilder.Property(p => p.AuditUpdateDate).HasColumnName("AuditDateUpdate");
            referenceOwnershipBuilder.Property(p => p.AuditTimeSpan).HasColumnName("AuditTimespan");
        }
    }
}

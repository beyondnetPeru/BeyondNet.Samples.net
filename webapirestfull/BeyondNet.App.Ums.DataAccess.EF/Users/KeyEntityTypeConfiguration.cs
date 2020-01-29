using BeyondNet.App.Ums.DataAccess.EF.Helpers;
using BeyondNet.App.Ums.Domain.User.Key;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeyondNet.App.Ums.DataAccess.EF.Users
{
    public class KeyEntityTypeConfiguration : IEntityTypeConfiguration<KeyEdit>
    {
        public void Configure(EntityTypeBuilder<KeyEdit> keyConfiguration)
        {
            keyConfiguration.ToTable("Keys");

            keyConfiguration.HasKey(c => c.Id);

            keyConfiguration.Property(c => c.Password).HasMaxLength(150).IsRequired();

            keyConfiguration.Property(c => c.LastSignOn);

            keyConfiguration.Property(c => c.Status);

            keyConfiguration.OwnsOne(c => c.Audit, AuditMapHelper<KeyEdit>.MapAudit);
        }
    }
}

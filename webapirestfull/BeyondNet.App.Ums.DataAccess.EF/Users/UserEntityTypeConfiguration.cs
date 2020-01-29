using BeyondNet.App.Ums.DataAccess.EF.Helpers;
using BeyondNet.App.Ums.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeyondNet.App.Ums.DataAccess.EF.Users
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEdit>
    {
        public void Configure(EntityTypeBuilder<UserEdit> userConfiguration)
        {
            userConfiguration.ToTable("Users");

            userConfiguration.HasKey(c => c.Id);

            userConfiguration.OwnsOne(c => c.Audit, AuditMapHelper<UserEdit>.MapAudit);

            userConfiguration.Property(c => c.UserName).HasMaxLength(150).IsRequired();

            userConfiguration.Property(c => c.FullName).HasMaxLength(300).IsRequired();

            userConfiguration.Property(c => c.Email).HasMaxLength(300).IsRequired();

            userConfiguration.Property(c => c.Status);

            var navigation = userConfiguration.Metadata.FindNavigation(nameof(UserEdit.Keys));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }

       
    }
}
using BeyondNet.App.Ums.DataAccess.EF.Users;
using BeyondNet.App.Ums.Domain.User;
using BeyondNet.App.Ums.Domain.User.Key;
using Microsoft.EntityFrameworkCore;

namespace BeyondNet.App.Ums.DataAccess.EF
{
    public class UmsDbContext : DbContext
    {
        public UmsDbContext(DbContextOptions<UmsDbContext> options) 
            : base(options)
        {
            //any changes to the context options can now be done here
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new KeyEntityTypeConfiguration());
        }

        public DbSet<UserEdit> Users { get; set; }
        public DbSet<KeyEdit> Keys { get; set; }
    }
}

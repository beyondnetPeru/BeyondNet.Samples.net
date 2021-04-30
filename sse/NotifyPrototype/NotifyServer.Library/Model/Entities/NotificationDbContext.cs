using Microsoft.EntityFrameworkCore;

namespace NotifyServer.Library.Model.Entities
{
    public class NotificationDbContext : DbContext {

        public DbSet<Notification> Notifications { get; set; }

        public static readonly Microsoft.Extensions.Logging.LoggerFactory myLoggerFactory =
            new Microsoft.Extensions.Logging.LoggerFactory(new[] {
                new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
            });

        public NotificationDbContext()
        {
            
        }

        public NotificationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(myLoggerFactory);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeyondNet.App.Ums.DataAccess.EF
{
    public abstract class DbEntityConfiguration<TEntity> where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> entity);
    }
}
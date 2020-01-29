using BeyondNet.App.Ums.Domain.Common.Interface;
using Microsoft.EntityFrameworkCore;

namespace BeyondNet.App.Ums.DataAccess.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _umDbContext;

        public UnitOfWork(DbContext umDbContext)
        {
            _umDbContext = umDbContext;
        }
        public UnitOfWork()
        {
        }

        public void Dispose()
        {
            
        }

        public void Commit()
        {
            _umDbContext.SaveChanges();
        }
    }
}

using System;

namespace BeyondNet.App.Ums.Domain.Common.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}

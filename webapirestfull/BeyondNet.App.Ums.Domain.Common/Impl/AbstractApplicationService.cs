using System;
using BeyondNet.App.Ums.Domain.Common.Interface;

namespace BeyondNet.App.Ums.Domain.Common.Impl
{
    public class AbstractApplicationService : IApplicationService
    {
        public AbstractApplicationService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public IUnitOfWork UnitOfWork { get; }
    }
}

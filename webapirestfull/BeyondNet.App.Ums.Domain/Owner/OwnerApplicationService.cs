using System.Collections.Generic;
using BeyondNet.App.Ums.Domain.Common.Impl;
using BeyondNet.App.Ums.Domain.Common.Interface;
using BeyondNet.App.Ums.Domain.Owner.Dto;

namespace BeyondNet.App.Ums.Domain.Owner
{
    public class OwnerApplicationService : AbstractApplicationService, IOwnerApplicationService
    {
        public OwnerApplicationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        public IEnumerable<OwnerDto> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}

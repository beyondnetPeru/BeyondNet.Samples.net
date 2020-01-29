using System.Collections.Generic;
using BeyondNet.App.Ums.Domain.Owner.Dto;

namespace BeyondNet.App.Ums.Domain.Owner
{
    public interface IOwnerApplicationService
    {
        IEnumerable<OwnerDto> GetAll();
    }
}

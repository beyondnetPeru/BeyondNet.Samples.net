using System;
using System.Collections.Generic;
using BeyondNet.App.Ums.Domain.Common.Interface;

namespace BeyondNet.App.Ums.Domain.Owner
{
    public interface IOwnerRepository : IRepository<OwnerEdit, Guid>
    {
        IEnumerable<OwnerInfoReadModel> GetAll();
        bool Exists(ISpecification<OwnerEdit> spec);
    }
}

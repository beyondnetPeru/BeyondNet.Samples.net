using System.Collections.Generic;
using BeyondNet.App.Ums.Domain.Common.Impl;


namespace BeyondNet.App.Ums.Domain.Common.Interface
{
    public interface IDomainEventRepository
    {
        void Add<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : DomainEvent;
        IEnumerable<DomainEventRecord> FindAll();
    }
}

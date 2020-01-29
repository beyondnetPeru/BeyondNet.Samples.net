using Beauty.Dick.Domain.Impl;
using Jal.Monads;
using System.Collections.Generic;

namespace Beauty.Dick.Domain.Interface
{
    public interface IDomainEventRepository
    {
        void Add<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : DomainEvent;
        Result<IEnumerable<DomainEventRecord>> FindAll();
    }
}

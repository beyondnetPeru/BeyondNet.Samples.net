using System;

namespace Beauty.Dick.Domain.Interface
{
    public interface IAggregateRoot
    {
        Guid Id { get; set; }
    }
}

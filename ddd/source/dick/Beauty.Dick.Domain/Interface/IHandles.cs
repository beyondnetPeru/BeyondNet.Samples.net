using Beauty.Dick.Domain.Impl;

namespace Beauty.Dick.Domain.Interface
{
    public interface IHandles<in T> where T : DomainEvent
    {
        void Handle(T args);
    }
}

using BeyondNet.App.Ums.Domain.Common.Impl;

namespace BeyondNet.App.Ums.Domain.Common.Interface
{
    public interface IHandles<in T> where T : DomainEvent
    {
        void Handle(T args); 
    } 
}

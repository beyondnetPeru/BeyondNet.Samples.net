using BeyondNet.App.Ums.Domain.Common.Interface;

namespace BeyondNet.App.Ums.Domain.Common.Impl
{
    public class DomainEventHandle<TDomainEvent> : IHandles<TDomainEvent> where TDomainEvent : DomainEvent
    {
        private readonly IDomainEventRepository _domainEventRepository;
        private readonly IRequestCorrelationIdentifier _requestCorrelationIdentifier;

        public DomainEventHandle(IDomainEventRepository domainEventRepository, 
            IRequestCorrelationIdentifier requestCorrelationIdentifier)
        {
            this._domainEventRepository = domainEventRepository;
            this._requestCorrelationIdentifier = requestCorrelationIdentifier;
        }

        public void Handle(TDomainEvent @event)
        {
            @event.Flatten();
            @event.CorrelationId = _requestCorrelationIdentifier.CorrelationId;
            _domainEventRepository.Add(@event);
        }
    }
}

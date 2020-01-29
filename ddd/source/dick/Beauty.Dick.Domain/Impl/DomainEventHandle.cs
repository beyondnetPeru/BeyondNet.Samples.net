using Beauty.Dick.Domain.Interface;

namespace Beauty.Dick.Domain.Impl
{
    public class DomainEventHandle<TDomainEvent> : IHandles<TDomainEvent> where TDomainEvent : DomainEvent
    {
        private readonly IDomainEventRepository _domainEventRepository;
        private readonly IRequestCorrelationIdentifier _requestCorrelationIdentifier;

        public DomainEventHandle(IDomainEventRepository domainEventRepository,
            IRequestCorrelationIdentifier requestCorrelationIdentifier)
        {
            _domainEventRepository = domainEventRepository;
            _requestCorrelationIdentifier = requestCorrelationIdentifier;
        }

        public void Handle(TDomainEvent @event)
        {
            @event.Flatten();
            @event.CorrelationId = _requestCorrelationIdentifier.CorrelationId;
            _domainEventRepository.Add(@event);
        }
    }
}

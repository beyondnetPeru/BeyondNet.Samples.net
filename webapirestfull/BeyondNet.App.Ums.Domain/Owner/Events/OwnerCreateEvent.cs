using BeyondNet.App.Ums.Domain.Common.Impl;

namespace BeyondNet.App.Ums.Domain.Owner.Events
{
    public class OwnerCreateEvent : DomainEvent
    {
        public OwnerEdit Owner { get; set; }
        public override void Flatten()
        {
            Args.Add("Id", Owner.Id);
            Args.Add("Name", Owner.Name);
        }
    }
}

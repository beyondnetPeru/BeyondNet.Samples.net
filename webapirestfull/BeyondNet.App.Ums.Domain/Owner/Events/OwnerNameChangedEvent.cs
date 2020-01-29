using BeyondNet.App.Ums.Domain.Common.Impl;

namespace BeyondNet.App.Ums.Domain.Owner.Events
{
    public class OwnerNameChangedEvent : DomainEvent
    {
        private readonly string _oldName;
        public OwnerEdit Owner { get; set; }

        public OwnerNameChangedEvent(string oldName)
        {
            _oldName = oldName;
        }
        public override void Flatten()
        {
            Args.Add("Id", Owner.Id);
            Args.Add("OldName", _oldName);
            Args.Add("Name", Owner.Name);
        }
    }
}

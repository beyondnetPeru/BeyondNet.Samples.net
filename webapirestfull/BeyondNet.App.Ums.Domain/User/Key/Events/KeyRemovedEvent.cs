using BeyondNet.App.Ums.Domain.Common.Impl;

namespace BeyondNet.App.Ums.Domain.User.Key.Events
{
    public class KeyRemovedEvent : DomainEvent
    {
        public KeyEdit Key { get; set; }

        public override void Flatten()
        {
            Args.Add("KeyId", Key.Id);
            Args.Add("Password", Key.Password);
            Args.Add("Status", Key.Status);
        }
    }
}

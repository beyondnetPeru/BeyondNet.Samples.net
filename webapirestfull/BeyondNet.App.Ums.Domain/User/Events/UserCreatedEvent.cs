using BeyondNet.App.Ums.Domain.Common.Impl;

namespace BeyondNet.App.Ums.Domain.User.Events
{
    public class UserCreatedEvent : DomainEvent
    {
        public UserEdit User { get; set; }

        public override void Flatten()
        {
            Args.Add("UserId", User.Id);
            Args.Add("ExternalId", User.ExternalId);
            Args.Add("UserName", User.UserName);
            Args.Add("FullName", User.FullName);
            Args.Add("Email", User.Email);
            Args.Add("Status", User.Status);
        }
    }
}

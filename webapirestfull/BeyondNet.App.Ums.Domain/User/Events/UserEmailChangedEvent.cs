using BeyondNet.App.Ums.Domain.Common.Impl;

namespace BeyondNet.App.Ums.Domain.User.Events
{
    public class UserEmailChangedEvent : DomainEvent
    {
        private readonly string _oldEmail;
        public UserEdit User { get; set; }

        public UserEmailChangedEvent(string oldEmail)
        {
            _oldEmail = oldEmail;
        }
        public override void Flatten()
        {
            Args.Add("UserId", User.Id);
            Args.Add("OldEmail", _oldEmail);
            Args.Add("NewEmail", User.Email);
        }
    }
}


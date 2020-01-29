using BeyondNet.App.Ums.Domain.Common.Impl;

namespace BeyondNet.App.Ums.Domain.User.Key.Events
{
    public class KeyChangedPasswordEvent : DomainEvent
    {
        private readonly string _oldPassword;
        public KeyEdit Key { get; set; }

        public KeyChangedPasswordEvent(string oldPassword)
        {
            _oldPassword = oldPassword;
        }

        public override void Flatten()
        {
            Args.Add("UserId", Key.User.Id);
            Args.Add("OldPassword", _oldPassword);
            Args.Add("Password", Key.Password);
        }
    }
}

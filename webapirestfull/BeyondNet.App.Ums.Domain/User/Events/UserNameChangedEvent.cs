using BeyondNet.App.Ums.Domain.Common.Impl;

namespace BeyondNet.App.Ums.Domain.User.Events
{
    public class UserNameChangedEvent : DomainEvent
    {
        private readonly string _oldUserName;

        public UserEdit User { get; set; }

        public UserNameChangedEvent(string oldUserName)
        {
            _oldUserName = oldUserName;
        }
        public override void Flatten()
        {
            Args.Add("UserId", User.Id);
            Args.Add("OldUserName", _oldUserName);
            Args.Add("NewUserName", User.UserName);
        }
    }
}

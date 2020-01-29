using BeyondNet.App.Ums.Domain.Common.Interface;
using BeyondNet.App.Ums.Helpers.Interface;

namespace BeyondNet.App.Ums.Domain.User.Events
{
    public class UserChangeEmailEventHandler : IHandles<UserEmailChangedEvent>, IHandles<UserCreatedEvent>
    {
        private readonly IEmailHelper _emailHelper;

        public UserChangeEmailEventHandler(IEmailHelper emailHelper)
        {
            _emailHelper = emailHelper;
        }
        public void Handle(UserEmailChangedEvent args)
        {
            _emailHelper.Send(new []{""});
        }

        public void Handle(UserCreatedEvent args)
        {
            _emailHelper.Send(new[] {""});
        }
    }
}

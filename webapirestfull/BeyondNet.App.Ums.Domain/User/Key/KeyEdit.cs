using System;
using BeyondNet.App.Ums.Domain.Common.Impl;
using BeyondNet.App.Ums.Domain.Common.Impl.ValueObjects;
using BeyondNet.App.Ums.Domain.User.Key.Events;

namespace BeyondNet.App.Ums.Domain.User.Key
{
    public class KeyEdit : AbstractEntity<KeyEdit>
    {
        #region Properties

        public UserEdit User { get; protected set; }

        public string Password { get; protected set; }

        public DateTime LastSignOn { get; protected set; }

        public AuditValue Audit { get; set; }

        public int Status { get; protected set; }

        #endregion

        #region Factory Methods

        public static KeyEdit Create(UserEdit user, string password)
        {
            var key = new KeyEdit()
            {
                Id = Guid.NewGuid(),
                User = user,
                Password = password,
                Status = EKeyStatus.Active
            };

            return key;
        }

        public virtual void ChangePassword(string newPassword)
        {
            var oldPassword = Password;

            Password = newPassword;

            DomainEvents.Raise(new KeyChangedPasswordEvent(oldPassword){Key = this});
        }

        public virtual void Deactivate()
        {
            Status = EKeyStatus.Inactive;

            DomainEvents.Raise(new KeyDeactivateEvent { Key = this });
        }

        #endregion

        #region Validation Rules

        protected override void Validate()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

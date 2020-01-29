using System;
using System.Collections.Generic;
using System.Linq;
using BeyondNet.App.Ums.Domain.Common.Impl;
using BeyondNet.App.Ums.Domain.Common.Impl.ValueObjects;
using BeyondNet.App.Ums.Domain.User.Events;
using BeyondNet.App.Ums.Domain.User.Key;
using BeyondNet.App.Ums.Domain.User.Key.Events;
using BeyondNet.App.Ums.Domain.User.Key.Specifications;
using BeyondNet.App.Ums.Globalization;
using BeyondNet.App.Ums.Helpers.Impl;
using FluentValidation.Results;

namespace BeyondNet.App.Ums.Domain.User
{
    public class UserEdit : AbstractEntity<UserEdit>
    {
        #region Declarations

        private readonly List<KeyEdit> _keys = new List<KeyEdit>();

        #endregion

        #region Properties

        public string ExternalId { get; private set; }

        public string UserName { get; private set; }

        public string FullName { get; private set; }

        public string Email { get; private set; }

        public AuditValue Audit { get; private set; }

        public int Status { get; private set; }

        public IReadOnlyCollection<KeyEdit> Keys => _keys;

        #endregion

        #region Factory Methods

        public static UserEdit Create(string externalId, string userName, string fullName, string email)
        {
            return Create(Guid.NewGuid(), externalId, userName, fullName, email);
        }

        public static UserEdit Create(Guid id, string externalId, string userName, string fullName, string email)
        {
            var user = new UserEdit()
            {
                Id = id,
                ExternalId = externalId,
                UserName = userName,
                FullName = fullName,
                Email = email,
                Status = EUserStatus.Active
            };

            var audit = new AuditValue();
            audit.Create(AuditHelper.UserName, AuditHelper.MachineName, AuditHelper.UtcDateTime);

            user.Audit = audit;

            MarkAsNew();

            DomainEvents.Raise(new UserCreatedEvent() { User = user });

            return user;
        }

        public virtual void BlockUser(Guid id)
        {
            Status = EUserStatus.Blocked;

            CommonUpdate(this);

            DomainEvents.Raise(new UserBlockedEvent { User = this});
        }

        public virtual void ChangeUserName(string newUserName)
        {
            if (string.Equals(UserName.ToUpper(), newUserName.ToUpper(), StringComparison.CurrentCultureIgnoreCase))
                throw new ApplicationException($"Username {UserName} and new username {newUserName} are the same.");

            var oldUserName = UserName;

            UserName = newUserName;

            CommonUpdate(this);

            DomainEvents.Raise(new UserNameChangedEvent(oldUserName) { User = this });
        }

        public virtual void ChangeEmail(string newEmail)
        {
            if (string.Equals(Email.ToUpper(), newEmail.ToUpper(), StringComparison.CurrentCultureIgnoreCase))
                throw new ApplicationException($"Email {Email} and new email {newEmail} are the same.");

            var oldEmail = Email;

            Email = newEmail;

            CommonUpdate(this);

            DomainEvents.Raise(new UserEmailChangedEvent(oldEmail) { User = this });
            
        }

        public virtual void Update(string externalId, string userName, string fullName, string email)
        {
            ExternalId = externalId;
            ChangeEmail(email);
            ChangeUserName(userName);
            FullName = fullName;

            CommonUpdate(this);

            DomainEvents.Raise(new UserUpdatedEvent {User = this});
        }

        private static void CommonUpdate(UserEdit user)
        {
            user.Audit.Update(AuditHelper.UserName, AuditHelper.MachineName, AuditHelper.UtcDateTime);

            MarkAsDirty();
        }

        public KeyEdit CreateKey(KeyEdit key)
        {
            var existKeyActive = Keys.Any(x => x.Status == EKeyStatus.Active);

            if (!existKeyActive)
            {
                AddBrokenValidationRule(new ValidationFailure(nameof(Status), UserMessages.ErrorRuleKeyActiveExists));
            }
            else
            {
                _keys.Add(key);

                DomainEvents.Raise(new KeyAddedEvent() { Key = key });
            }

            return key;
        }

        public virtual void DeleteKey(KeyEdit key)
        {
            if (key.Status == EKeyStatus.Active)
                AddBrokenValidationRule(new ValidationFailure(nameof(Status), UserMessages.ErrorRuleKeyIsActive));

            var keyFound = _keys.Find(new KeyExistsByKeySpec(key).IsSatisfiedBy);

            _keys.Remove(keyFound);

            DomainEvents.Raise(new KeyRemovedEvent() { Key = keyFound });
        }

        #endregion

        #region Validation Rules

        protected override void Validate()
        {
        }

        #endregion
    }
}

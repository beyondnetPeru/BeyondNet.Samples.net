using System;
using BeyondNet.App.Ums.Domain.Common.Impl;
using BeyondNet.App.Ums.Domain.Common.Impl.ValueObjects;
using BeyondNet.App.Ums.Domain.Owner.Events;
using BeyondNet.App.Ums.Helpers.Impl;

namespace BeyondNet.App.Ums.Domain.Owner
{
    public class OwnerEdit : AbstractEntity<OwnerEdit>
    {
        #region Properties

        public string Name { get; private set; }
        public int Status { get; private set; }
        public AuditValue Audit { get; private set; }

        #endregion

        #region Factory Methods

        public static OwnerEdit Create(string name)
        {
            return new OwnerEdit(){Id = Guid.NewGuid(), Name = name};
        }

        public static OwnerEdit Create(Guid id, string name) {
            
            var owner = new OwnerEdit { Id = id, Name = name, Status = EOwnerStatus.Active };

            var audit = new AuditValue();
            audit.Create(AuditHelper.UserName, AuditHelper.MachineName, AuditHelper.UtcDateTime);

            owner.Audit = audit;

            MarkAsNew();

            DomainEvents.Raise(new OwnerCreateEvent(){ Owner = owner });

            return owner;
        }

        public void ChangeName(string name)
        {
            var oldName = Name;

            Name = name;

            CommonUpdate(this);

            DomainEvents.Raise(new OwnerNameChangedEvent(oldName) { Owner = this });
        }

        private void CommonUpdate(OwnerEdit ownerEdit)
        {
            ownerEdit.Audit.Update(AuditHelper.UserName, AuditHelper.MachineName, AuditHelper.UtcDateTime);

            MarkAsDirty();
        }

        public void DisabledOwner()
        {
            Status = EOwnerStatus.Inactive;

            DomainEvents.Raise(new OwnerDisabledEvent(){ Owner = this});

            CommonUpdate(this);
        }

        public void EnabledOwner()
        {
            Status = EOwnerStatus.Active;

            DomainEvents.Raise(new OwnerDisabledEvent() { Owner = this });

            CommonUpdate(this);
        }

        #endregion

        #region Business Rules

        protected override void Validate()
        {
            
        }

        #endregion
    }
}

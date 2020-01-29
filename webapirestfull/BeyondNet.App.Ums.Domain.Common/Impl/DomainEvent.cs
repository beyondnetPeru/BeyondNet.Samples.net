using System;
using System.Collections.Generic;

namespace BeyondNet.App.Ums.Domain.Common.Impl
{
    public abstract class DomainEvent 
    {
        public string Type => GetType().Name;

        public DateTime Created { get; }

        public Dictionary<string, object> Args { get; }

        public string CorrelationId { get;  set; }

        protected DomainEvent()
        {
            Created = DateTime.Now;
            Args = new Dictionary<string, Object>();
        }

        public abstract void Flatten();
    }
}

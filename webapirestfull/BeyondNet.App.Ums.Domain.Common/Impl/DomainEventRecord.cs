using System;
using System.Collections.Generic;

namespace BeyondNet.App.Ums.Domain.Common.Impl
{
    public class DomainEventRecord
    {
        public string Type { get; set; }
        public List<KeyValuePair<string, string>> Args { get; set; }
        public string CorrelationId { get; set; }
        public DateTime Created { get; set; }
    }
}

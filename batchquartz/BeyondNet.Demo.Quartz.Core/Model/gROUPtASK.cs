using System.Collections.Generic;

namespace BeyondNet.Demo.Quartz.Core.Model
{
    public class GroupTask
    {
        public string GroupId { set; get; }
        public string Task { set; get; }
        public string ConfigurationEndpoint { set; get; }
        public IDictionary<string, string> Settings { set; get; }

        public override string ToString()
        {
            return $"{GroupId}-{Task}";
        }
    }
}

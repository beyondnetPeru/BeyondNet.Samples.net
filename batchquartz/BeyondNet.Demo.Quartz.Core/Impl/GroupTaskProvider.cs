using System.Collections.Generic;
using BeyondNet.Demo.Quartz.Core.Interfaces;
using BeyondNet.Demo.Quartz.Core.Model;

namespace BeyondNet.Demo.Quartz.Core.Impl
{
    public class GroupTaskProvider : IGroupTaskProvider
    {
        public GroupTask[] Provide()
        {
            var groupTask =  new List<GroupTask>()
            {
              new GroupTask() { ConfigurationEndpoint = "", GroupId = "", Task = "", Settings = new Dictionary<string, string>()}   
            };

            var setting = new Dictionary<string, string> {{"K1", "Key1"}};

            groupTask[0].Settings = setting;

            return groupTask.ToArray();

        }

        public GroupTask[] Provide(string taskType)
        {
            throw new System.NotImplementedException();
        }

        public GroupTask[] Provide(string taskType, string[] groups)
        {
            throw new System.NotImplementedException();
        }

        public IDictionary<string, string> PopulateSettings(GroupTask groupTask)
        {
            return new Dictionary<string, string> {{"Key1", "Value1"}, {"Key2", "Value2"}};
        }
    }
}

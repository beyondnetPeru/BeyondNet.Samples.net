using BeyondNet.Demo.Quartz.Core.Model;

namespace BeyondNet.Demo.Quartz.Core.Interfaces
{
    public interface IGroupTaskProvider
    {
        GroupTask[] Provide();

        GroupTask[] Provide(string taskType);

        GroupTask[] Provide(string taskType, string[] groups);
    }
}

using BeyondNet.Demo.Quartz.Core.Model;

namespace BeyondNet.Demo.Quartz.Core.Interfaces
{
    public interface IJobExecutor
    {
        void Execute(GroupTask groupTask);
    }
}

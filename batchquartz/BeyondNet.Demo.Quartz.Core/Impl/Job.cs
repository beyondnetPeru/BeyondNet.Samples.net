using System;
using BeyondNet.Demo.Quartz.Core.Interfaces;
using BeyondNet.Demo.Quartz.Core.Model;
using Quartz;

namespace BeyondNet.Demo.Quartz.Core.Impl
{
    public class Job<TExecutor>: IJob where TExecutor:class,IJobExecutor
    {
        public void Execute(IJobExecutionContext context)
        {
            var datamap = context.JobDetail.JobDataMap;
            var groupTask = datamap["groupTask"] as GroupTask;
            var executor = datamap["executor"] as TExecutor;

            executor?.Execute(groupTask);
        }
    }
}

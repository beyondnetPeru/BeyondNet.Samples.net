using System;
using BeyondNet.Demo.Quartz.Core.Interfaces;
using Quartz;
using Quartz.Impl;

namespace BeyondNet.Demo.Quartz.Core.Impl
{
    public class JobRunner<TExecutor>:IJobRunner where TExecutor: class, IJobExecutor
    {
        private readonly IGroupTaskProvider _provider;
        private IScheduler _scheduler;

        public JobRunner(IGroupTaskProvider provider)
        {
            _provider = new GroupTaskProvider();
        }

        public void Run(string[] groups = null)
        {
            var schedulerFactory = new StdSchedulerFactory();
            var groupTasks = _provider.Provide();
            _scheduler = schedulerFactory.GetScheduler();

            foreach (var group in groupTasks)
            {
                var trigger = TriggerBuilder.Create()
                    .WithIdentity(($"Group{group.GroupId}TriggerForTask{group.Task}"))
                    .WithSchedule(
                        CronScheduleBuilder.CronSchedule(group.Settings["schedule"])
                            .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById(group.Settings["timezone"])))
                    .Build();


            }

            _scheduler.Start();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}

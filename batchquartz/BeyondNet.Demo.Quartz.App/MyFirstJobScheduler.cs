using System;
using BeyondNet.Demo.Quartz.App.Jobs;
using Quartz;
using Quartz.Impl;

namespace BeyondNet.Demo.Quartz.App
{
    public class MyFirstJobScheduler
    {
        public static void Start()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            var job = JobBuilder.Create<MyFirtsJob>()
                .WithIdentity("MyFirstJob","JobGroup")
                .UsingJobData("var1","This a test for a JobDataMap")
                .UsingJobData("var2",20)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("MyFirstTrigger", "TriggerGroup")
                .WithCronSchedule("0 0/1 * 1/1 * ? *")
                .StartAt(DateTime.UtcNow)
                .UsingJobData("var3", "value for a trigger")
                .WithDailyTimeIntervalSchedule(s => s.WithIntervalInSeconds(10).OnEveryDay())
                .WithPriority(1)
                .Build();



            scheduler.ScheduleJob(job, trigger);
            scheduler.Start();

        }

    }
}

using System;
using Quartz;

namespace BeyondNet.Demo.Quartz.App.Jobs
{
    public class MyFirtsJob:IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var key = context.JobDetail.Key;
            var datamap = context.JobDetail.JobDataMap;
            var var1 = datamap.GetString("var1");
            var var2 = datamap.GetInt("var2");

            Console.WriteLine($"This is my first job in quartz with key ({key}) : {context.JobInstance} - {DateTime.Now} with values: VAR1 => {var1} - VAR2 => {var2}");
        }
    }
}

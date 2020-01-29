namespace BeyondNet.Demo.Quartz.Core.Interfaces
{
    public interface IJobRunner
    {
        void Run(string[] groups = null);
        void Stop();
    }
}

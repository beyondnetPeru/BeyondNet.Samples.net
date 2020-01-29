using System;

namespace BeyondNet.Demo.Polly.App.Interfaces
{
    public interface IRetryPolicy
    {
        T HandleRetry<T>(Func<T> func, int times, TimeSpan wait) where T : class;
        T HandleWaitAndRetry<T>(Func<T> func, int times);
        
    }
}

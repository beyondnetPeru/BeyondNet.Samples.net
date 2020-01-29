using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using BeyondNet.Demo.Polly.App.Interfaces;
using Polly;

namespace BeyondNet.Demo.Polly.App.Impl
{
    public class LinealPollyRetryPolicy : IRetryPolicy
    {
        readonly HttpStatusCode[] _httpStatusCodesWorthRetrying =
            {
                HttpStatusCode.RequestTimeout, // 408
                HttpStatusCode.InternalServerError, // 500
                HttpStatusCode.BadGateway, // 502
                HttpStatusCode.ServiceUnavailable, // 503
                HttpStatusCode.GatewayTimeout // 504
            };

        public T HandleRetry<T>(Func<T> func, int times, TimeSpan wait) where T : class
        {
            throw new NotImplementedException();
        }

        public T HandleWaitAndRetry<T>(Func<T> func, int times)
        {
            try
            {
                var policy = Policy
                    .Handle<WebException>()
                    .OrResult<T>(r => _httpStatusCodesWorthRetrying.Contains(HttpStatusCode.Accepted))
                    .Retry(1, (ex, context) =>
                    {
                        Debug.WriteLine($"Error handled by POLLY => {ex.Exception}");
                    });

                return policy.Execute(func);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error handled by POLLY => {0}", ex.Message);
                throw;
            }
        }
    }
}

using System;
using System.Linq;
using System.Net;
using Polly;

namespace BeyondNet.Demo.Polly.App.Samples
{
    public class SampleWrapperPolly
    {
        public string[] Handle(Func<string[]> func, string[] exceptions, int times = 1)
        {
            var ex = Type.GetType(exceptions.First()); 

            return Policy.Handle<WebException>().Retry(times).Execute(func);
        }

        public T Execute<T>(Func<T> func, int times = 1) 
        {
            return Policy.Handle<Exception>().Retry(1).Execute(func);
        }
    }
}
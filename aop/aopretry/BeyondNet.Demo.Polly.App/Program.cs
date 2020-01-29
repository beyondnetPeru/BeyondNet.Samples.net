using System;
using BeyondNet.Demo.Polly.App.Impl;

namespace BeyondNet.Demo.Polly.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string uri = "https://raw.githubusercontent.com/jason-roberts/PSPolDemoFiles/master/SomeRemoteData.txt-";

            var retry = new LinealPollyRetryPolicy();
            var executor =  new HttpExecutor(retry);

            var tableRuleResponse = executor.Execute<string>(uri);

            Console.WriteLine("Status: {0}",tableRuleResponse.Status);
            Console.WriteLine("Message: {0}",tableRuleResponse.Messages);
            Console.WriteLine("Data: {0}", tableRuleResponse.Data);

            Console.Read();
        }
    }
}

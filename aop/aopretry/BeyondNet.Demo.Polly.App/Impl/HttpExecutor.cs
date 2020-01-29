using System;
using System.Net;
using BeyondNet.Demo.Polly.App.Interfaces;
using BeyondNet.Demo.Polly.App.Model;

namespace BeyondNet.Demo.Polly.App.Impl
{
    public class HttpExecutor
    {
        private readonly IRetryPolicy _retryPolicy;

        public HttpExecutor(IRetryPolicy retryPolicy)
        {
            _retryPolicy = retryPolicy;
        }

        public TableRuleResponse<T> Execute<T>(string uri)
        {
            var tableRuleResponse = new TableRuleResponse<T>();
            
            var httpDownloader = new HttpDownloader();
            
           

            return tableRuleResponse;
        }
    }
}

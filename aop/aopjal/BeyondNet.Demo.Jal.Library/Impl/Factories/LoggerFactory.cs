using System;
using System.Linq;
using Jal.Factory.Interface;
using Jal.Monads;
using BeyondNet.Demo.Jal.Library.Interface;

namespace BeyondNet.Demo.Jal.Library.Impl.Factories
{
    public class LoggerFactory : ILoggerFactory
    {
        private readonly IObjectFactory _objectFactory;

        public LoggerFactory(IObjectFactory objectFactory)
        {
            _objectFactory = objectFactory;
        }
        public Result<ILogger> Create(string type)
        {
            try
            {
                return _objectFactory.Create<string, ILogger>(type).First().ToResult();
            }
            catch (Exception ex)
            {
                return Result.Failure<ILogger>(new[] { $"No logger type filter set up. Error: {ex.Message}" });
            }
        }
    }
}

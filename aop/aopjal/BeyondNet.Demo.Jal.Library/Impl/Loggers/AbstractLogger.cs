using System.Collections.Generic;
using System.Linq;
using Jal.Monads;
using BeyondNet.Demo.Jal.Library.Interface;
using BeyondNet.Demo.Jal.Library.Model;

namespace BeyondNet.Demo.Jal.Library.Impl.Loggers
{
    public abstract class AbstractLogger : ILogger
    {
        protected IEnumerable<LogLevel> ListLevelLogPermitted;
        protected readonly IFormatter Formatter;

        protected AbstractLogger(IFormatter formatter)
        {
            Formatter = formatter;
            ListLevelLogPermitted = new List<LogLevel>();
        }

        public Result SetRules(IEnumerable<LogLevel> listLevelLogPermitted)
        {
            ListLevelLogPermitted = listLevelLogPermitted;
            return Result.Success();
        }

        public virtual Result Log(LogMessage logMessage)
        {
            var levelIsPermitted = ListLevelLogPermitted.Contains(logMessage.Level);

            return !levelIsPermitted ? Result.Failure(new[] {$"The level: {logMessage.Level} is not permitted."}) : Result.Success();
        }
    }
}

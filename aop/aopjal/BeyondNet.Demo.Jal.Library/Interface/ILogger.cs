using System.Collections.Generic;
using Jal.Monads;
using BeyondNet.Demo.Jal.Library.Model;

namespace BeyondNet.Demo.Jal.Library.Interface
{
    public interface ILogger
    {
        Result SetRules(IEnumerable<LogLevel> listLevelLogPermitted);
        Result Log(LogMessage logMessage);
    }
}

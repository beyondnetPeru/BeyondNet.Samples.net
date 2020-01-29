using Jal.Monads;
using BeyondNet.Demo.Jal.Library.Interface;
using BeyondNet.Demo.Jal.Library.Model;

namespace BeyondNet.Demo.Jal.Library.Impl.Formatters
{
    public class NullFormatter:IFormatter
    {
        public Result<string> Apply(LogMessage logMessage)
        {
            return Result.Success($"Message: {logMessage.Message}.");
        }
    }
}

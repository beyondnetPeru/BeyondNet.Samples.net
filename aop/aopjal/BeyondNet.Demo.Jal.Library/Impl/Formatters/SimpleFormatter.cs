using BeyondNet.Demo.Jal.Library.Interface;
using Jal.Monads;
using BeyondNet.Demo.Jal.Library.Model;

namespace BeyondNet.Demo.Jal.Library.Impl.Formatters
{
    public class SimpleFormatter:IFormatter
    {
        public Result<string> Apply(LogMessage logMessage)
        {
            return Result.Success($"Level: {logMessage.Level} - DateTime: {logMessage.DateTime}, Message: {logMessage.Message}");
        }
    }
}

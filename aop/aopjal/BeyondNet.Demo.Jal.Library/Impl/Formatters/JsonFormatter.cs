using Jal.Monads;
using BeyondNet.Demo.Jal.Library.Interface;
using BeyondNet.Demo.Jal.Library.Model;
using Newtonsoft.Json;

namespace BeyondNet.Demo.Jal.Library.Impl.Formatters
{
    public class JsonFormatter : IFormatter
    {
        public Result<string> Apply(LogMessage logMessage)
        {
            return JsonConvert.SerializeObject(logMessage);
        }
    }
}

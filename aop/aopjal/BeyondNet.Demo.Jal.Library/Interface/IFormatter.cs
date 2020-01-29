using Jal.Monads;
using BeyondNet.Demo.Jal.Library.Model;

namespace BeyondNet.Demo.Jal.Library.Interface
{
    public interface IFormatter
    {
        Result<string> Apply(LogMessage logMessage);
    }
}

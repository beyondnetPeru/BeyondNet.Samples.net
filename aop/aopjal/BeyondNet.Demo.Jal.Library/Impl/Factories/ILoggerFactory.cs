using Jal.Monads;
using BeyondNet.Demo.Jal.Library.Interface;

namespace BeyondNet.Demo.Jal.Library.Impl.Factories
{
    public interface ILoggerFactory
    {
        Result<ILogger> Create(string type);
    }
}

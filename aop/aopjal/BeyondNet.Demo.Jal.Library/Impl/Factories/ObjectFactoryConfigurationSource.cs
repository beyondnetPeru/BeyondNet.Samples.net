using Jal.Factory.Impl;
using BeyondNet.Demo.Jal.Library.Impl.Loggers;
using BeyondNet.Demo.Jal.Library.Interface;

namespace BeyondNet.Demo.Jal.Library.Impl.Factories
{
    public class ObjectFactoryConfigurationSource : AbstractObjectFactoryConfigurationSource
    {
        public ObjectFactoryConfigurationSource()
        {
            For<string, ILogger>().Create<ConsoleLogger>().When(x => x.Equals("CONSOLE"));
            For<string, ILogger>().Create<DatabaseLogger>().When(x => x.Equals("DATABASE"));
            For<string, ILogger>().Create<FileLogger>().When(x => x.Equals("FILE"));
        }
    }
}

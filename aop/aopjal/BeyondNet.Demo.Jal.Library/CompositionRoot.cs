using LightInject;
using BeyondNet.Demo.Jal.Library.Helper;
using BeyondNet.Demo.Jal.Library.Impl.DataAccess;
using BeyondNet.Demo.Jal.Library.Impl.Factories;
using BeyondNet.Demo.Jal.Library.Impl.Formatters;
using BeyondNet.Demo.Jal.Library.Impl.Loggers;
using BeyondNet.Demo.Jal.Library.Interface;

namespace BeyondNet.Demo.Jal.Library
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ILogger, ConsoleLogger>(typeof(ConsoleLogger).FullName, new PerContainerLifetime());
            serviceRegistry.Register<ILogger, DatabaseLogger>(typeof(DatabaseLogger).FullName, new PerContainerLifetime());
            serviceRegistry.Register<ILogger, FileLogger>(typeof(FileLogger).FullName, new PerContainerLifetime());
            serviceRegistry.Register<ILoggerFactory, LoggerFactory>(new PerContainerLifetime());
            
            serviceRegistry.Register<IRepository, SqlLogRepository>();
            serviceRegistry.Register<IFormatter, SimpleFormatter>();
            serviceRegistry.Register<IFileManager, FileManager>();
        }
    }
}

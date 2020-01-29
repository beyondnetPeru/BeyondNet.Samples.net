using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using BeyondNet.Demo.Jal.Library.Extensions;
using BeyondNet.Demo.Jal.Library.Impl.Factories;
using BeyondNet.Demo.Jal.Library.Model;
using Jal.Bootstrapper.Impl;
using Jal.Bootstrapper.Interface;
using Jal.Bootstrapper.LightInject;
using Jal.Factory.LightInject.Installer;
using Jal.Finder.Atrribute;
using Jal.Finder.Impl;
using Jal.Locator.LightInject.Installer;
using Jal.Settings.LightInject.Installer;
using LightInject;


namespace BeyondNet.Demo.Jal.AppConsole
{
    public class Program
    {
        private static ServiceContainer _container;
        private static string _application;

        private static void Initialize()
        {
            _application = ConfigurationManager.AppSettings["applicationname"];

            var directory = AppDomain.CurrentDomain.BaseDirectory;

            var finder = AssemblyFinder.Builder.UsePath(directory).Create;

            var assemblies = finder.GetAssembliesTagged<AssemblyTagAttribute>();

            var compositionassembly = assemblies.Where(x => x.FullName.Contains("BeyondNet.Demo.Jal")).ToArray();

            var iocbootstrapper = new LightInjectBootStrapper(compositionassembly, serviceContainer =>
            {
                serviceContainer.RegisterFrom<ServiceLocatorCompositionRoot>();

                serviceContainer.RegisterFrom<SettingsCompositionRoot>();

                serviceContainer.RegisterFactory(assemblies);
            });

            var composite = new CompositeBootstrapper(new IBootstrapper[] { iocbootstrapper });

            composite.Configure();

            _container = iocbootstrapper.Result;
        }

        static void Main(string[] args)
        {
            Initialize();
            
            Console.WriteLine("Select one looger type: ( Console | Database | File )");

            var stringLogType = Console.ReadLine();

            if (string.IsNullOrEmpty(stringLogType))
            {
                Console.WriteLine("The Log Type cannot be empty or null.");
                Console.Read();
                return;
            }

            if (stringLogType.GetLogEnum() == LoggerType.None)
            {
                Console.WriteLine("The Log Type was not selected.");
                Console.Read();
                return;
            }
            
            stringLogType = stringLogType.ToUpper();
            

            var listLevelPermitted = new List<LogLevel>();

            Console.WriteLine("Select the Level Type to be included ( Message | Warning | Error ). Please, if you want select more than one use a comma separator.");

            var readLevelTypes = Console.ReadLine();

            if (string.IsNullOrEmpty(readLevelTypes))
            {
                Console.WriteLine("The Level Type was not selected.");
                Console.Read();
                return;
            }

            var listStringArrayLevelPermitted = readLevelTypes.Split(',').ToArray();

            foreach (var item in listStringArrayLevelPermitted)
            {
                var enumLevel = item.GetLevelEnum();

                if (enumLevel == LogLevel.None)
                {
                    Console.WriteLine("The Log level was not selected.");
                    Console.Read();
                    return;
                }

                listLevelPermitted.Add(enumLevel);
            }
            
            var logFactory = _container.GetInstance<ILoggerFactory>();
            var logger = logFactory.Create(stringLogType).Content;
            logger.SetRules(listLevelPermitted);

            try
            {
                logger.Log(new LogMessage(LogLevel.Message, $"Starting to create new {nameof(FooClass)} object.", DateTime.UtcNow));

                var foo = new FooClass
                {
                    //Foo1 = "Alberto Arroyo Raygada",
                    Foo2 = ".NET Software Developer"
                };

                var fooValidator =  new FooValidator();
                var result = fooValidator.Validate(foo);

                if (result.Errors.Any())
                {
                    var i = 1;
                    foreach (var error in result.Errors)
                    {
                        logger.Log(new LogMessage(LogLevel.Warning, $"Foo object is not valid. Error {i}: {error.ErrorMessage}", DateTime.UtcNow));
                        i++;
                    }
                }
                else
                {
                    logger.Log(new LogMessage(LogLevel.Message, $"Object {nameof(FooClass)} was created with values Foo1: {foo.Foo1}, Foo2: {foo.Foo2}", DateTime.UtcNow));
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                logger.Log(new LogMessage(LogLevel.Error, $"An exception was ocurred. Error: {ex.Message}", DateTime.UtcNow));
            }
            finally
            {
                logger.Log(new LogMessage(LogLevel.Message, $"Demo was executed sucessfully. Application: {_application}", DateTime.UtcNow));
            }
        }
    }
}

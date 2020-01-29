using Topshelf;

namespace BeyondNet.Demo.Quartz.TopShelf
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(serviceConfig =>
            {
                serviceConfig.UseNLog();

                serviceConfig.Service<ConverteService>(serviceInstance =>
                {
                    serviceInstance.ConstructUsing(() => new ConverteService());
                    serviceInstance.WhenStarted(execute => execute.Start());
                    serviceInstance.WhenStopped(execute => execute.Stop());
                });

                serviceConfig.SetServiceName("AwesomeFileConverter");
                serviceConfig.SetDisplayName("Awesome File Converter");
                serviceConfig.SetDescription("A demo service");

                serviceConfig.StartAutomatically();
            });
        }
    }
}

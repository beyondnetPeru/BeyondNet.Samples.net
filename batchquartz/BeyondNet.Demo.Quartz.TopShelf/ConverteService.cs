using System.IO;
using Topshelf.Logging;

namespace BeyondNet.Demo.Quartz.TopShelf
{
    public class ConverteService
    {
        private FileSystemWatcher _watcher;
        private readonly LogWriter _log = HostLogger.Get<ConverteService>();

        public bool Start()
        {
            _watcher = new FileSystemWatcher(@"E:\5. Temp\a", "*_in.txt");
            _watcher.Created += FileCreated;
            _watcher.IncludeSubdirectories = false;
            _watcher.EnableRaisingEvents = true;
            return true;
        }

        public void FileCreated(object sender, FileSystemEventArgs e)
        {
            _log.InfoFormat("Starting conversion of '{0}'", e.FullPath);

            var content = File.ReadAllText(e.FullPath);

            var upperContent = content.ToUpperInvariant();

            var dir = Path.GetDirectoryName(e.FullPath);

            var convertedFileName = Path.GetFileName(e.FullPath) + ".converted";

            var convertedPath = string.Empty;

            if (dir != null)
            {
                convertedPath = Path.Combine(dir, convertedFileName);
            }

            File.WriteAllText(convertedPath, upperContent);
        }

        public bool Stop()
        {
            _watcher.Dispose();

            return true;
        }
    }
}

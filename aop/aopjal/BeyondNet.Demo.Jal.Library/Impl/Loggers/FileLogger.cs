using Jal.Monads;
using BeyondNet.Demo.Jal.Library.Helper;
using BeyondNet.Demo.Jal.Library.Interface;
using BeyondNet.Demo.Jal.Library.Model;

namespace BeyondNet.Demo.Jal.Library.Impl.Loggers
{
    public class FileLogger : AbstractLogger
    {
        private readonly IFileManager _fileManager;

        public FileLogger(IFormatter formatter, IFileManager fileManager) 
            : base(formatter)
        {
            _fileManager = fileManager;
        }
        
        public override Result Log(LogMessage logMessage)
        {
            return base.Log(logMessage).OnSuccess(() =>
            {
                return Formatter.Apply(logMessage)
                    .OnSuccess(messageFormatted => _fileManager.Write(messageFormatted));
            });
        }
    }
}

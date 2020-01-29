using Jal.Monads;
using BeyondNet.Demo.Jal.Library.Interface;
using BeyondNet.Demo.Jal.Library.Model;

namespace BeyondNet.Demo.Jal.Library.Impl.Loggers
{
    public class DatabaseLogger : AbstractLogger
    {
        private readonly IRepository _repository;

        public DatabaseLogger(IFormatter formatter, IRepository repository) : base(formatter)
        {
            _repository = repository;
        }

        public override Result Log(LogMessage logMessage)
        {
            return base.Log(logMessage).OnSuccess(() =>
            {
                return Formatter.Apply(logMessage).OnSuccess(messageFormatted =>
                {
                    _repository.Log(logMessage.Level, messageFormatted, logMessage.DateTime);
                });
            });
        }
    }
}

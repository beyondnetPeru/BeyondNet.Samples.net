using System;
using Jal.Monads;
using BeyondNet.Demo.Jal.Library.Interface;
using BeyondNet.Demo.Jal.Library.Model;

namespace BeyondNet.Demo.Jal.Library.Impl.Loggers
{
    public class ConsoleLogger : AbstractLogger
    {
        public ConsoleLogger(IFormatter formatter) : base(formatter)
        {
        }

        public override Result Log(LogMessage logMessage)
        {
            return base.Log(logMessage).OnSuccess(() =>
            {
                return Formatter.Apply(logMessage)
                    .OnSuccess(messageFormatted => { Console.WriteLine(messageFormatted); });
            });
        }
    }
}

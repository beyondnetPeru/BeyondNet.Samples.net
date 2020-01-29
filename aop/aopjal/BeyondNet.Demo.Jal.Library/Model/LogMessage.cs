using System;
using System.Collections.Generic;

namespace BeyondNet.Demo.Jal.Library.Model
{
    public class LogMessage
    {
        public DateTime DateTime { get; set; }
        public LogLevel Level { get; set; }
        public string Message { get; set; }

        public LogMessage(LogLevel level, string message, DateTime dateTime)
        {
            Level = level;
            Message = message;
            DateTime = dateTime;
        }
    }
}

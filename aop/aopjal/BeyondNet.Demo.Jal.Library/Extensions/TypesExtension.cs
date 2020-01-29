using BeyondNet.Demo.Jal.Library.Model;

namespace BeyondNet.Demo.Jal.Library.Extensions
{
    public static class TypesExtension
    {
        public static LoggerType GetLogEnum(this string value)
        {
            var result = LoggerType.None;

            switch (value.ToUpper())
            {
                case "CONSOLE":
                    result = LoggerType.Console;
                    break;
                case "DATABASE":
                    result = LoggerType.Database;
                    break;
                case "FILE":
                    result = LoggerType.File;
                    break;
            }

            return result;
        }

        public static LogLevel GetLevelEnum(this string value)
        {
            var result = LogLevel.None;

            switch (value.ToUpper())
            {
                case "MESSAGE":
                    result = LogLevel.Message;
                    break;
                case "WARNING":
                    result = LogLevel.Warning;
                    break;
                case "ERROR":
                    result = LogLevel.Error;
                    break;
            }

            return result;
        }
    }
}

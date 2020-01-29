using System;

namespace BeyondNet.App.Ums.Helpers.Impl
{
    //TODO: I should include the userName logged and register that in the UserNameCreated 
    public class AuditHelper
    {
        public static string MachineName => Environment.MachineName;
        public static string UserName => string.Empty;
        public static DateTime UtcDateTime => DateTime.UtcNow;
        public static Int64 TimeSpan => (Int64) new TimeSpan().TotalMinutes;
    }
}

using System;
using System.Collections.Generic;

namespace NotifyServer.Model
{
    public class NotifyAdd
    {
        public string UserName { get; set; }
        public string NotificationType { get; set; }
        public string ProfileId { get; set; }
        public string Message { get; set; }
        public string Hyperlink { get; set; }
        public DateTime DateTime { get; set; }
        public bool Read { get; set; }
        public IDictionary<string, object> Parameters { get; set; }
        public string RawMessages { get; set; }
    }
}

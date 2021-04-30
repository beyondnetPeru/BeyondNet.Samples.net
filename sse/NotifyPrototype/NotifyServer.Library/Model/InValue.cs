using System;
using System.Collections.Generic;

namespace NotifyServer.Library.Model
{
    public class InValue
    {
        public string NotificationType { get; set; }
        public string ProfileId { get; set; }
        public string User { get; set; }
        public string Message { get; set; }
        public string Hyperlink { get; set; }
        public bool Read { get; set; }
        public IDictionary<string, object> Parameters { get; set; }
    }
}

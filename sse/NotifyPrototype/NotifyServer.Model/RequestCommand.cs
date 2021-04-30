using System.Collections.Generic;

namespace NotifyServer.Model
{
    public class RequestCommand
    {
        public string ProfileId { get; set; }
        public string UserName { get; set; }
        public string NotificationType { get; set; }
        public string Message { get; set; }
        public string Hyperlink { get; set; }
        public bool Read { get; set; }
        public IDictionary<string, object> Parameters { get; set; }
    }
}

using System.Collections.Concurrent;
using System.Collections.Generic;

namespace NotifyServer.Model
{
    public class ResponseMessage
    {
        public int Code { get; set; }
        public IDictionary<string, object> Data { get; set; }
        public IDictionary<string, object> Errors { get; set; }

        public ResponseMessage()
        {
            Data =  new ConcurrentDictionary<string, object>();
            Errors = new ConcurrentDictionary<string, object>();
        }
    }
}

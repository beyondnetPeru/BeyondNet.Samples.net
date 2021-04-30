using System;
using System.Collections.Generic;

namespace NotifyServer.Library.Model
{
    public class Result
    {
        public int ResponseCode { get; set; }
        public IDictionary<string, object> Data { get; set; }
        public IDictionary<string, object> Errors { get; set; }
    }
}

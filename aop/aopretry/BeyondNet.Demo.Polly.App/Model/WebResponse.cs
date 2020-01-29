using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BeyondNet.Demo.Polly.App.Model
{
    public class WebResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Content { get; set; }

        public string StatusDescription { get; set; }
    }
}

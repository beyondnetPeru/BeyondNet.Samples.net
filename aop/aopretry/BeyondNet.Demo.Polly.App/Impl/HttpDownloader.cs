using System.IO;
using System.Net;

namespace BeyondNet.Demo.Polly.App.Impl
{
    public class HttpDownloader
    {
        public Model.WebResponse GetFile(string uri)
        {
            var webRequest = WebRequest.Create(uri) as HttpWebRequest;
            Model.WebResponse webResponse;
            
            using (var request = webRequest.GetResponse() as HttpWebResponse)
            {
                var content = ReadContent(request);

                webResponse = new Model.WebResponse()
                {
                    StatusCode = request.StatusCode,
                    StatusDescription = request.StatusDescription,
                    Content = content
                };
            }

            return webResponse;
        }

        private string ReadContent(HttpWebResponse webResponse)
        {
            using (var stream = webResponse.GetResponseStream())
            {
                if (stream == null) return string.Empty;

                var streamReader = new StreamReader(stream);

                return streamReader.ReadToEnd();
            }
        }
    }
}

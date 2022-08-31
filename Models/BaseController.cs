using System.Net;

namespace test6._0.Models
{
    public abstract class BaseController
    {
        public HttpListenerRequest Request { get; set; }
        public HttpListenerResponse Response { get; set; }

    }
}

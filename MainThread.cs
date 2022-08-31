using test6._0.Models;

namespace test6._0
{
    /// <summary>
    /// Class where main thread will go after configuration
    /// </summary>
    internal static class MainThread
    {
        public static async Task Start(MyWebHost webHost) 
        {
            Console.WriteLine("Listening on: " + HttpConfigure.BuildAppUrl());
            webHost.httpListener.Start();
            while (webHost.IsListening) 
            {
                var context = await webHost.httpListener.GetContextAsync();

                Stream stream = context.Response.OutputStream;

                byte[] bytes = SerializeHelper.SerializeObject("Page not found.");

                string url = context.Request.Url.ToString();

                webHost.Routes.TryGetValue(url, out Route route); 

                if (route != null) 
                {
                    var method = route.Methods.First(m => m.Name == url.Split('/')[^1]);

                    var attribute = method.CustomAttributes.FirstOrDefault(att => att.AttributeType.IsSubclassOf(typeof(HttpBaseAttribute)));

                    if (attribute == null || 
                        attribute.AttributeType.Name == nameof(HttpGetAttribute) && context.Request.HttpMethod == "POST" ||
                        attribute.AttributeType.Name == nameof(HttpPostAttribute) && context.Request.HttpMethod == "GET") 
                    {
                        continue;
                    }

                    var instanceAsBase = (BaseController)Activator.CreateInstance(route.Controller);
                    instanceAsBase.Request = context.Request;
                    instanceAsBase.Response = context.Response;
                    var returned = method.Invoke(instanceAsBase, new object[] { });

                    if (HttpConfigure.CanReturnTypes) 
                    {
                        bytes = SerializeHelper.SerializeObject(returned);
                    }
                }
                stream.Write(bytes, 0, bytes.Length);

                stream.Close();
            }

            webHost.httpListener.Stop();
            webHost.httpListener.Close();
        }
    }
}

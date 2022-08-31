using System.Net;
using System.Reflection;

namespace test6._0.Models
{
    internal static class HttpSetupHelper
    {
        /// <summary>
        /// Setups and adds routes to the <paramref name="listener"/>
        /// </summary>
        /// <param name="listener"></param>
        public static Route[] ConfigureRoutes(HttpListener listener) 
        {
            var controllers = MyWebHost.GetTypes(type =>
                type.IsSubclassOf(typeof(BaseController)) && !type.IsAbstract
            );

            Route[] routes = new Route[controllers.Length];
            for (int i = 0; i < controllers.Length; i++)
            {
                routes[i] = new Route
                {
                    ControllerName = controllers[i].Name.Replace("Controller", ""),
                    Controller = controllers[i],
                    Methods = controllers[i].GetMethods(BindingFlags.Instance | BindingFlags.Public)
                        .Where(i => !i.IsVirtual && i.Name != "GetType")
                        .ToArray()
                };
            }

            for (int i = 0; i < routes.Length; i++)
            {
                foreach (var url in routes[i].BuildUrls())
                {
                    listener.Prefixes.Add(url + "/");
                }
            }
            return routes;
        }
        /// <summary>
        /// Setups and adds routes to the <paramref name="listener"/> and to the <paramref name="dictionary"/>
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="dictionary"></param>
        public static void ConfigureRoutesToDictionary(HttpListener listener, Dictionary<string, Route> dictionary)
        {
            var controllers = MyWebHost.GetTypes(type =>
                type.IsSubclassOf(typeof(BaseController)) && !type.IsAbstract
            );

            Route[] routes = new Route[controllers.Length];
            for (int i = 0; i < controllers.Length; i++)
            {
                routes[i] = new Route
                {
                    ControllerName = controllers[i].Name.Replace("Controller", ""),
                    Controller = controllers[i],
                    Methods = controllers[i].GetMethods(BindingFlags.Instance | BindingFlags.Public)
                        .Where(i => !i.IsVirtual && i.Name != "GetType")
                        .ToArray()
                };
            }

            for (int i = 0; i < routes.Length; i++)
            {
                foreach (var url in routes[i].BuildUrls())
                {
                    listener.Prefixes.Add(url + "/");
                    dictionary[url] = routes[i];
                }
            }
        }
    }
}

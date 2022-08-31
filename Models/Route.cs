using System.Reflection;

namespace test6._0.Models
{
    public class Route
    {

        public string ControllerName;
        public Type Controller;
        public MethodInfo[] Methods;
        /// <summary>
        /// Builds url strings by current route
        /// </summary>
        /// <returns></returns>
        public string[] BuildUrls() 
        {
            string[] routes = new string[Methods.Length];
            
            for (int i = 0; i < routes.Length; i++)
            {
                routes[i] = $"{HttpConfigure.Protocol}://{HttpConfigure.DomainName}:{HttpConfigure.Port}/{ControllerName}/{Methods[i].Name}";
            }
            return routes;
        }
    }
}

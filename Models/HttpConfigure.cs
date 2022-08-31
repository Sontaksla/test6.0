
namespace test6._0.Models
{
    public class HttpConfigure
    {
        static internal bool ControllersRouted { get; private set; }
        static internal bool CanReturnTypes { get; private set; }

        static internal string DomainName { get; private set; }
        static internal ushort Port { get; private set; }
        static internal string Protocol { get; private set; }
        static HttpConfigure()
        {
            Protocol = "http";
        }
        /// <summary>
        /// Builds app url
        /// </summary>
        /// <returns></returns>
        public static string BuildAppUrl() 
        {
            return $"{Protocol}://{DomainName}:{Port}";
        }
        public void SetDomain(string domainName, ushort port) 
        {
            DomainName = domainName;
            Port = port;
        }
        public void RouteControllers() 
        {
            ControllersRouted = true;
        }
        /// <summary>
        /// Will controller's methods return values automatically be added in Response
        /// </summary>
        public void ReturnTypes() 
        {
            CanReturnTypes = true;
        }
        public void AddHttps() 
        {
            Protocol = "https";
        }
    }
}

using System.Reflection;
using test6._0.Models;
using System.Net;

namespace test6._0 
{
    public sealed class MyWebHost
    {
        internal HttpConfigure httpConfigure;
        internal HttpListener httpListener;
        internal Dictionary<string, Route> Routes;
        internal static MyWebHost Instance = new MyWebHost();
        internal Type type;
        internal bool IsListening;
        public MyWebHost()
        {

            httpConfigure = new HttpConfigure();
            httpListener = new HttpListener();

            Routes = new Dictionary<string, Route>();
        }
        /// <summary>
        /// <para>Starts up the api.</para>  
        /// <paramref name="type"/> must be any type in your assembly.
        /// </summary>
        /// <param name="type"></param>
        /// <exception cref="NullReferenceException"></exception>
        public static async Task Start(Type type)
        {
            Instance.type = type;
            Instance.IsListening = true;

            //Getting StartupConfig class
            var configType = GetTypes(type => type.IsSubclassOf(typeof(StartupConfig)) && !type.IsAbstract)
                .FirstOrDefault();
            if (configType == null) throw new NullReferenceException("No startup config found. Please, create your startup config.");

            StartupConfig config = Activator.CreateInstance(configType) as StartupConfig;

            //Configuration
            config.Configure(Instance.httpConfigure);
            Configure();
            //Starting main work
            await MainThread.Start(Instance);
        }
        /// <summary>
        /// Stops listening to any requests and disposes classes
        /// </summary>
        public static void Stop()
        {
            Console.WriteLine("Shutting down the application...");
            Instance.IsListening = false;
        }
        private static void Configure()
        {
            if (HttpConfigure.ControllersRouted)
            {
                HttpSetupHelper.ConfigureRoutesToDictionary(Instance.httpListener, Instance.Routes);
            }

        }
        // Gets specified types in current assembly
        public static Type[] GetTypes(Func<Type, bool> predicate)
        {
            Type[] types = Assembly.GetAssembly(Instance.type).GetTypes()
                .Where(type => predicate(type)).ToArray();

            return types;
        }
    }
}

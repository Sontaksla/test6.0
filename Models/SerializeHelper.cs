using Newtonsoft.Json;
using System.Text;

namespace test6._0.Models
{
    internal static class SerializeHelper
    {
        public static byte[] SerializeObject(object obj)
        {

            string jsonStr = JsonConvert.SerializeObject(obj);

            return Encoding.UTF8.GetBytes(jsonStr);
        }
        public static T DeserializeObject<T>(byte[] bytes) 
        {
            string jsonStr = Encoding.UTF8.GetString(bytes);
            Console.WriteLine("JSON STRING: "+ jsonStr);
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }
    }
}

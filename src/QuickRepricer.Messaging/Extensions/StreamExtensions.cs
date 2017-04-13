using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace QuickRepricer.Messaging.Extensions
{
    public static class StreamExtensions
    {
        public static string ReadToEnd(this Stream stream)
        {
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }


        public static async Task<string> ReadToEndAsync(this Stream stream)
        {
            var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }
        

        public static T ReadFromJson<T>(this Stream stream)
        {
            var json = stream.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(json);
        }


        public static async Task<T> ReadFromJsonAsync<T>(this Stream stream)
        {
            var json = await stream.ReadToEndAsync();
            return await JsonConvert.DeserializeObjectAsync<T>(json);
        }


        public static object ReadFromJson(this Stream stream, string messageType)
        {
            var type = Type.GetType(messageType);
            var json = stream.ReadToEnd();
            return JsonConvert.DeserializeObject(json, type);
        }

        public static async Task<object> ReadFromJsonAsync(this Stream stream, string messageType)
        {
            var type = Type.GetType(messageType);
            var json = await stream.ReadToEndAsync();
            var settings = new JsonSerializerSettings();
           
            return await JsonConvert.DeserializeObjectAsync(json, type, settings);
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickRepricer.Messaging.Extensions
{
    public static class StringExtensions
    {
        public static object ReadFromJson(this string json, string messageType)
        {
            var type = Type.GetType(messageType);
            return JsonConvert.DeserializeObject(json, type);
        }


        public static async Task<object> ReadFromJsonAsync(this string json, string messageType)
        {
            var type = Type.GetType(messageType);
            return await JsonConvert.DeserializeObjectAsync(json, type, new JsonSerializerSettings());
        }


        public static T ReadFromJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static async Task<T> ReadFromJsonAsync<T>(this string json)
        {
            return await JsonConvert.DeserializeObjectAsync<T>(json);
        }
    }
}

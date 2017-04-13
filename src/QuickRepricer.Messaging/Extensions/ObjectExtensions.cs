using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickRepricer.Messaging.Extensions
{
    public static class ObjectExtensions
    {
        public static string GetMessageType(this object obj)
        {
            return obj.GetType().AssemblyQualifiedName;
        }

        public static string ToJsonString(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static async Task<string> ToJsonStringAsync(this object obj)
        {
            return await JsonConvert.SerializeObjectAsync(obj);
        }

        public static Stream ToJsonStream(this object obj)
        {
            var json = obj.ToJsonString();
            return new MemoryStream(Encoding.UTF8.GetBytes(json));
        }

        public static async Task<Stream> ToJsonStreamAsync(this object obj)
        {
            var json = await obj.ToJsonStringAsync();
            return new MemoryStream(Encoding.UTF8.GetBytes(json));
        }



    }
}

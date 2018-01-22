using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ASPNETCoreIdentitySample.Common.WebToolkit
{
    public static class MapperExtentions
    {
        public static async Task<T> DeserializeJsonBodyAsAsync<T>(this HttpContext context)
        {
            using (var bodyReader = new StreamReader(context.Request.Body, Encoding.UTF8))
            {
                var body = await bodyReader.ReadToEndAsync();
                context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));
                return JsonConvert.DeserializeObject<T>(body);
            }
        }

        public static async Task<T> DeserializeJsonBodyAsAsync<T>(this HttpRequest request)
        {
            var json = await request.ReadBodyAsString();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static async Task<string> ReadBodyAsString(this HttpRequest request)
        {
            using (var bodyReader = new StreamReader(request.Body, Encoding.UTF8))
            {
                var body = await bodyReader.ReadToEndAsync();
                request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));
                return body;
            }
        }

        public static async Task<Dictionary<string, string>> DeserializeJsonBodyAsDictionaryAsync(this HttpContext httpContext)
        {
            using (var bodyReader = new StreamReader(httpContext.Request.Body, Encoding.UTF8))
            {
                var body = await bodyReader.ReadToEndAsync();
                httpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(body);
            }
        }
    }
}
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETCoreIdentitySample.Common.WebToolkit
{
    public static class MapperExtentions
    {
        public static async Task<T> DeserializeJsonBodyAsAsync<T>(this HttpContext context)
        {
            using (var bodyReader = new StreamReader(context.Request.Body))
            {
                var body = await bodyReader.ReadToEndAsync().ConfigureAwait(false);
                context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));
                return JsonConvert.DeserializeObject<T>(body);
            }
        }

        public static async Task<T> DeserializeJsonBodyAsAsync<T>(this HttpRequest request)
        {
            var json = await request.ReadBodyAsString().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static async Task<string> ReadBodyAsString(this HttpRequest request)
        {
            using (var bodyReader = new StreamReader(request.Body))
            {
                var body = await bodyReader.ReadToEndAsync().ConfigureAwait(false);
                request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));
                return body;
            }
        }
    }
}
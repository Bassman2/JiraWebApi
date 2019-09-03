using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace JiraWebApiShare.Internal
{
    internal static class HttpContentExtensions
    {
        public static Task<T> ReadAsAsync<T>(this HttpContent content)
        {
            return Task.Run<T>(async () => 
                {
                    var options = new JsonSerializerOptions { AllowTrailingCommas = true, IgnoreNullValues = true };
                    return JsonSerializer.Deserialize<T>(await content.ReadAsStringAsync(), options);
                });
        }
    }
}

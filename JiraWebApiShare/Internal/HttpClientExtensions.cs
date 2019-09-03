using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JiraWebApiShare.Internal
{
    internal static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string requestUri, T value)
        {
            string str = JsonSerializer.Serialize<T>(value);
            HttpContent content = new StringContent(str, Encoding.UTF8, "application/json");
            return client.PostAsync(requestUri, content);
        }

        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, string requestUri, T value)
        {
            string str = JsonSerializer.Serialize<T>(value);
            HttpContent content = new StringContent(str, Encoding.UTF8, "application/json");
            return client.PutAsync(requestUri, content);
        }

    }
}

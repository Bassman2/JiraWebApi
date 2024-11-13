using JiraWebApi;
using System.Text.Json.Serialization;

namespace JiraWebApi.Internal
{
    internal class SessionPostResult
    {

        [JsonPropertyName("session")]
        public Session Session { get; set; }

        [JsonPropertyName("loginInfo")]
        public LoginInfo LoginInfo { get; set; }

    }
}

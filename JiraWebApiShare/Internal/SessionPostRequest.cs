using System.Text.Json.Serialization;

namespace JiraWebApi.Internal
{
    internal class SessionPostRequest
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}

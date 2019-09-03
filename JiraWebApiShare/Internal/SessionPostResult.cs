using System.Text.Json.Serialization;

namespace JiraWebApi.Internal
{
    internal class SessionPostResult
    {
        internal class Session_
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("value")]
            public string Value { get; set; }

        }

        [JsonPropertyName("session")]
        public Session_ Session { get; set; }

        [JsonPropertyName("loginInfo")]
        public LoginInfo LoginInfo { get; set; }

    }
}

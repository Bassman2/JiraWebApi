using System.Text.Json.Serialization;

namespace JiraWebApi.Internal
{
    internal class VersionMoveAfterPostRequest
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("after")]
        public string After { get; set; }

    }
}

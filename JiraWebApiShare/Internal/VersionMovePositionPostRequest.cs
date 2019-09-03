using System.Text.Json.Serialization;

namespace JiraWebApi.Internal
{
    internal class VersionMovePositionPostRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Only First, Last, Earlier or Later allowed.</remarks>
        [JsonPropertyName("position")]
        public Position Position { get; set; }
    }
}

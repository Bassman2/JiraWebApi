using System.Text.Json.Serialization;

namespace JiraWebApi
{
    /// <summary>
    /// Progress of an issue.
    /// </summary>
    public sealed class Progress
    {
        /// <summary>
        /// Initializes a new instance of the Progress class.
        /// </summary>
        private Progress()
        { }

        /// <summary>
        /// Progress values.
        /// </summary>
        [JsonPropertyName("progress")]
        public int Value { get; private set; }
        
        /// <summary>
        /// Total progress value.
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; private set; }

        /// <summary>
        /// Progress percentage.
        /// </summary>
        [JsonPropertyName("percent")]
        public int Percent { get; private set; }
    }
}

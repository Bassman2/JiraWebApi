using System.Text.Json.Serialization;

namespace JiraWebApi
{
    /// <summary>
    /// Represent an Application for remote links. 
    /// </summary>
    public sealed class Application
    {
        /// <summary>
        /// Initializes a new instance of the Application class.
        /// </summary>
        public Application()
        { }

        /// <summary>
        /// Type of the application.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Name of the application.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}

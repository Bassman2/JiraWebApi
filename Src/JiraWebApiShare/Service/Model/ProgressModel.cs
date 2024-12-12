namespace JiraWebApi.Service.Model;

/// <summary>
/// Progress of an issue.
/// </summary>
internal class ProgressModel
{
    /// <summary>
    /// Progress values.
    /// </summary>
    [JsonPropertyName("progress")]
    public int Value { get; set; }
    
    /// <summary>
    /// Total progress value.
    /// </summary>
    [JsonPropertyName("total")]
    public int Total { get; set; }

    /// <summary>
    /// Progress percentage.
    /// </summary>
    [JsonPropertyName("percent")]
    public int Percent { get; set; }
}

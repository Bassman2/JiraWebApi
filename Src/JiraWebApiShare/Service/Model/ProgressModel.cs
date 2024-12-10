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

namespace JiraWebApi.Service.Model;

internal class SearchResult
{
    /// <summary>
    /// Name of the classes which should be expanded.
    /// </summary>
    [JsonPropertyName("expand")]
    public string? Expand { get; private set; }
    
    [JsonPropertyName("startAt")]
    public int StartAt { get; private set; }

    [JsonPropertyName("maxResults")]
    public int MaxResults { get; private set; }

    [JsonPropertyName("total")]
    public int Total { get; private set; }

    [JsonPropertyName("issues")]
    public IEnumerable<Issue>? Issues { get; private set; }
}

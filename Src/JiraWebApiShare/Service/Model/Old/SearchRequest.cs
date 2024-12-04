namespace JiraWebApi.Service.Model;

internal class SearchRequest
{
    [JsonPropertyName("jql")]
    public string? Jql { private get; set; }

    [JsonPropertyName("startAt")]
    public int StartAt { private get; set; }

    [JsonPropertyName("maxResults")]
    public int MaxResults { private get; set; }

    [JsonPropertyName("fields")]
    public string? Fields { private get; set; }

    /// <summary>
    /// Name of the classes which should be expanded.
    /// </summary>
    [JsonPropertyName("expand")]
    public string? Expand { private get; set; }
}

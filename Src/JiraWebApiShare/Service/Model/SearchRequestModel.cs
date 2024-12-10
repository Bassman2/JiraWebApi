namespace JiraWebApi.Service.Model;

internal class SearchRequestModel
{
    [JsonPropertyName("jql")]
    public string? Jql { get; set; }

    [JsonPropertyName("startAt")]
    public int StartAt { get; set; }

    [JsonPropertyName("maxResults")]
    public int MaxResults { get; set; }

    [JsonPropertyName("fields")]
    public string? Fields { get; set; }

    /// <summary>
    /// Name of the classes which should be expanded.
    /// </summary>
    [JsonPropertyName("expand")]
    public string? Expand { get; set; }
}

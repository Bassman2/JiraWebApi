namespace JiraWebApi.Service.Model;

internal class CreateIssueModel
{
    /// <summary>
    /// Name of the classes which should be expanded.
    /// </summary>
    [JsonPropertyName("update")]
    public Dictionary<string, object>? Update { get; set; }

    /// <summary>
    /// Name of the classes which should be expanded.
    /// </summary>
    [JsonPropertyName("fields")]
    public Dictionary<string, object>? Fields { get; set; }
}

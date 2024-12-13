namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of an JIRA issue.
/// </summary>
/// <remarks>
/// In the Issue class some properties are for LINQ use only and are not read or writeable. 
/// See the documentation of the property to get detailed information.
/// </remarks>
internal class IssueModel
{
    [JsonPropertyName("self")]
    public Uri? Self { get; set; }

    /// <summary>
    /// Id of the JIRA issue.
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    /// Summary of the JIRA issue.
    /// </summary>
    [JsonPropertyName("key")]
    public string? Key { get; set; }

    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>

    [JsonPropertyName("fields")]
    public Dictionary<string, JsonElement?>? Fields { get; set; }
}



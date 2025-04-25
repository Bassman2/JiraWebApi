namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of a JIRA issue transition.
/// </summary>
internal class TransitionModel
{
    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    [JsonPropertyName("self")]
    public Uri? Self { get; set; }

    /// <summary>
    /// Id of the JIRA item.
    /// </summary>
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Name of the issue transition.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Result status of this issue transition.
    /// </summary>
    [JsonPropertyName("to")]
    public StatusModel? To { get; set; }

    // /// <summary>
    // /// Fields which are editable during this transition.
    // /// </summary>
    //[JsonPropertyName("fields")]
    //public Fields? Fields { get; set; }
}

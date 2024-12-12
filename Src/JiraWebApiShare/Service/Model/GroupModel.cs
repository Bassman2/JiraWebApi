namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of a JIRA group.
/// </summary>
internal class GroupModel
{
    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    [JsonPropertyName("self")]
    public Uri? Self { get; set; }

    /// <summary>
    /// Name of the JIRA group.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Users of the JIRA group.
    /// </summary>
    [JsonPropertyName("users")]
    public UsersListModel? Users { get; set; }

    /// <summary>
    /// Names of the expanded fields.
    /// </summary>
    [JsonPropertyName("expand")]
    public string? Expand { get; set; }
}

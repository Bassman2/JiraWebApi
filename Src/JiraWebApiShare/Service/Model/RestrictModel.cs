namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of a restrict.
/// </summary>
internal sealed class RestrictModel
{
    

    /// <summary>
    /// Groups of the restriction.
    /// </summary>
    [JsonPropertyName("groups")]
    public IEnumerable<GroupModel>? Groups { get; set; }

    /// <summary>
    /// Permissions of the restriction.
    /// </summary>
    [JsonPropertyName("permissions")]
    public IEnumerable<Permission>? Permission { get; set; }
}

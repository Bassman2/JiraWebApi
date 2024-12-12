namespace JiraWebApi.Service.Model;


/// <summary>
/// Representation of a JIRA permission.
/// </summary>
internal class PermissionModel
{
    /// <summary>
    /// Id of the permission.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Key of the permission.
    /// </summary>
    [JsonPropertyName("key")]
    public string? Key { get; set; }
}

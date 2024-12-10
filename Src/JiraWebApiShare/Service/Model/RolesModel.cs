namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of JIRA roles. 
/// </summary>
internal class RolesModel
{
    /// <summary>
    /// E-mail of the JIRY project.
    /// </summary>
    [JsonPropertyName("Developers")]
    public Uri? Developers { get; set; }
}

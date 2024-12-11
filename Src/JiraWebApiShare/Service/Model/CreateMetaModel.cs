namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of meta information for issue creation.
/// </summary>
internal class CreateMetaModel
{
    /// <summary>
    /// Name of the classes which should be expanded.
    /// </summary>
    [JsonPropertyName("expand")]
    public string? Expand { get; set; }

    /// <summary>
    /// Projects for which tickets can be created by the user.
    /// </summary>
    [JsonPropertyName("projects")]
    public IEnumerable<ProjectModel>? Projects { get; set; }
}

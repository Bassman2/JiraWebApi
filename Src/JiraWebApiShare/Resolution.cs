namespace JiraWebApi;

/// <summary>
/// Represents a JIRA issue resolution, which defines how an issue was resolved.
/// </summary>
public class Resolution
{
    internal Resolution(ResolutionModel model)
    {
        Self = model.Self;
        Id = model.Id;
        Name = model.Name;
        Description = model.Description;
        IconUrl = model.IconUrl;
    }
    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    public Uri? Self { get; }

    /// <summary>
    /// Id of the JIRA item.
    /// </summary>
    public int? Id { get; }

    /// <summary>
    /// Name of the JIRA item.
    /// </summary>
    public string? Name { get; }

    /// <summary>
    /// Description of the JIRA resolution.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Description of the JIRA resolution.
    /// </summary>
    public Uri? IconUrl { get; }
}
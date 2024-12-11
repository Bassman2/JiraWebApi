namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA issue priority. 
/// </summary>
[DebuggerDisplay("{Id}, {Name}, {Description}")]
public class Priority
{
    internal Priority(PriorityModel model)
    {
        Self = model.Self;
        Id = model.Id;
        Name = model.Name;
        Description = model.Description;
        IconUrl = model.IconUrl;
        StatusColor = model.StatusColor;
    }

    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    public string? Self { get; }

    /// <summary>
    /// Id of the JIRA item.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Name of the JIRA item.
    /// </summary>
    public string? Name { get; }

    /// <summary>
    /// Description of the JIRA priority.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Url of the icon of the JIRA priority.
    /// </summary>
    public Uri? IconUrl { get; }

    /// <summary>
    /// Status color of the JIRA priority.
    /// </summary>
    public string? StatusColor { get; }
}

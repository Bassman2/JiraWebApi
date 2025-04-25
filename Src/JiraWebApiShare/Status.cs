namespace JiraWebApi;

/// <summary>
/// Represents a JIRA issue status, including its metadata and associated status category.
/// </summary>
public class Status
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Status"/> class using the provided <see cref="StatusModel"/>.
    /// </summary>
    /// <param name="model">The <see cref="StatusModel"/> containing the data to initialize the status.</param>
    internal Status(StatusModel model)
    {
        Self = model.Self;
        Id = model.Id;
        Name = model.Name;
        Description = model.Description;
        IconUrl = model.IconUrl;
        StatusCategory = model.StatusCategory.CastModel<StatusCategory>();
    }

    /// <summary>
    /// Gets the URL of the JIRA REST resource for this status.
    /// </summary>
    public Uri? Self { get; }

    /// <summary>
    /// Gets the unique identifier of the status.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets the name of the status.
    /// </summary>
    public string? Name { get; }

    /// <summary>
    /// Gets the description of the status.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Gets the URL of the icon representing the status.
    /// </summary>
    public Uri? IconUrl { get; }

    /// <summary>
    /// Gets or sets the category associated with the status.
    /// </summary>
    public StatusCategory? StatusCategory { get; set; }
}

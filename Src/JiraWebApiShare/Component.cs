namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA component. 
/// </summary>
[DebuggerDisplay("{Name}")]
public class Component 
{
    /// <summary>
    /// Initializes a new instance of the Component class.
    /// </summary>
    internal Component(ComponentModel model)
    {
        Self = model.Self!;
        Id = model.Id;
        Name = model.Name;
        Description = model.Description;

        AssigneeType = model.AssigneeType;
        Assignee = model.Assignee.CastModel<User>();
        RealAssigneeType = model.RealAssigneeType;
        RealAssignee = model.RealAssignee.CastModel<User>(); 
        IsAssigneeTypeValid = model.IsAssigneeTypeValid;
        ProjectKey = model.ProjectKey;
        ProjectId = model.ProjectId;
        Archived = model.Archived;
        Deleted = model.Deleted;
    }

    /// <summary>
    /// Gets the URL of the JIRA REST resource for this component.
    /// </summary>
    public Uri? Self { get; }

    /// <summary>
    /// Gets the unique identifier of the component.
    /// </summary>
    public int Id { get; }

    //public string Key { get; }

    /// <summary>
    /// Gets or sets the name of the component.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the component.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the description of the component.
    /// </summary>
    public string? AssigneeType { get; set; }

    /// <summary>
    /// Gets or sets the user assigned to the component.
    /// </summary>
    public User? Assignee { get; set; }

    /// <summary>
    /// Gets or sets the user assigned to the component.
    /// </summary>
    public string? RealAssigneeType { get; set; }

    /// <summary>
    /// Gets or sets the user assigned to the component.
    /// </summary>
    public User? RealAssignee { get; set; }

    /// <summary>
    /// Gets or sets the user assigned to the component.
    /// </summary>
    public bool IsAssigneeTypeValid { get; set; }

    /// <summary>
    /// Gets or sets the key of the project to which the component belongs.
    /// </summary>
    public string? ProjectKey { get; set; }

    /// <summary>
    /// Gets or sets the ID of the project to which the component belongs.
    /// </summary>
    public int ProjectId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the component is archived.
    /// </summary>
    public bool Archived { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the component is deleted.
    /// </summary>
    public bool Deleted { get; set; }
}

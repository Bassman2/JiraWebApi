namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA component. 
/// </summary>
[DebuggerDisplay("{Name}")]
public sealed class Component 
{
    /// <summary>
    /// Initializes a new instance of the Component class.
    /// </summary>
    internal Component(ComponentModel model)
    {
        Self = model.Self;
        Id = model.Id;
        Name = model.Name;

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

    public string Self { get; }

    public int Id { get; }

    //public string Key { get; }

    public string? Name { get; set; }
       
    public string? AssigneeType { get; set; }

    public User? Assignee { get; set; }

    public string? RealAssigneeType { get; set; }

    public User? RealAssignee { get; set; }

    public bool IsAssigneeTypeValid { get; set; }

    /// <summary>
    /// Project to which the version belongs.
    /// </summary>
    public string? ProjectKey { get; set; }

    /// <summary>
    /// Project to which the version belongs.
    /// </summary>
    public int ProjectId { get; set; }


    public bool Archived { get; set; }


    public bool Deleted { get; set; }
}

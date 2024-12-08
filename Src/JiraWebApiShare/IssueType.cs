namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA issue type. 
/// </summary>
[DebuggerDisplay("{Id} {Name}")]
public sealed class IssueType 
{
    ///// <summary>
    ///// Initializes a new instance of the IssueType class.
    ///// </summary>
    internal IssueType(IssueTypeModel model)
    {
        Self = model.Self;
        Id = model.Id;
        Name = model.Name;
        Description = model.Description;
        IconUrl = model.IconUrl;
        IsSubtask = model.IsSubtask;
        AvatarId = model.AvatarId;
       //Fields = model.Fields
    }

    public string? Self { get; }

    /// <summary>
    /// Id of the JIRA item.
    /// </summary>
    
    public string? Id { get; }

    /// <summary>
    /// Name of the issue type.
    /// </summary>
    public string? Name { get; }

    /// <summary>
    /// Description of the JIRA issue type.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Url of the issue type icon.
    /// </summary>
    public Uri? IconUrl { get; }

    /// <summary>
    /// Signals if the issue type is a subtask issue type.
    /// </summary>
    public bool? IsSubtask { get; }

    public int? AvatarId { get; }

    ///// <summary>
    ///// Name of the classes which should be expanded.
    ///// </summary>
    //public string? Expand { get; internal init; }

    ///// <summary>
    ///// Fields which are editable for this issue type. Used by meta information.
    ///// </summary>
    ///// <remarks>Only filled by <see cref="Jira.GetCreateMetaAsync">GetCreateMetaAsync</see> and <see cref="Jira.GetEditMetaAsync">GetEditMetaAsync</see>.</remarks>
    //public Fields? Fields { get; internal init; }
}

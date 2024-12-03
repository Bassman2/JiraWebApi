namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of a JIRA issue type. 
/// </summary>
internal class IssueTypeModel : ComparableElementModel
{
    /// <summary>
    /// Initializes a new instance of the IssueType class.
    /// </summary>

    /// <summary>
    /// Description of the JIRA issue type.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; private set; }

    /// <summary>
    /// Url of the issue type icon.
    /// </summary>
    [JsonPropertyName("iconUrl")]
    public Uri? IconUrl { get; private set; }

    /// <summary>
    /// Signals if the issue type is a subtask issue type.
    /// </summary>
    [JsonPropertyName("subtask")]
    public bool IsSubtask { get; private set; }

    /// <summary>
    /// Name of the classes which should be expanded.
    /// </summary>
    [JsonPropertyName("expand")]
    public string? Expand { get; private set; }

    /// <summary>
    /// Fields which are editable for this issue type. Used by meta information.
    /// </summary>
    /// <remarks>Only filled by <see cref="Jira.GetCreateMetaAsync">GetCreateMetaAsync</see> and <see cref="Jira.GetEditMetaAsync">GetEditMetaAsync</see>.</remarks>
    //[JsonPropertyName("fields")]
    //public Fields? Fields { get; private set; }

    ///// <summary>
    ///// Returns a string that represents the issue type.
    ///// </summary>
    ///// <returns>A string that represents the issue type.</returns>
    //public override string ToString()
    //{
    //    return this.Name;
    //}

    public static implicit operator IssueType?(IssueTypeModel? model)
    {
        return model is null ? null : new IssueType()
        {
            Self = model.Self,
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            IconUrl = model.IconUrl,
            IsSubtask = model.IsSubtask,
            Expand = model.Expand,
            //Fields = model.Fields
        };
    }
}

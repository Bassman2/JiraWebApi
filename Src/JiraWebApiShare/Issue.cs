﻿using System.Text.Json.Serialization.Metadata;

namespace JiraWebApi;

/// <summary>
/// Representation of an JIRA issue.
/// </summary>
/// <remarks>
/// In the Issue class some properties are for LINQ use only and are not read or writeable. 
/// See the documentation of the property to get detailed information.
/// </remarks>
[DebuggerDisplay("{Id}, {Key}")]
public sealed class Issue 
{
    #region old

    //[DebuggerDisplay("{Id}, {Name}")]
    //private class CustomField
    //{
    //    public CustomField()
    //    { }

    //    public CustomField(Field field, CustomFieldValue value)
    //    {
    //        this.Id = field.Id;
    //        this.Name = field.Name;
    //        this.Value = value;
    //    }

    //    public string? Name { get; set; }
    //    public string? Id { get; set; }
    //    //public string Type { get; set; }
    //    public CustomFieldValue? Value { get; set; }
    //    public bool Changed { get; set; }

    //    ///// <summary>
    //    ///// Returns a string that represents the current object.
    //    ///// </summary>
    //    ///// <returns>A string that represents the current object.</returns>
    //    //public override string ToString()
    //    //{
    //    //    return string.Format("{0}, {1}", this.Id, this.Name);
    //    //}
    //}

    /// <summary>
    /// Initializes a new instance of the Issue class.
    /// </summary>
    //public Issue()
    //{
    //    //this.SerializeMode = SerializeMode.Link;
    //    this.customFields = new List<CustomField>();
    //}

    ///// <summary>
    ///// Initializes a new instance of the Issue class to create an issue link.
    ///// </summary>
    ///// <param name="issueKey">Key of the issue to link to.</param>
    //public Issue(string issueKey)
    //{
    //    //this.SerializeMode = SerializeMode.Link;
    //    this.Key = issueKey;
    //    this.customFields = new List<CustomField>();
    //}



    //private void AddCustomField<T>(Field fieldInfo, JToken value, CustomFieldType type)
    //{
    //    if (value.HasValues)
    //    {
    //        var val = value.ToObject<T>();
    //        this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue(type, val)));
    //    }
    //    else
    //    {
    //        this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue(type)));
    //    }
    //}

    //private void AddCustomFieldArray<T>(Field fieldInfo, JToken value, CustomFieldType type)
    //{
    //    if (value.HasValues)
    //    {
    //        var val = ((JArray)value).Select(v => ((JObject)v).ToObject<T>() ).ToArray();
    //        this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue(type, val)));
    //    }
    //    else
    //    {
    //        this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue(type)));
    //    }
    //}


    ///// <summary>
    ///// Support of the JQL 'issueHistory()' function in LINQ.
    ///// </summary>
    ///// <returns>Not used.</returns>
    //[JqlFunction("issueHistory")]
    //public static Issue[] IssueHistory()
    //{
    //    throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
    //}

    ///// <summary>
    ///// Support of the JQL 'linkedIssues()' function in LINQ.
    ///// </summary>
    ///// <returns>Not used.</returns>
    //[JqlFunction("linkedIssues")]
    //public static Issue[] LinkedIssues()
    //{
    //    throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
    //}

    ///// <summary>
    ///// Support of the JQL 'votedIssues()' function in LINQ.
    ///// </summary>
    ///// <returns>Not used.</returns>
    //[JqlFunction("votedIssues")]
    //public static Issue[] VotedIssues()
    //{
    //    throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
    //}

    ///// <summary>
    ///// Support of the JQL 'watchedIssues()' function in LINQ.
    ///// </summary>
    ///// <returns>Not used.</returns>
    //[JqlFunction("watchedIssues")]
    //public static Issue[] WatchedIssues()
    //{
    //    throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
    //}

    #endregion

    private readonly JiraService? service;

    internal Issue(JiraService service, IssueModel model)
    {
        this.service = service;
        Id = model.Id!;
        Key = model.Key!;
        Self = model.Self;

        if (model.Fields?.TryGetValue("project", out object? res) ?? false && res != null)
        {
            JsonElement element = (JsonElement)res!;

            ProjectModel? pm = JsonSerializer.Deserialize<ProjectModel>(element, SourceGenerationContext.Default.ProjectModel);


            Project = pm.CastModel<Project>(service);
        }                
    }

    #region fields

    /// <summary>
    /// Name of the classes which should be expanded.
    /// </summary>
    /// <remarks>Not useable by LINQ.</remarks>
    //public string? Expand { get; private set; }

    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    /// <remarks>Not useable by LINQ.</remarks>
    public Uri? Self { get; internal set; }

    /// <summary>
    /// Id of the JIRA issue.
    /// </summary>
    public string? Id { get; internal  set; }

    /// <summary>
    /// Summary of the JIRA issue.
    /// </summary>
    public string? Key { get; internal set; }

    /// <summary>
    /// Σ Progress
    /// </summary>
    //public Progress? AggregateProgress { get; private set; }

    /// <summary>
    /// Σ Remaining Estimate
    /// </summary>
    public long? AggregateTimeEstimate { get; private set; }

    /// <summary>
    /// Σ Original Estimate
    /// </summary>
    public long? AggregateTimeOriginalEstimate { get; private set; }

    /// <summary>
    /// Σ Time Spent
    /// </summary>
    public long? AggregateTimeSpent { get; private set; }

    //private void SetValue<T>(ref T value, T newValue, ref bool changed)
    //{
    //    if (value!.Equals(newValue))
    //    {
    //        value = newValue;
    //        changed = true;
    //    }
    //}

    /// <summary>
    /// Project version(s) for which the issue is (or was) manifesting.
    /// </summary>
    //[JqlFieldAttribute("affectedVersion", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable | JqlFieldCompare.Include)]
    //public List<IssueVersion>? AffectedVersions
    //{
    //    get => this.affectedversions;
    //    set => SetValue(ref this.affectedversions, value, ref this.affectedversionsChanged);
    //}

    //private bool affectedversionsChanged;
    //private List<IssueVersion> affectedversionsOrginal;
    //private List<IssueVersion> affectedversions;

    /// <summary>
    /// The person to whom the issue is currently assigned.
    /// </summary>
   // [JqlFieldAttribute("assignee", JqlFieldCompare.Comparable | JqlFieldCompare.Include | JqlFieldCompare.Was | JqlFieldCompare.WasInclude | JqlFieldCompare.Changed)]
    public User? Assignee { get; }
    //{
    //    get => this.assignee;
    //    set => SetValue(ref this.assignee, value, ref this.assigneeChanged);
    //}
    //private bool assigneeChanged;
    //private User? assignee;

    /// <summary>
    /// Attachments of the JIRA issue.
    /// </summary>
    //public List<Attachment>? Attachments { get;  }
        

    /// <summary>
    /// Comments of the JIRA issue.
    /// </summary>
    //public List<Comment>? Comments { get;  set; }

    /// <summary>
    /// Project component(s) to which this issue relates.
    /// </summary>
    public List<Component>? Components { get; }
    //{;
    //    get
    //    {
    //        return this.components;
    //    }
    //    set
    //    {
    //        if (this.components != value)
    //        {
    //            this.components = value;
    //            this.componentsChanged = true;
    //        }
    //    }
    //}
    //private bool componentsChanged;
    //private List<Component>? componentsOrginal;
    //private List<Component>? components;

    /// <summary>
    /// The time and date on which this issue was entered into JIRA.
    /// </summary>
    public DateTime? CreatedDate { get; }

    /// <summary>
    /// A detailed description of the issue.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// The date by which this issue is scheduled to be completed.
    /// </summary>
    public DateTime? DueDate { get; }
   
    /// <summary>
    /// The hardware or software environment to which the issue relates.
    /// </summary>
    public string? Environment { get; }
    
    /// <summary>
    /// Search for issues that belong to a particular epic in GreenHopper. 
    /// </summary>
    /// <remarks>For LINQ use only.</remarks>
    /// <remarks>Only available with GreenHopper 6.1.2 or later.</remarks>
    //[JqlFieldAttribute("\"epic link\"", JqlFieldCompare.Comparable | JqlFieldCompare.Include)]
    //public string EpicLink { get { throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly); } }

    /// <summary>
    /// Filter to use by LINQ.
    /// </summary>
    /// <remarks>For LINQ use only.</remarks>
    //[JqlFieldAttribute("filter", JqlFieldCompare.Comparable | JqlFieldCompare.Include)]
    //public Filter Filter
    //{
    //    get { throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly); }
    //}

    /// <summary>
    /// Project version(s) in which the issue was (or will be) fixed.
    /// </summary>
    public List<IssueVersion>? FixVersions { get; }
    
    /// <summary>
    /// JIRA can be used to track many different types of issues. 
    /// </summary>
    public IssueType? IssueType { get; }
    
    /// <summary>
    /// Labels to which this issue relates.
    /// </summary>
    public List<string>? Labels { get; }
    
    /// <summary>
    /// Date at which the issue was last viewed.
    /// </summary>
    public DateTime? LastViewed { get; set; }

    /// <summary>
    /// Issue level for LINQ filtering.
    /// </summary>
    /// <remarks>For LINQ use only.</remarks>
    //[JqlFieldAttribute("level", JqlFieldCompare.Comparable | JqlFieldCompare.Include)]
    //public string Level
    //{
    //    get { throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly); }
    //}

    /// <summary>
    ///  A list of links to related issues.
    /// </summary>
    //public IEnumerable<IssueLink>? Links { get;  set; }

    /// <summary>
    /// The Original Estimate of the total amount of time required to resolve the issue, as estimated when the issue was created.
    /// </summary>
    //[JqlFieldAttribute("originalEstimate", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable | JqlFieldCompare.Include)]
    //public string? OriginalEstimate { get { throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly); } }

    /// <summary>
    /// Original estimate in seconds,
    /// </summary>
    public long? OriginalEstimateSeconds { get;  set; }

    /// <summary>
    /// Parent issue of a sub task issue.
    /// </summary>
    public Issue? Parent { get; }

    /// <summary>
    /// The importance of the issue in relation to other issues.
    /// </summary>
    public Priority? Priority { get; }
    
    /// <summary>
    /// Progress of the issue.
    /// </summary>
    //public Progress? Progress { get; }

    /// <summary>
    /// The 'parent' project to which the issue belongs.
    /// </summary>
    public Project? Project { get; }

    /// <summary>
    /// Remaining estimate as string ('4w', '2d', '5h').
    /// </summary>
    /// <remarks>For LINQ use only.</remarks>
    //[JqlFieldAttribute("remainingEstimate", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable | JqlFieldCompare.Include)]
    //public SortableString RemainingEstimate { get { throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly); } }

    /// <summary>
    /// Remaining estimate in seconds.
    /// </summary>
    public long? RemainingEstimateSeconds { get;  }

    /// <summary>
    /// Reporter of the JIRA issue.
    /// </summary>
    [JqlFieldAttribute("reporter", JqlFieldCompare.Comparable | JqlFieldCompare.Include | JqlFieldCompare.Was | JqlFieldCompare.WasInclude | JqlFieldCompare.Changed)]
    public User? Reporter { get; }
    
    /// <summary>
    /// A record of the issue's resolution, if the issue has been resolved or closed.
    /// </summary>
    //public Resolution? Resolution { get; }
    
    /// <summary>
    /// The time and date on which this issue was resolved.
    /// </summary>
    public DateTime? ResolutionDate { get; }

    
    /// <summary>
    /// The security level of the issue.
    /// </summary>
    /// <remarks>For LINQ use only.</remarks>
    //public string SecurityLevel { get { throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly); } }
    

    /// <summary>
    /// Sprint to wich the ticket belongs.
    /// </summary>
    /// <remarks>For LINQ use only and only with Greenhopper.</remarks>
    //[JqlFieldAttribute("sprint", JqlFieldCompare.Comparable | JqlFieldCompare.Include)]
    //public string Sprint
    //{
    //    get { throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly); }
    //}

    /// <summary>
    /// The stage the issue is currently at in its lifecycle ('workflow'). 
    /// </summary>
    public Status? Status { get; }
    
    /// <summary>
    /// Subtask issues of this issue.
    /// </summary>
    public IEnumerable<Issue>? Subtasks { get; set; }

    /// <summary>
    /// A brief one-line summary of the issue
    /// </summary>
    public string? Summary { get; }
    
    
    /// <summary>
    /// The sum of the Time Spent from each of the individual work logs for this issue in seconds.
    /// </summary>
    public long? TimeSpentSeconds { get; private set; }

    /// <summary>
    /// Time tracking of the issue.
    /// </summary>
    public TimeTracking? TimeTracking { get; }

    /// <summary>
    /// The time and date on which this issue was last edited.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Custom fields of the issue.
    /// </summary>
    /// <param name="name">Name of the custom field.</param>
    /// <returns>Value of the custom field.</returns>
    //public CustomFieldValue? this[string name]
    //{
    //    get
    //    {
    //        return this.customFields?.Where(c => c.Name!.Trim() == name.Trim()).Select(c => c.Value).FirstOrDefault();
    //    }
    //    set
    //    {
    //        CustomField? customField = this.customFields?.Where(c => c.Name!.Trim() == name.Trim()).FirstOrDefault();
    //        if (customField == null)
    //        {
    //            this.customFields?.Add(new CustomField() { Name = name, Value = value, Changed = true });
    //        }
    //        else
    //        {
    //            customField.Value = value;
    //            customField.Changed = true;
    //        }
    //    }
    //}
    //private List<CustomField>? customFields;

    #endregion

    #region Methods

    public async Task<Issue?> CreateSubIssueAsync(IssueType issueType, string reporter, string summary, string description, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        CreateIssueModel model = new() { Fields = [] };
        model.Fields.Add("parent", new IssueModel() { Key = this.Key });
        model.Fields.Add("project", new ProjectModel() { Id = this.Project!.Id });
        model.Fields.Add("issuetype", new IssueTypeModel() { Id = issueType.Id });
        model.Fields.Add("reporter", new UserModel() { Name = reporter });
        model.Fields.Add("summary", summary);
        model.Fields.Add("description", description);

        var res = await service.CreateIssueAsync(model, cancellationToken);
        return res.CastModel<Issue>(service);
    }

    #endregion
}



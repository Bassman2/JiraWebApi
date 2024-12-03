namespace JiraWebApi;

/// <summary>
/// Representation of an JIRA issue.
/// </summary>
/// <remarks>
/// In the Issue class some properties are for LINQ use only and are not read or writeable. 
/// See the documentation of the property to get detailed information.
/// </remarks>
[DebuggerDisplay("{Id}, {Name}")]
public sealed class Issue 
{

    [DebuggerDisplay("{Id}, {Name}")]
    private class CustomField
    {
        public CustomField()
        { }

        public CustomField(Field field, CustomFieldValue value)
        {
            this.Id = field.Id;
            this.Name = field.Name;
            this.Value = value;
        }

        public string? Name { get; set; }
        public string? Id { get; set; }
        //public string Type { get; set; }
        public CustomFieldValue? Value { get; set; }
        public bool Changed { get; set; }

        ///// <summary>
        ///// Returns a string that represents the current object.
        ///// </summary>
        ///// <returns>A string that represents the current object.</returns>
        //public override string ToString()
        //{
        //    return string.Format("{0}, {1}", this.Id, this.Name);
        //}
    }

    /// <summary>
    /// Initializes a new instance of the Issue class.
    /// </summary>
    public Issue()
    {
        //this.SerializeMode = SerializeMode.Link;
        this.customFields = new List<CustomField>();
    }

    /// <summary>
    /// Initializes a new instance of the Issue class to create an issue link.
    /// </summary>
    /// <param name="issueKey">Key of the issue to link to.</param>
    public Issue(string issueKey)
    {
        //this.SerializeMode = SerializeMode.Link;
        this.Key = issueKey;
        this.customFields = new List<CustomField>();
    }

   

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

    #region fields

    /// <summary>
    /// Name of the classes which should be expanded.
    /// </summary>
    /// <remarks>Not useable by LINQ.</remarks>
    public string? Expand { get; private set; }

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
    public Progress? AggregateProgress { get; private set; }

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

    private void SetValue<T>(ref T value, T newValue, ref bool changed)
    {
        if (value!.Equals(newValue))
        {
            value = newValue;
            changed = true;
        }
    }

    /// <summary>
    /// Project version(s) for which the issue is (or was) manifesting.
    /// </summary>
    [JqlFieldAttribute("affectedVersion", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable | JqlFieldCompare.Include)]
    public ComparableList<IssueVersion> AffectedVersions
    {
        get => this.affectedversions;
        set => SetValue(ref this.affectedversions, value, ref this.affectedversionsChanged);
    }

    private bool affectedversionsChanged;
    private ComparableList<IssueVersion> affectedversionsOrginal;
    private ComparableList<IssueVersion> affectedversions;

    /// <summary>
    /// The person to whom the issue is currently assigned.
    /// </summary>
    [JqlFieldAttribute("assignee", JqlFieldCompare.Comparable | JqlFieldCompare.Include | JqlFieldCompare.Was | JqlFieldCompare.WasInclude | JqlFieldCompare.Changed)]
    public User? Assignee
    {
        get => this.assignee;
        set => SetValue(ref this.assignee, value, ref this.assigneeChanged);
    }
    private bool assigneeChanged;
    private User? assignee;

    /// <summary>
    /// Attachments of the JIRA issue.
    /// </summary>
    public IEnumerable<Attachment> Attachments { get; private set; }

    /// <summary>
    /// Category of the JIRA issue.
    /// </summary>
    /// <remarks>For LINQ use only.</remarks>
    [JqlFieldAttribute("category", JqlFieldCompare.Comparable | JqlFieldCompare.Include)]
    public string Category
    {
        get { throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly); }
    }

    /// <summary>
    /// Comments of the JIRA issue.
    /// </summary>
    [JqlFieldAttribute("comment", JqlFieldCompare.Contains)]
    public ComparableList<Comment> Comments { get; private set; }

    /// <summary>
    /// Project component(s) to which this issue relates.
    /// </summary>
    [JqlFieldAttribute("component", JqlFieldCompare.Comparable | JqlFieldCompare.Include)]
    public ComparableList<Component> Components
    {
        get
        {
            return this.components;
        }
        set
        {
            if (this.components != value)
            {
                this.components = value;
                this.componentsChanged = true;
            }
        }
    }
    private bool componentsChanged;
    private ComparableList<Component> componentsOrginal;
    private ComparableList<Component> components;

    /// <summary>
    /// The time and date on which this issue was entered into JIRA.
    /// </summary>
    [JqlFieldAttribute("createdDate", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable | JqlFieldCompare.Include)]
    public DateTime? CreatedDate { get; private set; }

    /// <summary>
    /// A detailed description of the issue.
    /// </summary>
    [JqlFieldAttribute("description", JqlFieldCompare.Contains)]
    public string? Description
    {
        get
        {
            return this.description;
        }
        set
        {
            if (this.description != value)
            {
                this.description = value;
                this.descriptionChanged = true;
            }
        }
    }
    private bool descriptionChanged = false;
    private string? description;

    /// <summary>
    /// The date by which this issue is scheduled to be completed.
    /// </summary>
    [JqlFieldAttribute("dueDate", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable | JqlFieldCompare.Include)]
    public DateTime? DueDate
    {
        get
        {
            return this.dueDate;
        }
        set
        {
            if (this.dueDate != value)
            {
                this.dueDate = value;
                this.dueDateChanged = true;
            }
        }
    }
    private bool dueDateChanged = false;
    private DateTime? dueDate;

    /// <summary>
    /// The hardware or software environment to which the issue relates.
    /// </summary>
    [JqlFieldAttribute("environment", JqlFieldCompare.Contains)]
    public string Environment
    {
        get
        {
            return this.environment;
        }
        set
        {
            if (this.environment != value)
            {
                this.environment = value;
                this.environmentChanged = true;
            }
        }
    }
    private bool environmentChanged = false;
    private string environment;

    /// <summary>
    /// Search for issues that belong to a particular epic in GreenHopper. 
    /// </summary>
    /// <remarks>For LINQ use only.</remarks>
    /// <remarks>Only available with GreenHopper 6.1.2 or later.</remarks>
    [JqlFieldAttribute("\"epic link\"", JqlFieldCompare.Comparable | JqlFieldCompare.Include)]
    public string EpicLink { get { throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly); } }

    /// <summary>
    /// Filter to use by LINQ.
    /// </summary>
    /// <remarks>For LINQ use only.</remarks>
    [JqlFieldAttribute("filter", JqlFieldCompare.Comparable | JqlFieldCompare.Include)]
    public Filter Filter
    {
        get { throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly); }
    }

    /// <summary>
    /// Project version(s) in which the issue was (or will be) fixed.
    /// </summary>
    [JqlFieldAttribute("fixVersion", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable | JqlFieldCompare.Include)]
    public ComparableList<IssueVersion> FixVersions
    {
        get
        {
            return this.fixVersions;
        }
        set
        {
            if (this.fixVersions != value)
            {
                this.fixVersions = value;
                this.fixVersionsChanged = true;
            }
        }
    }
    private bool fixVersionsChanged;
    private ComparableList<IssueVersion> fixVersionsOrginal;
    private ComparableList<IssueVersion> fixVersions;

    /// <summary>
    /// JIRA can be used to track many different types of issues. 
    /// </summary>
    [JqlFieldAttribute("issuetype", JqlFieldCompare.Comparable | JqlFieldCompare.Include)]
    public IssueType IssueType
    {
        get
        {
            return this.issueType;
        }
        set
        {
            if (this.issueType != value)
            {
                this.issueType = value;
                this.issueTypeChanged = true;
            }
        }
    }
    private bool issueTypeChanged;
    private IssueType issueType;

    /// <summary>
    /// Labels to which this issue relates.
    /// </summary>
    public IEnumerable<string> Labels
    {
        get
        {
            return this.labels;
        }
        set
        {
            if (this.labels != value)
            {
                this.labels = value;
                this.labelsChanged = true;
            }
        }
    }
    private bool labelsChanged;
    private IEnumerable<string> labelsOrginal;
    private IEnumerable<string> labels;

    /// <summary>
    /// Date at which the issue was last viewed.
    /// </summary>
    [JqlFieldAttribute("lastViewed", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable | JqlFieldCompare.Include)]
    public DateTime? LastViewed { get; private set; }

    /// <summary>
    /// Issue level for LINQ filtering.
    /// </summary>
    /// <remarks>For LINQ use only.</remarks>
    [JqlFieldAttribute("level", JqlFieldCompare.Comparable | JqlFieldCompare.Include)]
    public string Level
    {
        get { throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly); }
    }

    /// <summary>
    ///  A list of links to related issues.
    /// </summary>
    public IEnumerable<IssueLink> Links { get; private set; }

    /// <summary>
    /// The Original Estimate of the total amount of time required to resolve the issue, as estimated when the issue was created.
    /// </summary>
    [JqlFieldAttribute("originalEstimate", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable | JqlFieldCompare.Include)]
    public SortableString OriginalEstimate { get { throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly); } }

    /// <summary>
    /// Original estimate in seconds,
    /// </summary>
    public long? OriginalEstimateSeconds { get; private set; }

    /// <summary>
    /// Parent issue of a sub task issue.
    /// </summary>
    [JqlFieldAttribute("parent", JqlFieldCompare.Comparable | JqlFieldCompare.Include)]
    public Issue Parent { get; set; }

    /// <summary>
    /// The importance of the issue in relation to other issues.
    /// </summary>
    [JqlFieldAttribute("priority", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable | JqlFieldCompare.Include | JqlFieldCompare.Was | JqlFieldCompare.WasInclude | JqlFieldCompare.Changed)]
    public Priority Priority
    {
        get
        {
            return this.priority;
        }
        set
        {
            if (this.priority != value)
            {
                this.priority = value;
                this.priorityChanged = true;
            }
        }
    }
    private bool priorityChanged;
    private Priority priority;

    /// <summary>
    /// Progress of the issue.
    /// </summary>
    public Progress Progress { get; private set; }

    /// <summary>
    /// The 'parent' project to which the issue belongs.
    /// </summary>
    [JqlFieldAttribute("project", JqlFieldCompare.Comparable | JqlFieldCompare.Include)]
    public Project Project
    {
        get
        {
            return this.project;
        }
        set
        {
            if (this.project != value)
            {
                this.project = value;
                this.projectChanged = true;
            }
        }
    }
    private bool projectChanged = false;
    private Project project;

    /// <summary>
    /// Remaining estimate as string ('4w', '2d', '5h').
    /// </summary>
    /// <remarks>For LINQ use only.</remarks>
    [JqlFieldAttribute("remainingEstimate", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable | JqlFieldCompare.Include)]
    public SortableString RemainingEstimate { get { throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly); } }

    /// <summary>
    /// Remaining estimate in seconds.
    /// </summary>
    public long? RemainingEstimateSeconds { get; private set; }

    /// <summary>
    /// Reporter of the JIRA issue.
    /// </summary>
    [JqlFieldAttribute("reporter", JqlFieldCompare.Comparable | JqlFieldCompare.Include | JqlFieldCompare.Was | JqlFieldCompare.WasInclude | JqlFieldCompare.Changed)]
    public User Reporter
    {
        get
        {
            return this.reporter;
        }
        set
        {
            if (this.reporter != value)
            {
                this.reporter = value;
                this.reporterChanged = true;
            }
        }
    }
    private bool reporterChanged;
    private User reporter;

    /// <summary>
    /// A record of the issue's resolution, if the issue has been resolved or closed.
    /// </summary>
    [JqlFieldAttribute("resolution", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable | JqlFieldCompare.Include | JqlFieldCompare.Was | JqlFieldCompare.WasInclude | JqlFieldCompare.Changed)]
    public Resolution Resolution
    {
        get
        {
            return this.resolution;
        }
        set
        {
            if (this.resolution != value)
            {
                this.resolution = value;
                this.resolutionChanged = true;
            }
        }
    }
    private bool resolutionChanged;
    private Resolution resolution;

    /// <summary>
    /// The time and date on which this issue was resolved.
    /// </summary>
    [JqlFieldAttribute("resolved", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable | JqlFieldCompare.Include)]
    public DateTime? ResolutionDate { get; private set; }

    
    /// <summary>
    /// The security level of the issue.
    /// </summary>
    /// <remarks>For LINQ use only.</remarks>
    public string SecurityLevel { get { throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly); } }
    

    /// <summary>
    /// Sprint to wich the ticket belongs.
    /// </summary>
    /// <remarks>For LINQ use only and only with Greenhopper.</remarks>
    [JqlFieldAttribute("sprint", JqlFieldCompare.Comparable | JqlFieldCompare.Include)]
    public string Sprint
    {
        get { throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly); }
    }

    /// <summary>
    /// The stage the issue is currently at in its lifecycle ('workflow'). 
    /// </summary>
    [JqlFieldAttribute("status", JqlFieldCompare.Comparable | JqlFieldCompare.Include | JqlFieldCompare.Was | JqlFieldCompare.WasInclude | JqlFieldCompare.Changed)]
    public Status Status
    {
        get
        {
            return this.status;
        }
        set
        {
            if (this.status != value)
            {
                this.status = value;
                this.statusChanged = true;
            }
        }
    }
    private bool statusChanged;
    private Status status;

    /// <summary>
    /// Subtask issues of this issue.
    /// </summary>
    public IEnumerable<Issue> Subtasks { get; private set; }

    /// <summary>
    /// A brief one-line summary of the issue
    /// </summary>
    [JqlFieldAttribute("summary", JqlFieldCompare.Contains)]
    public string Summary
    {
        get
        {
            return this.summary;
        }
        set
        {
            if (this.summary != value)
            {
                this.summary = value;
                this.summaryChanged = true;
            }
        }
    }
    private bool summaryChanged = false;
    private string summary;

    /// <summary>
    /// Text field for LINQ text search in all text fields.
    /// </summary>
    /// <remarks>For LINQ use only.</remarks>
    [JqlFieldAttribute("text", JqlFieldCompare.Contains)]
    public string Text
    {
        get { throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly); }
    }

    /// <summary>
    /// Thumbnails of the issue.
    /// </summary>
    /// <remarks>For LINQ use only.</remarks>
    public IEnumerable<string> Thumbnails { get { throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly); } }
    
    /// <summary>
    /// The sum of the Time Spent from each of the individual work logs for this issue.
    /// </summary>
    [JqlFieldAttribute("timeSpent", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable | JqlFieldCompare.Include)]
    public SortableString TimeSpent { get { throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly); } }

    /// <summary>
    /// The sum of the Time Spent from each of the individual work logs for this issue in seconds.
    /// </summary>
    public long? TimeSpentSeconds { get; private set; }

    /// <summary>
    /// Time tracking of the issue.
    /// </summary>
    public TimeTracking TimeTracking
    {
        get
        {
            return this.timeTracking;
        }
        set
        {
            this.timeTracking = value;
            this.timeTrackingChanged = true;
        }
    }
    private bool timeTrackingChanged = false;
    private TimeTracking timeTracking;

    /// <summary>
    /// The time and date on which this issue was last edited.
    /// </summary>
    [JqlFieldAttribute("updated", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable | JqlFieldCompare.Include)]
    public DateTime? UpdatedDate { get; private set; }

    private Votes votes;
    /// <summary>
    /// Voters of the issue.
    /// </summary>
    [JqlFieldAttribute("votes", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable | JqlFieldCompare.Include)]
    public int Votes { get { return this.votes != null ? this.votes.VoteNum : 0; } }

    /// <summary>
    /// The number indicates how many votes this issue has.
    /// </summary>
    public IEnumerable<User> Voters { get { return this.votes != null ? this.votes.Voters : null; } }

    /// <summary>
    /// Voter of the issue.
    /// </summary>
    [JqlFieldAttribute("voter", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable | JqlFieldCompare.Include)]
    public User Voter { get { throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly); } }

    private Watchers watchers;

    /// <summary>
    /// The number indicates how many people who are watching this issue.
    /// </summary>
    [JqlFieldAttribute("watchers", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable | JqlFieldCompare.Include)]
    public int WatchCount
    {
        get { return this.watchers != null ? this.watchers.WatchCount : 0; }
    }

    /// <summary>
    /// The people who are watching this issue.
    /// </summary>
    public IEnumerable<User> Watchers
    {
        get { return this.watchers != null ? this.watchers.Users : null; }
    }

    /// <summary>
    /// The people who are watching this issue.
    /// </summary>
    [JqlFieldAttribute("watcher", JqlFieldCompare.Comparable | JqlFieldCompare.Include)]
    public User Watcher { get { throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly); } }

    private WorklogGetResult worklogs;
    /// <summary>
    /// Worklogs of the issue.
    /// </summary>
    public IEnumerable<Worklog>? Worklogs { get { return this.worklogs != null ? this.worklogs.Worklogs: null; } }

    /// <summary>
    /// Workratio of the issue.
    /// </summary>
    [JqlFieldAttribute("workRatio", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable | JqlFieldCompare.Include)]
    public int Workratio { get; private set; }

    /// <summary>
    /// Custom fields of the issue.
    /// </summary>
    /// <param name="name">Name of the custom field.</param>
    /// <returns>Value of the custom field.</returns>
    public CustomFieldValue? this[string name]
    {
        get
        {
            return this.customFields.Where(c => c.Name.Trim() == name.Trim()).Select(c => c.Value).FirstOrDefault();
        }
        set
        {
            CustomField? customField = this.customFields.Where(c => c.Name.Trim() == name.Trim()).FirstOrDefault();
            if (customField == null)
            {
                this.customFields.Add(new CustomField() { Name = name, Value = value, Changed = true });
            }
            else
            {
                customField.Value = value;
                customField.Changed = true;
            }
        }
    }
    private List<CustomField> customFields;

    #endregion

    /// <summary>
    /// Returns the issue key as represent the issue as string.
    /// </summary>
    /// <returns>A string element.</returns>
    public override string ToString()
    {
        return this.Key;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not Issue issue)
        {
            return false;
        }
        return this.Id == issue.Id && this.Key == issue.Key;
    }

    /// <summary>
    /// Serves as a hash function for a particular type. 
    /// </summary>
    /// <returns>A hash code for the current Object.</returns>
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    /// <summary>
    /// Compare equal operator.
    /// </summary>
    /// <param name="issue1">The first issue to compare, or null.</param>
    /// <param name="issue2">The second issue to compare, or null.</param>
    /// <returns>true if the id and key of the first issue is equal to the id and key of the second issue; otherwise, false.</returns>
    public static bool operator ==(Issue issue1, Issue issue2)
    {
        if (ReferenceEquals(issue1, issue2))
        {
            return true;
        }
        if (((object)issue1 == null) || ((object)issue2 == null))
        {
            return false;
        }
        return issue1.Equals(issue2);
    }

    /// <summary>
    /// Compare not equal operator.
    /// </summary>
    /// <param name="issue1">The first issue to compare, or null.</param>
    /// <param name="issue2">The second issue to compare, or null.</param>
    /// <returns>true if the id and key of the first issue is different from the id and key of the second issue; otherwise, false.</returns>
    public static bool operator !=(Issue issue1, Issue issue2)
    {
        return !(issue1 == issue2);
    }

    /// <summary>
    /// Compare equal operator.
    /// </summary>
    /// <param name="key">Key key of the first issue to compare, or null.</param>
    /// <param name="issue">The second issue to compare, or null.</param>
    /// <returns>true if the id of the first element is equal to the id of the second element; otherwise, false.</returns>
    public static bool operator ==(string key, Issue issue)
    {
        return /*issue != null &&*/ key == issue.Key;
    }

    /// <summary>
    ///  Compare not equal operator.
    /// </summary>
    /// <param name="key">Key of the first issue to compare.</param>
    /// <param name="issue">The second issue to compare.</param>
    /// <returns>true if the id of the first element is different from the id of the second element; otherwise, false.</returns>
    public static bool operator !=(string key, Issue issue)
    {
        return /*issue == null ||*/ key != issue.Key;
    }

    /// <summary>
    /// Compare greater than operator to allow LINQ compare.
    /// </summary>
    /// <param name="key">Key of the first issue to compare.</param>
    /// <param name="issue">The second issue to compare.</param>
    /// <returns>Not used.</returns>
    public static bool operator >(string key, Issue issue)
    {
        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
    }

    /// <summary>
    /// Compare greater or equal operator to allow LINQ compare.
    /// </summary>
    /// <param name="key">Key of the first issue to compare.</param>
    /// <param name="issue">The second issue to compare.</param>
    /// <returns>Not used.</returns>
    public static bool operator >=(string key, Issue issue)
    {
        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
    }

    /// <summary>
    /// Compare less than operator to allow LINQ compare.
    /// </summary>
    /// <param name="key">Key of the first issue to compare.</param>
    /// <param name="issue">The second issue to compare.</param>
    /// <returns>Not used.</returns>
    public static bool operator <(string key, Issue issue)
    {
        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
    }

    /// <summary>
    /// Compare less than or equal operator to allow LINQ compare.
    /// </summary>
    /// <param name="key">Key of the first issue to compare.</param>
    /// <param name="issue">The second issue to compare.</param>
    /// <returns>Not used.</returns>
    public static bool operator <=(string key, Issue issue)
    {
        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
    }

    /// <summary>
    /// Implicite cast operator to cast from issue to key string.
    /// </summary>
    /// <param name="issue">Issue to cast.</param>
    /// <returns>String with the key of the issue.</returns>
    public static implicit operator string?(Issue? issue)
    {
        return issue?.Key;
    }

    /// <summary>
    /// Support of the JQL In operator in LINQ.
    /// </summary>
    /// <param name="values">List to compare with.</param>
    /// <returns>true if the source element is in the values list; otherwise, false.</returns>
    public bool In(params string[] values)
    {
        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
    }

    /// <summary>
    /// Support of the JQL 'in' operator in LINQ.
    /// </summary>
    /// <param name="values">List to compare with.</param>
    /// <returns>true if the source element is in the values list; otherwise, false.</returns>
    public bool In(params Issue[] values)
    {
        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
    }

    /// <summary>
    /// Support of the JQL Not In operator in LINQ.
    /// </summary>
    /// <param name="values">List to compare with.</param>
    /// <returns>true if the source element is not in the values list; otherwise, false.</returns>
    public bool NotIn(params string[] values)
    {
        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
    }

    /// <summary>
    /// Support of the JQL Not In operator in LINQ.
    /// </summary>
    /// <param name="values">List to compare with.</param>
    /// <returns>true if the source element is not in the values list; otherwise, false.</returns>
    public bool NotIn(params Issue[] values)
    {
        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
    }
}



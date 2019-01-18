using JiraWebApi.Internal;
using JiraWebApi.Linq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of an JIRA issue.
    /// </summary>
    /// <remarks>
    /// In the Issue class some properties are for LINQ use only and are not read or writeable. 
    /// See the documentation of the property to get detailed information.
    /// </remarks>
    [Serializable]
    public sealed class Issue : ISerializable
    {
        internal SerializeMode SerializeMode { get; set; }
        
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

            public string Name { get; set; }
            public string Id { get; set; }
            //public string Type { get; set; }
            public CustomFieldValue Value { get; set; }
            public bool Changed { get; set; }

            /// <summary>
            /// Returns a string that represents the current object.
            /// </summary>
            /// <returns>A string that represents the current object.</returns>
            public override string ToString()
            {
                return string.Format("{0}, {1}", this.Id, this.Name);
            }
        }

        /// <summary>
        /// Initializes a new instance of the Issue class.
        /// </summary>
        public Issue()
        {
            this.SerializeMode = SerializeMode.Link;
            this.customFields = new List<CustomField>();
        }

        /// <summary>
        /// Initializes a new instance of the Issue class to create an issue link.
        /// </summary>
        /// <param name="issueKey">Key of the issue to link to.</param>
        public Issue(string issueKey)
        {
            this.SerializeMode = SerializeMode.Link;
            this.Key = issueKey;
            this.customFields = new List<CustomField>();
        }

        #region Serialization

        /// <summary>
        /// Initializes a new instance of the Issue class by serialization.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo to populate with data.</param>
        /// <param name="context">The destination <see cref="System.Runtime.Serialization.StreamingContext">System.Runtime.Serialization.StreamingContext</see> for this serialization.</param>
        public Issue(SerializationInfo info, StreamingContext context)
        {
            Jira jira = context.Context as Jira;

            this.customFields = new List<CustomField>();

            foreach (SerializationEntry entry in info)
            {
                switch (entry.Name)
                {
                case "errorMessages":
                    throw new JiraException(((JArray)entry.Value).Select(t => (string)t).ToList());
                case "errors":
                    throw new JiraException(((IEnumerable<KeyValuePair<string, JToken>>)((JObject)entry.Value)).ToDictionary(e => e.Key, e => (string)e.Value));
                case "expand":
                    this.Expand = info.GetString("expand");
                    break;
                case "id":
                    this.Id = info.GetString("id");
                    break;
                case "self":
                    this.Self = new Uri(info.GetString("self"));
                    break;
                case "key":
                    this.Key = info.GetString("key");
                    break;
                case "fields":
#region Fields
                    int i = 1;
                    foreach (KeyValuePair<string, JToken> field in (JObject)entry.Value)
                    {
                        JObject value = field.Value as JObject;

                        JsonTrace.WriteLine(string.Format("{0} : {1} : {2}", i++, field.Key, value));

                        switch (field.Key)
                        {
                        case "aggregateprogress":
                            this.AggregateProgress = field.Value.ToObject<Progress>();
                            break;
                        case "aggregatetimeestimate":
                            this.AggregateTimeEstimate = (long?)field.Value;
                            break;
                        case "aggregatetimeoriginalestimate":
                            this.AggregateTimeOriginalEstimate = (long?)field.Value;
                            break;
                        case "aggregatetimespent":
                            this.AggregateTimeSpent = (long?)field.Value;
                            break;
                        case "assignee":
                            this.assignee = field.Value.ToObject<User>();
                            break;
                        case "attachment":
                            this.Attachments = field.Value.Select(c => c.ToObject<Attachment>());
                            break;
                        case "comment":
                            {
                                CommentGetResult result = field.Value.ToObject<CommentGetResult>();
                                this.Comments = result.Comments.ToComparableList();
                            }
                            break;
                        case "components":
                            this.components = this.componentsOrginal = field.Value.Select(c => c.ToObject<Component>()).ToComparableList();
                            break;
                        case "created":
                            this.CreatedDate = (DateTime?)field.Value;
                            break;
                        case "description":
                            this.description = (string)field.Value;
                            break;
                        case "duedate":
                            this.dueDate = (DateTime?)field.Value;
                            break;
                        case "environment":
                            this.environment = (string)field.Value;
                            break;
                        case "fixVersions":
                            this.fixVersions = this.fixVersionsOrginal = field.Value.Select(c => c.ToObject<IssueVersion>()).ToComparableList();
                            break;
                        case "issuelinks":
                            this.Links = field.Value.Select(c => c.ToObject<IssueLink>());
                            break;
                        case "issuetype":
                            this.issueType = field.Value.ToObject<IssueType>();
                            break;
                        case "labels":
                            this.labels = field.Value.Select(c => c.ToObject<string>());
                            break;
                        case "lastViewed":
                            this.LastViewed = (DateTime?)field.Value;
                            break;
                        case "parent":
                            this.Parent = field.Value.ToObject<Issue>();
                            break;
                        case "priority":
                            this.priority = field.Value.ToObject<Priority>();
                            break;
                        case "progress":
                            this.Progress = field.Value.ToObject<Progress>();
                            break;
                        case "project":
                            this.project = field.Value.ToObject<Project>();
                            break;
                        case "reporter":
                            this.reporter = field.Value.ToObject<User>();
                            break;
                        case "resolution":
                            this.resolution = field.Value.ToObject<Resolution>();
                            break;
                        case "resolutiondate":
                            this.ResolutionDate = (DateTime?)field.Value;
                            break;
                        case "status":
                            this.status = field.Value.ToObject<Status>();
                            break;
                        case "subtasks":
                            this.Subtasks = field.Value.Select(c => c.ToObject<Issue>()).ToArray();
                            break;
                        case "summary":
                            this.summary = (string)field.Value;
                            break;
                        case "timeestimate":
                            this.RemainingEstimateSeconds = (long?)field.Value;
                            break;
                        case "timeoriginalestimate":
                            this.OriginalEstimateSeconds = (long?)field.Value;
                            break;
                        case "timespent":
                            this.TimeSpentSeconds = (long?)field.Value;
                            break;
                        case "timetracking":
                            this.TimeTracking = field.Value.ToObject<TimeTracking>();
                            break;
                        case "updated":
                            this.UpdatedDate = (DateTime?)field.Value;
                            break;
                        case "versions":
                            this.affectedversions = this.affectedversionsOrginal = field.Value.Select(c => c.ToObject<IssueVersion>()).ToComparableList();
                            break;
                        case "votes":
                            this.votes = field.Value.ToObject<Votes>();
                            break;
                        case "watches":
                            this.watchers = field.Value.ToObject<Watchers>(); 
                            break;
                        case "worklog":
                            this.worklogs = field.Value.ToObject<WorklogGetResult>(); 
                            break;
                        case "workratio":
                            this.Workratio = (int)(long)field.Value;    // get as long and convert afterwards to int
                            break;
                        default:
                            // custom fields
                            if (field.Key.StartsWith("customfield_"))
                            {
                                Field fieldInfo = jira.GetCachedFieldsAsync().Result.Where(f => f.IsCustom && f.Id.Trim() == field.Key.Trim()).FirstOrDefault();
                                if (fieldInfo != null)
                                {
                                    switch (fieldInfo.Schema.Custom)
                                    {
                                    case "com.atlassian.jira.plugins.jira-importers-plugin:bug-importid":
                                        this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue((double?)field.Value)));
                                        break;

                                    case "com.atlassian.jira.plugin.system.customfieldtypes:cascadingselect":
                                        {
                                            CustomFieldOption customFieldOption = field.Value.ToObject<CustomFieldOption>();
                                            this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue(customFieldOption != null ? customFieldOption.Value : null)));
                                        }
                                        break;
                                    case "com.atlassian.jira.plugin.system.customfieldtypes:datepicker":
                                        this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue((DateTime?)field.Value)));
                                        break;
                                    case "com.atlassian.jira.plugin.system.customfieldtypes:datetime":
                                        this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue((DateTime?)field.Value)));
                                        break;
                                    case "com.atlassian.jira.plugin.system.customfieldtypes:float":
                                        this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue((double?)field.Value)));
                                        break;
                                    case "com.atlassian.jira.plugin.system.customfieldtypes:grouppicker":
                                        AddCustomField<Group>(fieldInfo, field.Value, CustomFieldType.Group);
                                        break;
                                    case "com.atlassian.jira.plugin.system.customfieldtypes:importid":
                                        this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue((double?)field.Value)));
                                        break;
                                    case "com.atlassian.jira.plugin.system.customfieldtypes:labels":
                                        {
                                            JArray array = field.Value as JArray;
                                            if (array != null && array.Count > 0)
                                            {
                                                string[] values = ((JArray)field.Value).Select(v => (string)((JValue)v).Value).ToArray();
                                                this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue(values)));
                                            }
                                            else
                                            {
                                                this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue((string[])null)));
                                            }
                                        }
                                        break;
                                    case "com.atlassian.jira.plugin.system.customfieldtypes:multicheckboxes":
                                        {
                                            JArray array = field.Value as JArray;
                                            if (array != null && array.Count > 0)
                                            {
                                                CustomFieldOption[] values = ((JArray)field.Value).Select(v => ((JObject)v).ToObject<CustomFieldOption>()).ToArray();
                                                string[] names = values.Select(v => v.Value).ToArray();
                                                this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue(names)));
                                            }
                                            else
                                            {
                                                this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue((string[])null)));
                                            }
                                        }
                                        break;
                                    case "com.atlassian.jira.plugin.system.customfieldtypes:multigrouppicker":
                                        AddCustomFieldArray<Group>(fieldInfo, field.Value, CustomFieldType.GroupArray);
                                        break;
                                    case "com.atlassian.jira.plugin.system.customfieldtypes:multiselect":
                                        {
                                            JArray array = field.Value as JArray;
                                            if (array != null && array.Count > 0)
                                            {
                                                CustomFieldOption[] values = ((JArray)field.Value).Select(v => ((JObject)v).ToObject<CustomFieldOption>()).ToArray();
                                                string[] names = values.Select(v => v.Value).ToArray();
                                                this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue(names)));
                                            }
                                            else
                                            {
                                                this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue((string[])null)));
                                            }
                                        }
                                        break;
                                    case "com.atlassian.jira.plugin.system.customfieldtypes:multiuserpicker":
                                        AddCustomFieldArray<User>(fieldInfo, field.Value, CustomFieldType.UserArray);
                                        break;
                                    case "com.atlassian.jira.plugin.system.customfieldtypes:multiversion":
                                        AddCustomFieldArray<IssueVersion>(fieldInfo, field.Value, CustomFieldType.VersionArray);
                                        break;
                                    case "com.atlassian.jira.plugin.system.customfieldtypes:project":
                                        AddCustomField<Project>(fieldInfo, field.Value, CustomFieldType.Project);
                                        break;
                                    case "com.atlassian.jira.plugin.system.customfieldtypes:radiobuttons":
                                        {
                                            CustomFieldOption customFieldOption = field.Value.ToObject<CustomFieldOption>();
                                            this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue(customFieldOption != null ? customFieldOption.Value : null)));
                                        }
                                        break;
                                    case "com.atlassian.jira.plugin.system.customfieldtypes:select":
                                        {
                                            CustomFieldOption customFieldOption = field.Value.ToObject<CustomFieldOption>();
                                            this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue(customFieldOption != null ? customFieldOption.Value : null)));
                                        }
                                        break;
                                    case "com.atlassian.jira.plugin.system.customfieldtypes:textarea":
                                    case "com.atlassian.jira.plugin.system.customfieldtypes:textfield":
                                        this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue((string)field.Value)));
                                        break;
                                    case "com.atlassian.jira.plugin.system.customfieldtypes:url":
                                        this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue((string)field.Value)));
                                        break;
                                    case "com.atlassian.jira.plugin.system.customfieldtypes:userpicker":
                                        AddCustomField<User>(fieldInfo, field.Value, CustomFieldType.User);
                                        break;
                                    case "com.atlassian.jira.plugin.system.customfieldtypes:version":
                                        AddCustomField<IssueVersion>(fieldInfo, field.Value, CustomFieldType.Version);
                                        break;
                                    default:
                                        Trace.TraceWarning("Unsupported custom field schema custom entry '{0}' for field {1}.", fieldInfo.Schema.Custom, fieldInfo.Name);
                                        break;
                                    }
                                }
                                else
                                {
                                    Trace.TraceWarning("Custom field {0} not found", field.Key);
                                }
                            }
                            break;
                        }

                    }
                    JsonTrace.WriteLine("---Fields ready---");
#endregion
                    break;
                case "renderedFields":
                    break;
                case "names":
                    //JObject names = ;
                    foreach (KeyValuePair<string, JToken> name in (JObject)entry.Value)
                    {
                        JObject value = name.Value as JObject;
                        //name.Key
                        //(string)value.Value;
                    }
                    //var x = names.Select(n => n.Value as JObject);
                    //var y = x.Where(o => o.Key);
                    break;
                case "schema":
                    break;
                case "transitions":
                    break;
                case "operations":
                    break;
                case "editmeta":
                    break;
                case "changelog":
                    break;
                default:
                    Trace.TraceWarning("Json element '{0}' unkown", entry.Name);
                    break;
                }
            }
        }

        /// <summary>
        /// Populates a System.Runtime.Serialization.SerializationInfo with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo to populate with data.</param>
        /// <param name="context">The destination <see cref="System.Runtime.Serialization.StreamingContext">System.Runtime.Serialization.StreamingContext</see> for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            switch (this.SerializeMode)
            {
            case SerializeMode.Link:
                GetObjectDataLink(info, context);
                break;
            case SerializeMode.Create:
                GetObjectDataCreate(info, context);
                break;
            case SerializeMode.Update:
                GetObjectDataSerialize(info, context);
                break;
            }
            // reset to link after serialization
            this.SerializeMode = SerializeMode.Link;
        }

        /// <summary>
        /// Link ticket serialization.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo to populate with data.</param>
        /// <param name="context">The destination <see cref="System.Runtime.Serialization.StreamingContext">System.Runtime.Serialization.StreamingContext</see> for this serialization.</param>
        private void GetObjectDataLink(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("key", (string)this.Key);
        }

        /// <summary>
        /// Create ticket serialization.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo to populate with data.</param>
        /// <param name="context">The destination <see cref="System.Runtime.Serialization.StreamingContext">System.Runtime.Serialization.StreamingContext</see> for this serialization.</param>
        private void GetObjectDataCreate(SerializationInfo info, StreamingContext context)
        {
            Jira jira = context.Context as Jira;
            JObject fields = new JObject();

            // nessesary fields
            fields.Add("summary", this.summary);
            fields.Add("project", new JObject(new JProperty("id", this.project.Id)));
            fields.Add("issuetype", new JObject(new JProperty("id", this.issueType.Id)));

            if (this.affectedversionsChanged)
            {
                fields.Add("versions", new JArray(this.affectedversions.Select(v => new JObject(new JProperty("id", v.Id)))));
            }

            if (this.descriptionChanged)
            {
                fields.Add("description", this.description);
            }
            if (this.environmentChanged)
            {
                fields.Add("environment", this.environment);
            }

            if (this.labelsChanged)
            {

                fields.Add("labels", new JArray(this.labels));
            }

            // class fields
            if (this.reporterChanged)
            {
                fields.Add("reporter", new JObject(new JProperty("name", this.reporter.Name)));
            }
            if (this.priorityChanged)
            {
                fields.Add("priority", new JObject(new JProperty("id", this.priority.Id)));
            }
            if (this.statusChanged)
            {
                fields.Add("status", new JObject(new JProperty("id", this.status.Id)));
            }
            if (this.resolutionChanged)
            {
                fields.Add("resolution", new JObject(new JProperty("id", this.resolution.Id)));
            }
            if (this.assigneeChanged)
            {
                fields.Add("assignee", new JObject(new JProperty("name", this.assignee.Name)));
            }

            // class array fields
            if (this.componentsChanged)
            {
                fields.Add("components", new JArray(this.components.Select(c => new JObject(new JProperty("id", c.Id)))));
            }
            if (this.fixVersionsChanged)
            {
                fields.Add("fixVersions", new JArray(this.fixVersions.Select(v => new JObject(new JProperty("id", v.Id)))));
            }


            // date fields
            if (this.dueDateChanged)
            {
                fields.Add("duedate", this.dueDate);
            }

            if (this.timeTrackingChanged)
            {
                fields.Add("timetracking", new JObject(
                    new JProperty("originalEstimate", this.timeTracking.OriginalEstimate),
                    new JProperty("remainingEstimate", this.timeTracking.RemainingEstimate)));
            }

            // custom fields
            foreach (CustomField customField in this.customFields)
            {
                Field fieldInfo = jira.GetCachedFieldsAsync().Result.Where(f => f.IsCustom && f.Name.Trim() == customField.Name.Trim()).FirstOrDefault();
                if (fieldInfo != null)
                {
                    if (customField.Changed)
                    {
                        switch (fieldInfo.Schema.Custom)
                        {
                        case "com.atlassian.jira.plugins.jira-importers-plugin:bug-importid":
                            CheckCustomFieldValue(customField, CustomFieldType.Double);
                            fields.Add(fieldInfo.Id, (string)customField.Value.Value);
                            break;

                        case "com.atlassian.jira.plugin.system.customfieldtypes:cascadingselect":
                            CheckCustomFieldValue(customField, CustomFieldType.String);
                            fields.Add(fieldInfo.Id, new JObject(new JProperty("value", (string)customField.Value.Value)));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:datepicker":
                            CheckCustomFieldValue(customField, CustomFieldType.DateTime);
                            fields.Add(fieldInfo.Id, (DateTime)customField.Value.Value);
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:datetime":
                            // this custom control accepts only a special date time format which is not ISO conform
                            CheckCustomFieldValue(customField, CustomFieldType.DateTime);
                            fields.Add(fieldInfo.Id, ((DateTime?)customField.Value.Value).ToJiraRestString());
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:float":
                            CheckCustomFieldValue(customField, CustomFieldType.Double);
                            fields.Add(fieldInfo.Id, (double?)customField.Value.Value);
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:grouppicker":
                            CheckCustomFieldValue(customField, CustomFieldType.Group);
                            fields.Add(fieldInfo.Id, new JObject(new JProperty("name", ((Group)customField.Value.Value).Name))); 
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:importid":
                            CheckCustomFieldValue(customField, CustomFieldType.Double);
                            fields.Add(fieldInfo.Id, (double?)customField.Value.Value);
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:labels":
                            CheckCustomFieldValue(customField, CustomFieldType.StringArray);
                            fields.Add(fieldInfo.Id, new JArray((string[])customField.Value.Value));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:multicheckboxes":
                            CheckCustomFieldValue(customField, CustomFieldType.StringArray);
                            fields.Add(fieldInfo.Id, new JArray(((string[])customField.Value.Value).Select(v => new JObject(new JProperty("value", v)))));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:multigrouppicker":
                            CheckCustomFieldValue(customField, CustomFieldType.GroupArray);
                            fields.Add(fieldInfo.Id, new JArray(((Group[])customField.Value.Value).Select(g => new JObject(new JProperty("name", g.Name)))));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:multiselect":
                            CheckCustomFieldValue(customField, CustomFieldType.StringArray);
                            fields.Add(fieldInfo.Id, new JArray(((string[])customField.Value.Value).Select(v => new JObject(new JProperty("value", v)))));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:multiuserpicker":
                            CheckCustomFieldValue(customField, CustomFieldType.UserArray);
                            fields.Add(fieldInfo.Id, new JArray(((User[])customField.Value.Value).Select(u => new JObject(new JProperty("name", u.Name)))));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:multiversion":
                            CheckCustomFieldValue(customField, CustomFieldType.VersionArray);
                            fields.Add(fieldInfo.Id, new JArray(((IssueVersion[])customField.Value.Value).Select(v => new JObject(new JProperty("id", v.Id)))));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:project":
                            CheckCustomFieldValue(customField, CustomFieldType.Project);
                            fields.Add(fieldInfo.Id, new JObject(new JProperty("id", ((Project)customField.Value.Value).Id)));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:radiobuttons":
                            CheckCustomFieldValue(customField, CustomFieldType.String);
                            fields.Add(fieldInfo.Id, new JObject(new JProperty("value", (string)customField.Value.Value)));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:select":
                            CheckCustomFieldValue(customField, CustomFieldType.String);
                            fields.Add(fieldInfo.Id, new JObject(new JProperty("value", (string)customField.Value.Value)));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:textarea":
                        case "com.atlassian.jira.plugin.system.customfieldtypes:textfield":
                            CheckCustomFieldValue(customField, CustomFieldType.String);
                            fields.Add(fieldInfo.Id, (string)customField.Value.Value);
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:url":
                            CheckCustomFieldValue(customField, CustomFieldType.String);
                            fields.Add(fieldInfo.Id, (string)customField.Value.Value);
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:userpicker":
                            CheckCustomFieldValue(customField, CustomFieldType.User);
                            fields.Add(fieldInfo.Id, new JObject(new JProperty("name", ((User)customField.Value.Value).Name)));                            
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:version":
                            CheckCustomFieldValue(customField, CustomFieldType.Version);
                            fields.Add(fieldInfo.Id, new JObject(new JProperty("id", ((IssueVersion)customField.Value.Value).Id)));                            
                            break;
                        default:
                            Trace.TraceWarning("Unsupported custom field schema custom entry '{0}' for field {1}.", fieldInfo.Schema.Custom, fieldInfo.Name);
                            break;
                        }
                        customField.Changed = false;
                    }
                }
                else
                {
                    Trace.TraceWarning("Custom field {0} not found", customField.Name);
                }
                string x = fields.ToString();
            }

                            
            // add fields
            info.AddValue("fields", fields);
        }

        private void CheckCustomFieldValue(CustomField customField, CustomFieldType customFieldType)
        {
            if (customField.Value.Type != customFieldType)
            {
                Trace.TraceError("Wrong type for custom field '{0}'. Type '{1}' needed.", customField.Name, customFieldType.ToString());
            }
        }

        
        /// <summary>
        /// Update ticket serialization.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo to populate with data.</param>
        /// <param name="context">The destination <see cref="System.Runtime.Serialization.StreamingContext">System.Runtime.Serialization.StreamingContext</see> for this serialization.</param>
        private void GetObjectDataSerialize(SerializationInfo info, StreamingContext context)
        {
            Jira jira = context.Context as Jira;
            

            info.AddValue("key", (string)this.Key);

            JObject update = new JObject();

            // text fields
            if (this.summaryChanged)
            {
                update.Add("summary", new JArray(new JObject(new JProperty("set", this.summary))));
            }
            if (this.descriptionChanged)
            {
                update.Add("description", new JArray(new JObject(new JProperty("set", this.description))));
            }
            if (this.environmentChanged)
            {
                update.Add("environment", new JArray(new JObject(new JProperty("set", this.environment))));
            }

            if (this.labelsChanged)
            {
                update.Add("labels", new JArray(
                    this.labels.Except(this.labelsOrginal).Select(l => new JObject(new JProperty("add", l))),
                    this.labelsOrginal.Except(this.labels).Select(l => new JObject(new JProperty("remove", l)))
                ));
            }


            // class fields
            if (this.projectChanged)
            {
                update.Add("project", new JArray(new JObject(new JProperty("set", this.project.Id))));
            }
            if (this.issueTypeChanged)
            {
                update.Add("issuetype", new JArray(new JObject(new JProperty("set", this.issueType.Id))));
            }
            if (this.reporterChanged)
            {
                update.Add("reporter", new JArray(new JObject(new JProperty("set", this.reporter.Name))));
            }
            if (this.priorityChanged)
            {
                update.Add("priority", new JArray(new JObject(new JProperty("set", this.priority.Id))));
            }
            if (this.statusChanged)
            {
                update.Add("status", new JArray(new JObject(new JProperty("set", this.status.Id))));
            }
            if (this.resolutionChanged)
            {
                update.Add("resolution", new JArray(new JObject(new JProperty("set", this.resolution.Id))));
            }
            if (this.assigneeChanged)
            {
                update.Add("assignee", new JArray(new JObject(new JProperty("set", this.assignee.Name))));
            }

            // class array fields
            if (this.componentsChanged)
            {
                update.Add("components", new JArray(
                    this.components.Except(this.componentsOrginal).Select(c => new JObject(new JProperty("add", c.Id))),
                    this.componentsOrginal.Except(this.components).Select(c => new JObject(new JProperty("remove", c.Id)))
                ));
            }
            if (this.fixVersionsChanged)
            {
                update.Add("fixVersions", new JArray(
                    this.fixVersions.Except(this.fixVersionsOrginal).Select(v => new JObject(new JProperty("add", v.Id))),
                    this.fixVersionsOrginal.Except(this.fixVersions).Select(v => new JObject(new JProperty("remove", v.Id)))
                ));
            }
            if (this.affectedversionsChanged)
            {
                update.Add("versions", new JArray(
                    this.affectedversions.Except(this.affectedversionsOrginal).Select(v => new JObject(new JProperty("add", v.Id))),
                    this.affectedversionsOrginal.Except(this.affectedversions).Select(v => new JObject(new JProperty("remove", v.Id)))
                ));
            }

            // date fields
            if (this.dueDateChanged)
            {
                update.Add("dueDate", new JArray(new JObject(new JProperty("set", this.dueDate))));
            }

            // custom fields
            foreach (CustomField customField in this.customFields)
            {
                Field fieldInfo = jira.GetCachedFieldsAsync().Result.Where(f => f.IsCustom && f.Name.Trim() == customField.Name.Trim()).FirstOrDefault();
                if (fieldInfo != null)
                {
                    if (customField.Changed)
                    {
                        switch (fieldInfo.Schema.Custom)
                        {
                        case "com.atlassian.jira.plugins.jira-importers-plugin:bug-importid":
                            CheckCustomFieldValue(customField, CustomFieldType.Double);
                            //update.Add(fieldInfo.Id, (string)customField.Value.Value);
                            update.Add(fieldInfo.Id, new JArray(new JObject(new JProperty("set", (string)customField.Value.Value))));
                            break;

                        case "com.atlassian.jira.plugin.system.customfieldtypes:cascadingselect":
                            CheckCustomFieldValue(customField, CustomFieldType.String);
                            //update.Add(fieldInfo.Id, new JObject(new JProperty("value", (string)customField.Value.Value)));
                            update.Add(fieldInfo.Id, new JArray(new JObject(new JProperty("set", (string)customField.Value.Value))));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:datepicker":
                            CheckCustomFieldValue(customField, CustomFieldType.DateTime);
                            //update.Add(fieldInfo.Id, (DateTime)customField.Value.Value);
                            update.Add(fieldInfo.Id, new JArray(new JObject(new JProperty("set", (DateTime)customField.Value.Value))));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:datetime":
                            //CheckCustomFieldValue(customField, CustomFieldType.DateTime);
                            //update.Add(fieldInfo.Id, (DateTime)customField.Value.Value);
                            update.Add(fieldInfo.Id, new JArray(new JObject(new JProperty("set", (DateTime)customField.Value.Value))));
                             break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:float":
                            CheckCustomFieldValue(customField, CustomFieldType.Double);
                            //update.Add(fieldInfo.Id, (double?)customField.Value.Value);
                            update.Add(fieldInfo.Id, new JArray(new JObject(new JProperty("set", (double)customField.Value.Value))));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:grouppicker":
                            CheckCustomFieldValue(customField, CustomFieldType.Group);
                            update.Add(fieldInfo.Id, new JObject(new JProperty("name", ((Group)customField.Value.Value).Name)));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:importid":
                            CheckCustomFieldValue(customField, CustomFieldType.Double);
                            //update.Add(fieldInfo.Id, (double?)customField.Value.Value);
                            update.Add(fieldInfo.Id, new JArray(new JObject(new JProperty("set", (double)customField.Value.Value))));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:labels":
                            CheckCustomFieldValue(customField, CustomFieldType.StringArray);
                            update.Add(fieldInfo.Id, new JArray((string[])customField.Value.Value));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:multicheckboxes":
                            CheckCustomFieldValue(customField, CustomFieldType.StringArray);
                            update.Add(fieldInfo.Id, new JArray(((string[])customField.Value.Value).Select(v => new JObject(new JProperty("value", v)))));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:multigrouppicker":
                            CheckCustomFieldValue(customField, CustomFieldType.GroupArray);
                            update.Add(fieldInfo.Id, new JArray(((Group[])customField.Value.Value).Select(g => new JObject(new JProperty("name", g.Name)))));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:multiselect":
                            CheckCustomFieldValue(customField, CustomFieldType.StringArray);
                            update.Add(fieldInfo.Id, new JArray(((string[])customField.Value.Value).Select(v => new JObject(new JProperty("value", v)))));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:multiuserpicker":
                            CheckCustomFieldValue(customField, CustomFieldType.UserArray);
                            update.Add(fieldInfo.Id, new JArray(((User[])customField.Value.Value).Select(u => new JObject(new JProperty("name", u.Name)))));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:multiversion":
                            CheckCustomFieldValue(customField, CustomFieldType.VersionArray);
                            update.Add(fieldInfo.Id, new JArray(((IssueVersion[])customField.Value.Value).Select(v => new JObject(new JProperty("id", v.Id)))));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:project":
                            CheckCustomFieldValue(customField, CustomFieldType.Project);
                            //update.Add(fieldInfo.Id, new JObject(new JProperty("id", ((Project)customField.Value.Value).Id)));
                            update.Add(fieldInfo.Id, new JArray(new JObject(new JProperty("set", ((Project)customField.Value.Value).Id))));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:radiobuttons":
                            CheckCustomFieldValue(customField, CustomFieldType.String);
                            //update.Add(fieldInfo.Id, new JObject(new JProperty("value", (string)customField.Value.Value)));
                            update.Add(fieldInfo.Id, new JArray(new JObject(new JProperty("set", (string)customField.Value.Value))));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:select":
                            CheckCustomFieldValue(customField, CustomFieldType.String);
                            //update.Add(fieldInfo.Id, new JObject(new JProperty("value", (string)customField.Value.Value)));
                            update.Add(fieldInfo.Id, new JArray(new JObject(new JProperty("set", (string)customField.Value.Value))));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:textarea":
                        case "com.atlassian.jira.plugin.system.customfieldtypes:textfield":
                            CheckCustomFieldValue(customField, CustomFieldType.String);
                            //update.Add(fieldInfo.Id, (string)customField.Value.Value);
                            update.Add(fieldInfo.Id, new JArray(new JObject(new JProperty("set", (string)customField.Value.Value))));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:url":
                            CheckCustomFieldValue(customField, CustomFieldType.String);
                            //update.Add(fieldInfo.Id, (string)customField.Value.Value);
                            update.Add(fieldInfo.Id, new JArray(new JObject(new JProperty("set", (string)customField.Value.Value))));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:userpicker":
                            CheckCustomFieldValue(customField, CustomFieldType.User);
                            //update.Add(fieldInfo.Id, new JObject(new JProperty("name", ((User)customField.Value.Value).Name)));
                            update.Add(fieldInfo.Id, new JArray(new JObject(new JProperty("set", ((User)customField.Value.Value).Name))));
                            break;
                        case "com.atlassian.jira.plugin.system.customfieldtypes:version":
                            CheckCustomFieldValue(customField, CustomFieldType.Version);
                            //update.Add(fieldInfo.Id, new JObject(new JProperty("id", ((IssueVersion)customField.Value.Value).Id)));
                            update.Add(fieldInfo.Id, new JArray(new JObject(new JProperty("set", ((IssueVersion)customField.Value.Value).Id))));
                            break;
                        default:
                            Trace.TraceWarning("Unsupported custom field schema custom entry '{0}' for field {1}.", fieldInfo.Schema.Custom, fieldInfo.Name);
                            break;
                        }
                        customField.Changed = false;
                    }
                }
                else
                {
                    Trace.TraceWarning("Custom field {0} not found", customField.Name);
                }
            }

            string x = update.ToString();

            // add update
            info.AddValue("update", update);
        }

        internal void UpdateCustomFields(IEnumerable<Field> fields)
        {
            foreach (CustomField customField in this.customFields)
            {
                if (string.IsNullOrEmpty(customField.Name))
                {
                    customField.Name = fields.Where(f => f.Id == customField.Id).Select(f => f.Name).First();
                }
                if (string.IsNullOrEmpty(customField.Id))
                {
                    customField.Id = fields.Where(f => f.Name == customField.Name).Select(f => f.Id).First();
                }
                //if (string.IsNullOrEmpty(customField.Type))
                //{
                //    customField.Type = fields.Where(f => f.Name == customField.Name).Select(f => f.Schema.Type).First();
                //}
            }
        }

        internal void ResetAllChanged()
        {
            this.affectedversionsOrginal = this.affectedversions;
            this.affectedversionsChanged = false;
            this.assigneeChanged = false;
            this.componentsOrginal = this.components;
            this.componentsChanged = false;
            this.descriptionChanged = false;
            this.dueDateChanged = false;
            this.environmentChanged = false;
            this.fixVersionsOrginal = this.fixVersions;
            this.fixVersionsChanged = false;
            this.issueTypeChanged = false;
            this.labelsOrginal = this.labels;
            this.labelsChanged = false;
            this.priorityChanged = false;
            this.projectChanged = false;
            this.reporterChanged = false;
            this.resolutionChanged = false;
            this.statusChanged = false;
            this.summaryChanged = false;
            this.timeTrackingChanged = false;
        }

        private void AddCustomField<T>(Field fieldInfo, JToken value, CustomFieldType type)
        {
            if (value.HasValues)
            {
                var val = value.ToObject<T>();
                this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue(type, val)));
            }
            else
            {
                this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue(type)));
            }
        }

        private void AddCustomFieldArray<T>(Field fieldInfo, JToken value, CustomFieldType type)
        {
            if (value.HasValues)
            {
                var val = ((JArray)value).Select(v => ((JObject)v).ToObject<T>() ).ToArray();
                this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue(type, val)));
            }
            else
            {
                this.customFields.Add(new CustomField(fieldInfo, new CustomFieldValue(type)));
            }
        }

        #endregion

        /// <summary>
        /// Support of the JQL 'issueHistory()' function in LINQ.
        /// </summary>
        /// <returns>Not used.</returns>
        [JqlFunction("issueHistory")]
        public static Issue[] IssueHistory()
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'linkedIssues()' function in LINQ.
        /// </summary>
        /// <returns>Not used.</returns>
        [JqlFunction("linkedIssues")]
        public static Issue[] LinkedIssues()
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'votedIssues()' function in LINQ.
        /// </summary>
        /// <returns>Not used.</returns>
        [JqlFunction("votedIssues")]
        public static Issue[] VotedIssues()
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'watchedIssues()' function in LINQ.
        /// </summary>
        /// <returns>Not used.</returns>
        [JqlFunction("watchedIssues")]
        public static Issue[] WatchedIssues()
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        #region fields

        /// <summary>
        /// Name of the classes which should be expanded.
        /// </summary>
        /// <remarks>Not useable by LINQ.</remarks>
        public string Expand { get; private set; }

        /// <summary>
        /// Url of the JIRA REST item.
        /// </summary>
        /// <remarks>Not useable by LINQ.</remarks>
        public Uri Self { get; private set; }

        /// <summary>
        /// Id of the JIRA issue.
        /// </summary>
        [JqlFieldAttribute("id", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable)]
        public string Id { get; private set; }

        /// <summary>
        /// Summary of the JIRA issue.
        /// </summary>
        [JqlFieldAttribute("key", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable | JqlFieldCompare.Include)]
        public SortableString Key { get; set; }

        /// <summary>
        /// Σ Progress
        /// </summary>
        public Progress AggregateProgress { get; private set; }

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

        /// <summary>
        /// Project version(s) for which the issue is (or was) manifesting.
        /// </summary>
        [JqlFieldAttribute("affectedVersion", JqlFieldCompare.Comparable | JqlFieldCompare.Sortable | JqlFieldCompare.Include)]
        public ComparableList<IssueVersion> AffectedVersions
        {
            get
            {
                return this.affectedversions;
            }
            set
            {
                if (this.affectedversions != value)
                {
                    this.affectedversions = value;
                    this.affectedversionsChanged = true;
                }
            }
        }
        private bool affectedversionsChanged;
        private ComparableList<IssueVersion> affectedversionsOrginal;
        private ComparableList<IssueVersion> affectedversions;

        /// <summary>
        /// The person to whom the issue is currently assigned.
        /// </summary>
        [JqlFieldAttribute("assignee", JqlFieldCompare.Comparable | JqlFieldCompare.Include | JqlFieldCompare.Was | JqlFieldCompare.WasInclude | JqlFieldCompare.Changed)]
        public User Assignee
        {
            get
            {
                return this.assignee;
            }
            set
            {
                if (this.assignee != value)
                {
                    this.assignee = value;
                    this.assigneeChanged = true;
                }
            }
        }
        private bool assigneeChanged;
        private User assignee;

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
        public string Description
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
        private string description;

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

                if (value != null)
                    this.timeTrackingChanged = true;
                else
                    this.timeTrackingChanged = false;
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

            set { this.watchers.Users = value; }
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
        public IEnumerable<Worklog> Worklogs { get { return this.worklogs != null ? this.worklogs.Worklogs: null; } }

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
        public CustomFieldValue this[string name]
        {
            get
            {
                return this.customFields.Where(c => c.Name.Trim() == name.Trim()).Select(c => c.Value).FirstOrDefault();
            }
            set
            {
                CustomField customField = this.customFields.Where(c => c.Name.Trim() == name.Trim()).FirstOrDefault();
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
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Issue issue = obj as Issue;
            if ((object)issue == null)
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
            return issue != null && key == issue.Key;
        }

        /// <summary>
        ///  Compare not equal operator.
        /// </summary>
        /// <param name="key">Key of the first issue to compare.</param>
        /// <param name="issue">The second issue to compare.</param>
        /// <returns>true if the id of the first element is different from the id of the second element; otherwise, false.</returns>
        public static bool operator !=(string key, Issue issue)
        {
            return issue == null || key != issue.Key;
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
        public static implicit operator string(Issue issue)
        {
            return issue != null ? issue.Key : null;
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
}



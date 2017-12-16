using JiraWebApi.Internal;
using JiraWebApi.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// JIRA Wep Api main class.
    /// </summary>
    public sealed class Jira : IDisposable
    {
        private Uri host;
        private WebRequestHandler handler;
        private HttpClient client;
        private bool disposed = false;
        private readonly JiraQueryProvider provider;
        private IEnumerable<MediaTypeFormatter> jsonFormatters;
        private JsonMediaTypeFormatter jsonMediaTypeFormatter;
        
        /// <summary>
        /// Initializes a new instance of the Jira class.
        /// </summary>
        /// <param name="host">Host URL of the JIRA server.</param>
        /// <param name="username">Name of the user to login.</param>
        /// <param name="password">Password of the user to login.</param>
        /// <example>
        /// <code>
        /// using (Jira jira = new Jira(new Uri("https://jira.atlassian.com")))
        /// {
        ///     LoginInfo loginInfo = jira.GetLoginInfoAsync().Result;
        /// }
        /// </code>
        /// </example>
        public Jira(Uri host, string username = null, string password = null)
        {
            if (host == null)
            {
                throw new ArgumentNullException("host");
            }
            
            this.host = host;
            this.provider = new JiraQueryProvider(this);

            // connect
            this.handler = new WebRequestHandler();
            this.handler.CookieContainer = new System.Net.CookieContainer();
            this.handler.UseCookies = true;
            this.client = new HttpClient(this.handler);
            this.client.BaseAddress = host;
            
            this.jsonMediaTypeFormatter = new JsonMediaTypeFormatter()
            { 
                SerializerSettings = new JsonSerializerSettings()
                { 
                    Context = new StreamingContext(StreamingContextStates.All, this),
                    NullValueHandling = NullValueHandling.Ignore,
                    //DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind
                }
            };
            this.jsonFormatters = new MediaTypeFormatter[] { this.jsonMediaTypeFormatter };
            
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                this.Login(username, password);
            }
        }

        /// <summary>
        /// Release allocated resources.
        /// </summary>
        /// <remarks>
        /// Short dispose handling because of sealed class.
        /// </remarks>
        public void Dispose()
        {
            if (!this.disposed)
            {
                if (this.client != null)
                {
                    this.client.Dispose();
                    this.client = null;
                }
                if (this.handler != null)
                {
                    this.handler.Dispose();
                    this.handler = null;
                }

                // note disposing has been done.
                this.disposed = true;
            }
            GC.SuppressFinalize(this);
        }

        #region Login

        /// <summary>
        /// Creates a new session for a user in JIRA.
        /// </summary>
        /// <param name="username">Name of the user to login.</param>
        /// <param name="password">Password of the user to login.</param>
        public void Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("The username is null or empty.");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("The password is null or empty.");
            }

            SessionPostRequest req = new SessionPostRequest() { Username = username, Password = password };
            JsonTrace.WriteRequest(this, req);            
            using (HttpResponseMessage response = this.client.PostAsJsonAsync("rest/auth/1/session", req).Result)
            {
                response.EnsureSuccess();
                SessionPostResult res = response.Content.ReadAsAsync<SessionPostResult>().Result;
            }
        }

        /// <summary>
        /// Creates a new session for a user in JIRA.
        /// </summary>
        /// <param name="username">Name of the user to login.</param>
        /// <param name="password">Password of the user to login.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task LoginAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("The username is null or empty.");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("The password is null or empty.");
            }

            SessionPostRequest req = new SessionPostRequest() { Username = username, Password = password };
            JsonTrace.WriteRequest(this, req);            
            using (HttpResponseMessage response = await this.client.PostAsJsonAsync("rest/auth/1/session", req))
            {
                response.EnsureSuccess();
                SessionPostResult res = await response.Content.ReadAsAsync<SessionPostResult>();
            }
        }

        /// <summary>
        /// Logs the current user out of JIRA, destroying the existing session, if any.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task LogoutAsync()
        {
            using (HttpResponseMessage response = await this.client.DeleteAsync("rest/auth/1/session"))
            {
                response.EnsureSuccess();
            }
        }

        /// <summary>
        /// Returns information about the currently authenticated user's session.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<LoginInfo> GetLoginInfoAsync()
        {
            SessionGetResult res = null;
            using (HttpResponseMessage response = await this.client.GetAsync("rest/auth/1/session"))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<SessionGetResult>();
            }
            return res != null ? res.LoginInfo : null;
        }

        #endregion

        #region ServerInfo

        /// <summary>
        /// Returns general information about the current JIRA server.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<ServerInfo> GetServerInfoAsync()
        {
            ServerInfo res = null;

            using (HttpResponseMessage response = await this.client.GetAsync("rest/api/2/serverInfo"))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<ServerInfo>();
            }
            return res;
        }

        #endregion

        #region IssueType

        /// <summary>
        /// Returns a list of all issue types visible to the user.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IEnumerable<IssueType>> GetIssueTypesAsync()
        {
            IEnumerable<IssueType> res = null;

            using (HttpResponseMessage response = await this.client.GetAsync("rest/api/2/issuetype"))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<IEnumerable<IssueType>>();
            }
            return res;
        }

        /// <summary>
        /// Returns a full representation of the issue type that has the given id.
        /// </summary>
        /// <param name="issueTypeId">Id of the issue type.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IssueType> GetIssueTypeAsync(string issueTypeId)
        {
            if (string.IsNullOrEmpty(issueTypeId))
            {
                throw new ArgumentException("The issueTypeId is null or empty.");
            }
            
            IssueType res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/issuetype/{0}", issueTypeId)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<IssueType>();
            }
            return res;
        }

        #endregion

        #region Priority

        /// <summary>
        /// Returns a list of all issue priorities.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IEnumerable<Priority>> GetPrioritiesAsync()
        {
            IEnumerable<Priority> res = null;

            using (HttpResponseMessage response = await this.client.GetAsync("rest/api/2/priority"))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<IEnumerable<Priority>>();
            }
            return res;
        }

        /// <summary>
        /// Returns an issue priority.
        /// </summary>
        /// <param name="priorityId">Id of the priority.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Priority> GetPriorityAsync(string priorityId)
        {
            if (string.IsNullOrEmpty(priorityId))
            {
                throw new ArgumentException("The priorityId is null or empty.");
            }

            Priority res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/priority/{0}", priorityId)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<Priority>();
            }
            return res;
        }

        #endregion

        #region Resolution

        /// <summary>
        /// Returns a list of all resolutions.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IEnumerable<Resolution>> GetResolutionsAsync()
        {
            IEnumerable<Resolution> res = null;

            using (HttpResponseMessage response = await this.client.GetAsync("rest/api/2/resolution"))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<IEnumerable<Resolution>>();
            }
            return res;
        }

        /// <summary>
        /// Returns a resolution.
        /// </summary>
        /// <param name="resolutionId">Id of the resolution.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Resolution> GetResolutionAsync(string resolutionId)
        {
            if (string.IsNullOrEmpty(resolutionId))
            {
                throw new ArgumentException("The resolutionId is null or empty.");
            }

            Resolution res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/resolution/{0}", resolutionId)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<Resolution>();
            }
            return res;
        }

        #endregion

        #region Status

        /// <summary>
        /// Returns a list of all statuses.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IEnumerable<Status>> GetStatusesAsync()
        {
            IEnumerable<Status> res = null;
            using (HttpResponseMessage response = await this.client.GetAsync("rest/api/2/status"))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<IEnumerable<Status>>();
            }
            return res;
        }

        /// <summary>
        /// Returns a full representation of the Status having the given id or name.
        /// </summary>
        /// <param name="statusIdOrName">Id or name of the status.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Status> GetStatusAsync(string statusIdOrName)
        {
            if (string.IsNullOrEmpty(statusIdOrName))
            {
                throw new ArgumentException("The statusIdOrName is null or empty.");
            }

            Status res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/status/{0}", statusIdOrName)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<Status>();
            }
            return res;
        }

        #endregion

        #region IssueLinkType

        /// <summary>
        /// Returns a list of available issue link types, if issue linking is enabled. Each issue link type has an id, a name and a label for the outward and inward link relationship.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IEnumerable<IssueLinkType>> GetIssueLinkTypesAsync()
        {
            IssueLinkTypesRespnse res = null;
            using (HttpResponseMessage response = await this.client.GetAsync("rest/api/2/issueLinkType"))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<IssueLinkTypesRespnse>();
            }
            return res != null ? res.IssueLinkTypes : null;
        }

        /// <summary>
        /// Returns for a given issue link type id all information about this issue link type.
        /// </summary>
        /// <param name="issueLinkTypeId">Id of the link type.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IssueLinkType> GetIssueLinkTypeAsync(string issueLinkTypeId)
        {
            if (string.IsNullOrEmpty(issueLinkTypeId))
            {
                throw new ArgumentException("The issueLinkTypeId is null or empty.");
            }

            IssueLinkType res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/issueLinkType/{0}", issueLinkTypeId)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<IssueLinkType>();
            }
            return res;
        }

        #endregion

        #region Field

        /// <summary>
        /// Returns a list of all fields, both System and Custom.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IEnumerable<Field>> GetFieldsAsync()
        {
            IEnumerable<Field> res = null;
            using (HttpResponseMessage response = await this.client.GetAsync("rest/api/2/field"))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<IEnumerable<Field>>();
            }
            return res;
        }

        /// <summary>
        /// Returns a full representation of the Custom Field Option that has the given id.
        /// </summary>
        /// <param name="customFieldOptionId">Id of the custom field option.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<CustomFieldOption> GetCustomFieldOptionAsync(string customFieldOptionId)
        {
            if (string.IsNullOrEmpty(customFieldOptionId))
            {
                throw new ArgumentException("The customFieldOptionId is null or empty.");
            }

            CustomFieldOption res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/customFieldOption/{0}", customFieldOptionId)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<CustomFieldOption>();
            }
            return res;
        }

        #endregion

        #region Group

        /// <summary>
        /// Get group with specified group name.
        /// </summary>
        /// <param name="groupName">Name of the group</param>
        /// <param name="expandGroup">Expand group parameter.</param>
        /// <returns>Jira group.</returns>
        /// <remarks>Only supported with JIRA 6.0 or later</remarks>
        public async Task<Group> GetGroupAsync(string groupName, string expandGroup = null) 
        {
            if (string.IsNullOrEmpty(groupName))
            {
                throw new ArgumentException("The groupName is null or empty.");
            }

            Group res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/group?groupname={0}{1}", groupName, string.IsNullOrEmpty(expandGroup) ? "" : "&expand=" + expandGroup)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<Group>();
            }
            return res;
        }

        #endregion

        #region User

        /// <summary>
        /// Returns a user.
        /// </summary>
        /// <param name="username">Name of the user.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks>This resource cannot be accessed anonymously.</remarks>
        public async Task<User> GetUserAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("The username is null or empty.");
            }

            User res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/user?username={0}", username)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<User>();
            }
            return res;
        }

        #endregion

        #region Filter

        /// <summary>
        /// Creates a new filter, and returns newly created filter Currently sets permissions just using the users default sharing permissions.
        /// </summary>
        /// <param name="filter">Filter class to create.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks>Only for JIRA 5.2.1 or later.</remarks>
        public async Task<Filter> CreateFilterAsync(Filter filter)
        {
            if (filter == (Filter)null)
            {
                throw new ArgumentNullException("filter");
            }

            //FilterPostRequest req = new FilterPostRequest() { Name = filter.Name, Description = filter.Description, Jql = filter.Jql };
            Filter res = null;
            JsonTrace.WriteRequest(this, filter);            
            using (HttpResponseMessage response = await this.client.PostAsJsonAsync("rest/api/2/filter", filter))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<Filter>();
            }
            return res;
        }

        /// <summary>
        /// Returns the favourite filters of the logged-in user.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IEnumerable<Filter>> GetFilterFavouritesAsync()
        {
            IEnumerable<Filter> res = null;
            using (HttpResponseMessage response = await this.client.GetAsync("rest/api/2/filter/favourite"))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<IEnumerable<Filter>>();
            }
            return res;
        }

        /// <summary>
        /// Returns a filter given an id.
        /// </summary>
        /// <param name="filterId">Id of the filter.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Filter> GetFilterAsync(string filterId)
        {
            if (string.IsNullOrEmpty(filterId))
            {
                throw new ArgumentException("The filterId is null or empty.");
            }

            Filter res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/filter/{0}", filterId)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<Filter>();
            }
            return res;
        }

        /// <summary>
        /// Updates an existing filter, and returns its new value.
        /// </summary>
        /// <param name="filter">Filter class to update.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks>Only for JIRA 5.2.1 or later.</remarks>
        public async Task<Filter> UpdateFilterAsync(Filter filter)
        {
            if (filter == (Filter)null)
            {
                throw new ArgumentNullException("filter");
            }

            Filter res = null;
            using (HttpResponseMessage response = await this.client.PutAsJsonAsync<Filter>(string.Format("rest/api/2/filter/{0}", filter.Id), filter))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<Filter>();
            } 
            return res;
        }

        /// <summary>
        /// Delete a filter.
        /// </summary>
        /// <param name="filterId">Id of the filter to delete.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks>Only for JIRA 5.2.1 or later.</remarks>
        public async Task DeleteFilterAsync(string filterId)
        {
            if (string.IsNullOrEmpty(filterId))
            {
                throw new ArgumentException("The filterId is null or empty.");
            }

            using (HttpResponseMessage response = await this.client.DeleteAsync(string.Format("rest/api/2/filter/{0}", filterId)))
            {
                response.EnsureSuccess();
            }
        }

        #endregion

        #region Component

        /// <summary>
        /// Contains a full representation of a the specified project's components.
        /// </summary>
        /// <param name="projectKey">Key of the project.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IEnumerable<Component>> GetComponentsAsync(string projectKey)
        {
            string function = MethodBase.GetCurrentMethod().Name;
            if (string.IsNullOrEmpty(projectKey))
            {
                throw new ArgumentException("The projectKey is null or empty.");
            }

            IEnumerable<Component> res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/project/{0}/components", projectKey)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<IEnumerable<Component>>();
            }
            return res;
        }

        /// <summary>
        /// Create a component.
        /// </summary>
        /// <param name="component">Component class to create.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Component> CreateComponentAsync(Component component)
        {
            if (component == (Component)null)
            {
                throw new ArgumentNullException("component");
            }

            Component res = null;
            JsonTrace.WriteRequest(this, component);            
            using (HttpResponseMessage response = await this.client.PostAsJsonAsync("rest/api/2/component", component))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<Component>();
            }
            return res;
        }

        /// <summary>
        /// Returns a project component.
        /// </summary>
        /// <param name="componentId">Id of the component to get.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Component> GetComponentAsync(string componentId)
        {
            if (string.IsNullOrEmpty(componentId))
            {
                throw new ArgumentException("The componentId is null or empty.");
            }

            Component res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/component/{0}", componentId)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<Component>();
            }
            return res;
        }

        /// <summary>
        /// Modify a component.
        /// </summary>
        /// <param name="component">Component class to update.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Component> UpdateComponentAsync(Component component)
        {
            if (component == (Component)null)
            {
                throw new ArgumentNullException("component");
            }

            Component res = null;
            using (HttpResponseMessage response = await this.client.PutAsJsonAsync(string.Format("rest/api/2/component/{0}", component.Id), component))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<Component>();
            }
            return res;
        }

        /// <summary>
        /// Delete a project component.
        /// </summary>
        /// <param name="componentId">Id of the component to delete.</param>
        /// <param name="moveIssuesTo">The new component applied to issues whose 'id' component will be deleted. If this value is null, then the 'id' component is simply removed from the related isues.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task DeleteComponentAsync(string componentId, string moveIssuesTo = null)
        {
            if (string.IsNullOrEmpty(componentId))
            {
                throw new ArgumentException("The componentId is null or empty.");
            }

            using (HttpResponseMessage response = await this.client.DeleteAsync(string.Format("rest/api/2/component/{0}?{1}", componentId, moveIssuesTo != null ? moveIssuesTo : "")))
            {
                response.EnsureSuccess();
            }
        }

        /// <summary>
        /// Returns counts of issues related to this component.
        /// </summary>
        /// <param name="componentId">Id of the component.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<int> ComponentRelatedIssuesCountAsync(string componentId)
        {
            if (string.IsNullOrEmpty(componentId))
            {
                throw new ArgumentException("The componentId is null or empty.");
            } 

            ComponentRelatedIssueCounts res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/component/{0}/relatedIssueCounts", componentId)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<ComponentRelatedIssueCounts>();
            }
            return res.IssueCount;
        }

        #endregion

        #region Version

        /// <summary>
        /// Contains a full representation of a the specified project's versions.
        /// </summary>
        /// <param name="projectKey">Key of the project.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IEnumerable<IssueVersion>> GetVersionsAsync(string projectKey)
        {
            if (string.IsNullOrEmpty(projectKey))
            {
                throw new ArgumentException("The projectKey is null or empty.");
            }

            IEnumerable<IssueVersion> res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/project/{0}/versions", projectKey)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<IEnumerable<IssueVersion>>();
            }
            return res;
        }

        /// <summary>
        /// Create a version.
        /// </summary>
        /// <param name="version">Class of the version to create.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IssueVersion> CreateVersionAsync(IssueVersion version)
        {
            if (version == (IssueVersion)null)
            {
                throw new ArgumentNullException("version");
            }

            IssueVersion res = null;
            JsonTrace.WriteRequest(this, version);
            using (HttpResponseMessage response = await this.client.PostAsJsonAsync("rest/api/2/version", version))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<IssueVersion>();
            }
            return res;
        }

        /// <summary>
        /// Returns a project version.
        /// </summary>
        /// <param name="versionId">Id of the version.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IssueVersion> GetVersionAsync(string versionId)
        {
            if (string.IsNullOrEmpty(versionId))
            {
                throw new ArgumentException("The versionId is null or empty.");
            }

            IssueVersion res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/version/{0}", versionId)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<IssueVersion>();
            }
            return res;
        }

        /// <summary>
        /// Modify a version.
        /// </summary>
        /// <param name="version">Class of the version to update.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IssueVersion> UpdateVersionAsync(IssueVersion version)
        {
            if (version == (IssueVersion)null)
            {
                throw new ArgumentNullException("version");
            }

            IssueVersion res = null;
            using (HttpResponseMessage response = await this.client.PutAsJsonAsync(string.Format("rest/api/2/version/{0}", version.Id), version))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<IssueVersion>();
            }
            return res;
        }

        /// <summary>
        /// Delete a project version.
        /// </summary>
        /// <param name="versionId">Id of the version to delete.</param>
        /// <param name="moveFixIssuesTo">Id of the version to move fix issues to.</param>
        /// <param name="moveAffectedIssuesTo">Id of the version to move affected issues to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task DeleteVersionAsync(string versionId, string moveFixIssuesTo = null, string moveAffectedIssuesTo = null)
        {
            if (string.IsNullOrEmpty(versionId))
            {
                throw new ArgumentException("The versionId is null or empty.");
            }

            using (HttpResponseMessage response = await this.client.DeleteAsync(string.Format("rest/api/2/version/{0}?{1}{2}", versionId, moveFixIssuesTo != null ? moveFixIssuesTo : "", moveAffectedIssuesTo != null ? moveAffectedIssuesTo : "")))
            {
                response.EnsureSuccess();
            }
        }

        /// <summary>
        /// Modify a version's sequence within a project. 
        /// </summary>
        /// <param name="versionId">Id of the version to move.</param>
        /// <param name="versionIdAfter">Id of the version to move after.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IssueVersion> MoveVersionAsync(string versionId, string versionIdAfter)
        {
            if (string.IsNullOrEmpty(versionId))
            {
                throw new ArgumentException("The versionId is null or empty.");
            }
            if (string.IsNullOrEmpty(versionIdAfter))
            {
                throw new ArgumentException("The versionIdAfter is null or empty.");
            }

            VersionMoveAfterPostRequest req = new VersionMoveAfterPostRequest() { After = versionIdAfter };
            IssueVersion res = null;
            JsonTrace.WriteRequest(this, req);
            using (HttpResponseMessage response = await this.client.PostAsJsonAsync(string.Format("rest/api/2/version/{0}/move", versionId), req))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<IssueVersion>();
            }
            return res;
        }

        /// <summary>
        /// Modify a version's sequence within a project. 
        /// </summary>
        /// <param name="versionId">Id of the version to move.</param>
        /// <param name="position">Position to move the version to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IssueVersion> MoveVersionAsync(string versionId, Position position)
        {
            if (string.IsNullOrEmpty(versionId))
            {
                throw new ArgumentException("The versionId is null or empty.");
            }

            VersionMovePositionPostRequest req = new VersionMovePositionPostRequest() { Position = position };
            IssueVersion res = null;
            JsonTrace.WriteRequest(this, req);
            using (HttpResponseMessage response = await this.client.PostAsJsonAsync(string.Format("rest/api/2/version/{0}/move", versionId), req))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<IssueVersion>();
            }
            return res;
        }

        /// <summary>
        /// Returns a bean containing the number of fixed in and affected issues for the given version.
        /// </summary>
        /// <param name="versionId">Id of the version.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<int> VersionRelatedIssuesCountsAsync(string versionId)
        {
            if (string.IsNullOrEmpty(versionId))
            {
                throw new ArgumentException("The versionId is null or empty.");
            }

            VersionRelatedIssueCounts res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/version/{0}/relatedIssueCounts", versionId)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<VersionRelatedIssueCounts>();
            }
            return res.IssuesAffectedCount;
        }

        /// <summary>
        /// Returns the number of unresolved issues for the given version.
        /// </summary>
        /// <param name="versionId">Id of the version.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<int> VersionUnresolvedIssueCountAsync(string versionId)
        {
            if (string.IsNullOrEmpty(versionId))
            {
                throw new ArgumentException("The versionId is null or empty.");
            }

            VersionUnresolvedIssueCount res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/version/{0}/unresolvedIssueCount", versionId)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<VersionUnresolvedIssueCount>();
            }
            return res.IssuesUnresolvedCount;
        }

        #endregion

        #region Project

        /// <summary>
        /// Returns all projects which are visible for the currently logged in user. If no user is logged in, it returns the list of projects that are visible when using anonymous access.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks>Only the fields Self, Id, Key, Name and AvatarUrls will be filled by GetProjectsAsync. Call GetProjectAsync to get all fields. </remarks>
        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            IEnumerable<Project> res = null;

            using (HttpResponseMessage response = await this.client.GetAsync("rest/api/2/project"))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<IEnumerable<Project>>();
            }
            return res;
        }

        /// <summary>
        /// Contains a full representation of a project in JSON format.
        /// </summary>
        /// <param name="projectKey">Key of the project.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Project> GetProjectAsync(string projectKey)
        {
            if (string.IsNullOrEmpty(projectKey))
            {
                throw new ArgumentException("The projectKey is null or empty.");
            }

            Project res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/project/{0}", projectKey)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<Project>();
            }
            return res;
        }

        #endregion

        #region Comment

        /// <summary>
        /// Returns all comments for an issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IEnumerable<Comment>> GetCommentsAsync(string issueIdOrKey)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }

            CommentGetResult res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/issue/{0}/comment", issueIdOrKey)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<CommentGetResult>();
            }
            return res.Comments;
        }
        
        /// <summary>
        /// Adds a new comment to an issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="comment">Comment class to add.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Comment> AddCommentAsync(string issueIdOrKey, Comment comment)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (comment == null)
            {
                throw new ArgumentNullException("comment");
            }

            Comment res = null;
            JsonTrace.WriteRequest(this, comment);
            using (HttpResponseMessage response = await this.client.PostAsJsonAsync<Comment>(string.Format("rest/api/2/issue/{0}/comment", issueIdOrKey), comment))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<Comment>();
            }
            return res;
        }

        /// <summary>
        /// Returns all comments for an issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="commentId">Id of the comment.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Comment> GetCommentAsync(string issueIdOrKey, string commentId)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (string.IsNullOrEmpty(commentId))
            {
                throw new ArgumentException("The commentId is null or empty.");
            }
            
            Comment res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/issue/{0}/comment/{1}", issueIdOrKey, commentId)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<Comment>();
            }
            return res;
        }

        /// <summary>
        /// Updates an existing comment.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="comment">Class of the comment to update.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks>Json bug in JIRA 5.0.4</remarks>
        public async Task<Comment> UpdateCommentAsync(string issueIdOrKey, Comment comment)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (comment == null)
            {
                throw new ArgumentNullException("comment");
            }

            // TODO find a way to suppress deserialize
            DateTime? created = comment.Created;
            DateTime? updated = comment.Updated;
            comment.Created = null;
            comment.Updated = null;
            Comment res = null;
            using (HttpResponseMessage response = await this.client.PutAsJsonAsync<Comment>(string.Format("rest/api/2/issue/{0}/comment/{1}", issueIdOrKey, comment.Id), comment))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<Comment>();
            }
            comment.Created = created;
            comment.Updated = updated;
            return res;
        }

        /// <summary>
        /// Deletes an existing comment.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="commentId">Id of the comment.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task DeleteCommentAsync(string issueIdOrKey, string commentId)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (string.IsNullOrEmpty(commentId))
            {
                throw new ArgumentException("The commentId is null or empty.");
            }
            
            using (HttpResponseMessage response = await this.client.DeleteAsync(string.Format("rest/api/2/issue/{0}/comment/{1}", issueIdOrKey, commentId)))
            {
                response.EnsureSuccess();
            }
        }

        #endregion

        #region Meta

        /// <summary>
        /// Returns the meta data for creating issues. This includes the available projects, issue types and fields, 
        /// including field types and whether or not those fields are required. 
        /// Projects will not be returned if the user does not have permission to create issues in that project. 
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<CreateMeta> GetCreateMetaAsync(/*string projectKey, string issueTypeId*/)
        {
            CreateMeta res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/issue/createmeta?expand=projects.issuetypes.fields"/*?projectKeys={0}&issuetypeIds={1}", projectKey, issueTypeId*/)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<CreateMeta>();
            }
            return res;
        }

        /// <summary>
        /// Returns the meta data for editing an issue. 
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<EditMeta> GetEditMetaAsync(string issueIdOrKey)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }

            EditMeta res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/issue/{0}/editmeta", issueIdOrKey)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<EditMeta>();
            }
            return res;
        }

        #endregion
        
        #region Notify

        /// <summary>
        /// Sends a notification (email) to the list or recipients defined in the request.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="notify">Class with the notify nformations.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks>Only for JIRA 5.2.1 or later.</remarks>
        public async Task SendNotifyAsync(string issueIdOrKey, Notify notify)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (notify == null)
            {
                throw new ArgumentNullException("notify");
            }

            JsonTrace.WriteRequest(this, notify);
            using (HttpResponseMessage response = await this.client.PostAsJsonAsync<Notify>(string.Format("rest/api/2/issue/{0}/notify", issueIdOrKey), notify))
            {
                response.EnsureSuccess();
            }
        }

        #endregion

        #region RemoteLink

        /// <summary>
        /// A representing the remote issue links on the issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IEnumerable<RemoteLink>> GetIssueRemoteLinksAsync(string issueIdOrKey)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }

            IEnumerable<RemoteLink> res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/issue/{0}/remotelink", issueIdOrKey)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<IEnumerable<RemoteLink>>();
            }
            return res;
        }

        //public async Task<RemoteLink> GetIssueRemoteLinkAsync(string issueIdOrKey, string globalId)
        //{
        //    IEnumerable<RemoteLink> res = null;
        //    using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/issue/{0}/remotelink?globalId={1}", issueIdOrKey, globalId)))
        //    {
        //        response.EnsureSuccess();
        //        res = await response.Content.ReadAsAsync<IEnumerable<RemoteLink>>();
        //    }
        //    return res.ElementAtOrDefault(0);
        //}

        /// <summary>
        /// Creates or updates a remote issue link from a JSON representation.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="remoteLink">Class of the remote link to create.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks>If a globalId is provided and a remote issue link exists with that globalId, the remote issue link is updated. Otherwise, the remote issue link is created.</remarks>
        public async Task<RemoteLink> CreateIssueRemoteLinkAsync(string issueIdOrKey, RemoteLink remoteLink)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (remoteLink == null)
            {
                throw new ArgumentNullException("remoteLink");
            }

            RemoteLink res = null;
            JsonTrace.WriteRequest(this, remoteLink);
            using (HttpResponseMessage response = await this.client.PostAsJsonAsync<RemoteLink>(string.Format("rest/api/2/issue/{0}/remotelink", issueIdOrKey), remoteLink))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<RemoteLink>();
            }
            return res;
        }

        //public async Task DeleteIssueRemoteLinkAsync(string issueIdOrKey, string globalId)
        //{
        //    using (HttpResponseMessage response = await this.client.DeleteAsync(string.Format("rest/api/2/issue/{0}/remotelink?globalId={1}", issueIdOrKey, globalId)))
        //    {
        //        response.EnsureSuccess();
        //    }
        //}

        /// <summary>
        /// Get the remote issue link with the given id on the issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="remoteLinkId">Id of the remote link.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<RemoteLink> GetIssueRemoteLinkAsync(string issueIdOrKey, string remoteLinkId)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (string.IsNullOrEmpty(remoteLinkId))
            {
                throw new ArgumentException("The remoteLinkId is null or empty.");
            }

            RemoteLink res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/issue/{0}/remotelink/{1}", issueIdOrKey, remoteLinkId)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<RemoteLink>();
            }
            return res;
        }

        /// <summary>
        /// Updates a remote issue link.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="remoteLink">Remote link to update.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task UpdateIssueRemoteLinkAsync(string issueIdOrKey, RemoteLink remoteLink)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (remoteLink == null)
            {
                throw new ArgumentNullException("remoteLink");
            }

            using (HttpResponseMessage response = await this.client.PutAsJsonAsync<RemoteLink>(string.Format("rest/api/2/issue/{0}/remotelink/{1}", issueIdOrKey, remoteLink.Id), remoteLink))
            {
                response.EnsureSuccess();
            }
        }

        /// <summary>
        /// Delete the remote issue link with the given global id on the issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="remoteLinkId">Id of the remote link.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task DeleteIssueRemoteLinkAsync(string issueIdOrKey, string remoteLinkId)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (string.IsNullOrEmpty(remoteLinkId))
            {
                throw new ArgumentException("The remoteLinkId is null or empty.");
            }

            using (HttpResponseMessage response = await this.client.DeleteAsync(string.Format("rest/api/2/issue/{0}/remotelink/{1}", issueIdOrKey, remoteLinkId)))
            {
                response.EnsureSuccess();
            }
        }

        #endregion

        #region Transition

        /// <summary>
        /// Get a list of the transitions possible for this issue by the current user, along with fields that are required and their types. 
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IEnumerable<Transition>> GetTransitionsAsync(string issueIdOrKey)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }

            TransitionGetResult res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/issue/{0}/transitions", issueIdOrKey)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<TransitionGetResult>();
            }
            return res.Transitions;
        }

        /// <summary>
        /// Get a list of the transitions possible for this issue by the current user, along with fields that are required and their types.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="transitionId">Id of the transition.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Transition> GetTransitionAsync(string issueIdOrKey, string transitionId)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (string.IsNullOrEmpty(transitionId))
            {
                throw new ArgumentException("The transitionId is null or empty.");
            }

            TransitionGetResult res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/issue/{0}/transitions?transitionId={1}", issueIdOrKey, transitionId)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<TransitionGetResult>();
            }
            return res.Transitions.FirstOrDefault();
        }

        /// <summary>
        /// Perform a transition on an issue. When performing the transition you can udate or set other issue fields. 
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="transition">Transition class.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task TransitionAsync(string issueIdOrKey, Transition transition)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (transition == null)
            {
                throw new ArgumentNullException("transition");
            }

            TransitionPostReq req = new TransitionPostReq() { Transition = transition };
            JsonTrace.WriteRequest(this, req);
            using (HttpResponseMessage response = await this.client.PostAsJsonAsync<TransitionPostReq>(string.Format("rest/api/2/issue/{0}/transitions", issueIdOrKey), req))
            {
                response.EnsureSuccess();
            }
        }

        #endregion

        #region Votes

        /// <summary>
        /// A resource representing the voters on the issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Votes> GetIssueVotesAsync(string issueIdOrKey)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }

            Votes res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/issue/{0}/votes", issueIdOrKey)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<Votes>();
            }
            return res;
        }

        /// <summary>
        /// Cast your vote in favour of an issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task AddIssueVoteAsync(string issueIdOrKey)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }

            using (HttpResponseMessage response = await this.client.PostAsync(string.Format("rest/api/2/issue/{0}/votes", issueIdOrKey), null))
            {
                response.EnsureSuccess();
            }
        }

        /// <summary>
        /// Remove your vote from an issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task DeleteIssueVoteAsync(string issueIdOrKey)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }

            using (HttpResponseMessage response = await this.client.DeleteAsync(string.Format("rest/api/2/issue/{0}/votes", issueIdOrKey)))
            {
                response.EnsureSuccess();
            }
        }

        #endregion

        #region Watchers

        /// <summary>
        /// Returns the list of watchers for the issue with the given key.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Watchers> GetIssueWatchersAsync(string issueIdOrKey)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }

            Watchers res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/issue/{0}/watchers", issueIdOrKey)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<Watchers>();
            }
            return res;
        }

        /// <summary>
        /// Adds a user to an issue's watcher list.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="username">Username of the new watcher.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task AddIssueWatcherAsync(string issueIdOrKey, string username)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("The username is null or empty.");
            }

            JsonTrace.WriteRequest(this, username);
            using (HttpResponseMessage response = await this.client.PostAsJsonAsync<string>(string.Format("rest/api/2/issue/{0}/watchers", issueIdOrKey), username))
            {
                response.EnsureSuccess();
            }
        }

        /// <summary>
        /// Removes a user from an issue's watcher list.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="username">Username of the watcher to delete.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task DeleteIssueWatcherAsync(string issueIdOrKey, string username)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("The username is null or empty.");
            }

            using (HttpResponseMessage response = await this.client.DeleteAsync(string.Format("rest/api/2/issue/{0}/watchers?username={1}", issueIdOrKey, username)))
            {
                response.EnsureSuccess();
            }
        }

        #endregion

        #region Workog

        /// <summary>
        /// Returns all work logs for an issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IEnumerable<Worklog>> GetIssueWorklogsAsync(string issueIdOrKey)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }

            WorklogGetResult res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/issue/{0}/worklog", issueIdOrKey)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<WorklogGetResult>();
            }
            return res.Worklogs;
        }

        /// <summary>
        /// Adds a new worklog entry to an issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="worklog">Worklog to add.</param>
        /// <param name="adjustEstimate">Adjust estimate flags.</param>
        /// <param name="value">Value for AdjustEstimate.New and AdjustEstimate.Manual.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task AddIssueWorklogAsync(string issueIdOrKey, Worklog worklog, AdjustEstimate adjustEstimate = AdjustEstimate.Auto, string value = null)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (worklog == null)
            {
                throw new ArgumentNullException("worklog");
            }

            string uri = null;
            switch (adjustEstimate)
            {
            case AdjustEstimate.New:
                uri = string.Format("rest/api/2/issue/{0}/worklog?adjustEstimate=new&newEstimate={1}", issueIdOrKey, value);
                break;
            case AdjustEstimate.Leave:
                uri = string.Format("rest/api/2/issue/{0}/worklog?adjustEstimate=leave", issueIdOrKey);
                break;
            case AdjustEstimate.Manual:
                uri = string.Format("rest/api/2/issue/{0}/worklog?adjustEstimate=manual&reduceBy={1}", issueIdOrKey, value);
                break;
            case AdjustEstimate.Auto:
                uri = string.Format("rest/api/2/issue/{0}/worklog?adjustEstimate=auto", issueIdOrKey);
                break;
            }

            JsonTrace.WriteRequest(this, worklog);
            using (HttpResponseMessage response = await this.client.PostAsJsonAsync<Worklog>(uri, worklog))
            {
                response.EnsureSuccess();
            }
        }

        /// <summary>
        /// Returns a specific worklog.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="worklogId">Id of the worklog.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Worklog> GetIssueWorklogAsync(string issueIdOrKey, string worklogId)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (string.IsNullOrEmpty(worklogId))
            {
                throw new ArgumentException("The worklogId is null or empty.");
            }

            Worklog res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/issue/{0}/worklog/{1}", issueIdOrKey, worklogId)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<Worklog>();
            }
            return res;
        }

        /// <summary>
        /// Updates an existing worklog entry.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="worklog">Worklog class to update.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Worklog> UpdateIssueWorklogAsync(string issueIdOrKey, Worklog worklog)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (worklog == null)
            {
                throw new ArgumentNullException("worklog");
            }

            // must be set to 0; so sav
            int timeSpentSeconds = worklog.TimeSpentSeconds;
            worklog.TimeSpentSeconds = 0;

            Worklog res = null;
            JsonTrace.WriteRequest(this, worklog);
            using (HttpResponseMessage response = await this.client.PutAsJsonAsync<Worklog>(string.Format("rest/api/2/issue/{0}/worklog/{1}", issueIdOrKey, worklog.Id), worklog))
            {
                worklog.TimeSpentSeconds = timeSpentSeconds;
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<Worklog>();
            }
            return res;
        }

        /// <summary>
        /// Deletes an existing worklog entry.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="worklogId">Id of the worklog to delete.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task DeleteIssueWorklogAsync(string issueIdOrKey, string worklogId)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (string.IsNullOrEmpty(worklogId))
            {
                throw new ArgumentException("The worklogId is null or empty.");
            }

            using (HttpResponseMessage response = await this.client.DeleteAsync(string.Format("rest/api/2/issue/{0}/worklog/{1}", issueIdOrKey, worklogId)))
            {
                response.EnsureSuccess();
            }
        }

        #endregion

        #region Attachments

        /// <summary>
        /// Returns the meta informations for an attachments, specifically if they are enabled and the maximum upload size allowed.
        /// </summary>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<AttachmentMeta> GetAttachmentMetaAsync()
        {
            AttachmentMeta res = null;
            using (HttpResponseMessage response = await this.client.GetAsync("rest/api/2/attachment/meta"))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<AttachmentMeta>();
            }
            return res;
        }

        /// <summary>
        /// Returns the meta-data for an attachment, including the URI of the actual attached file.
        /// </summary>
        /// <param name="attachmentId">The id of the attachment to get.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Attachment> GetAttachmentAsync(string attachmentId)
        {
            if (string.IsNullOrEmpty(attachmentId))
            {
                throw new ArgumentException("The attachmentId is null or empty.");
            }

            Attachment res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/attachment/{0}", attachmentId)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<Attachment>();
            }
            return res;
        }

        /// <summary>
        /// Remove an attachment from an issue.
        /// </summary>
        /// <param name="attachmentId">The id of the attachment to delete.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task DeleteAttachmentAsync(string attachmentId)
        {
            if (string.IsNullOrEmpty(attachmentId))
            {
                throw new ArgumentException("The attachmentId is null or empty.");
            }

            using (HttpResponseMessage response = await this.client.DeleteAsync(string.Format("rest/api/2/attachment/{0}", attachmentId)))
            {
                response.EnsureSuccess();
            }
        }

        /// <summary>
        /// Add one or more attachments to an issue.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="files">List with attachments to add.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IEnumerable<Attachment>> AddAttachmentsAsync(string issueIdOrKey, IEnumerable<KeyValuePair<string, Stream>> files)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (files == null)
            {
                throw new ArgumentNullException("files");
            }

            MultipartFormDataContent req = new MultipartFormDataContent();
            req.Headers.Add("X-Atlassian-Token", "nocheck");
            foreach (KeyValuePair<string, Stream> file in files)
            {
                req.Add(new StreamContent(file.Value), "file", file.Key);
            }
            IEnumerable<Attachment> res = null;
            using (HttpResponseMessage response = await this.client.PostAsync(string.Format("rest/api/2/issue/{0}/attachments", issueIdOrKey), req))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<IEnumerable<Attachment>>();
            }
            return res;
        }

        /// <summary>
        /// Get the date stream of an attachment.
        /// </summary>
        /// <param name="attachmentUrl">Url of the attachment.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Stream> GetAttachmentStreamAsync(Uri attachmentUrl)
        {
            if (attachmentUrl == null)
            {
                throw new ArgumentNullException("attachmentUrl");
            }
            //Stream stream = null;
            //using (HttpResponseMessage response = await this.client.GetAsync(attachmentUrl))
            //{
            //    response.EnsureSuccess();
            //    stream = await response.Content.ReadAsStreamAsync();
            //}
            //return stream;

            HttpResponseMessage response = await this.client.GetAsync(attachmentUrl);
            response.EnsureSuccess();
            return await response.Content.ReadAsStreamAsync();
        }

        #endregion

        #region Link

        /// <summary>
        /// Creates or updates a remote issue link. If a globalId is provided and a remote issue link exists with that globalId, the remote issue link is updated. Otherwise, the remote issue link is created.
        /// </summary>
        /// <param name="issueLink">IssueLink class to create.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task CreateIssueLinkAsync(IssueLink issueLink)
        {
            if (issueLink == null)
            {
                throw new ArgumentNullException("issueLink");
            }

            JsonTrace.WriteRequest(this, issueLink);
            using (HttpResponseMessage response = await this.client.PostAsJsonAsync<IssueLink>("rest/api/2/issueLink", issueLink))
            {
                response.EnsureSuccess();
            }
        }

        /// <summary>
        /// Get the remote issue link with the given id on the issue.
        /// </summary>
        /// <param name="issueLinkId">Id of the link.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IssueLink> GetIssueLinkAsync(string issueLinkId)
        {
            if (string.IsNullOrEmpty(issueLinkId))
            {
                throw new ArgumentException("The issueLinkId is null or empty.");
            }

            IssueLink res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/issueLink/{0}", issueLinkId)))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<IssueLink>();
            }
            return res;
        }

        /// <summary>
        /// Delete the remote issue link with the given global id on the issue.
        /// </summary>
        /// <param name="linkId">Id of the link to delete.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task DeleteIssueLinkAsync(string linkId)
        {
            if (string.IsNullOrEmpty(linkId))
            {
                throw new ArgumentException("The linkId is null or empty.");
            }

            using (HttpResponseMessage response = await this.client.DeleteAsync(string.Format("rest/api/2/issueLink/{0}", linkId)))
            {
                response.EnsureSuccess();
            }
        }

        #endregion

        #region Issues

        /// <summary>
        /// Creates an issue or a sub-task.
        /// </summary>
        /// <param name="issue">Issue class to create.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Issue> CreateIssueAsync(Issue issue)
        {
            if (issue == null)
            {
                throw new ArgumentNullException("issue");
            }

            Issue res = null;
            //issue.SerializeMode = SerializeMode.Create; // set for trace
            //JsonTrace.WriteRequest(this, issue);
            issue.SerializeMode = SerializeMode.Create; // set for create
            //using (HttpResponseMessage response = await this.client.PostAsJsonAsync("rest/api/2/issue", issue))
            using (HttpResponseMessage response = await this.client.PostAsync<Issue>("rest/api/2/issue", issue, this.jsonMediaTypeFormatter))
            {
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<Issue>();
            }
            issue.ResetAllChanged();
            res.UpdateCustomFields(await GetCachedFieldsAsync());
            return res;
        }

        /// <summary>
        /// Returns a full representation of the issue for the given issue key. 
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="fields">Fields which should be filled.</param>
        /// <param name="expand">Objects which should be expanded.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<Issue> GetIssueAsync(string issueIdOrKey, string fields = null, string expand = null)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            
            Issue res = null;
            using (HttpResponseMessage response = await this.client.GetAsync(string.Format("rest/api/2/issue/{0}{1}{2}", issueIdOrKey, string.IsNullOrEmpty(fields) ? "" : "?fields=" + fields,  string.IsNullOrEmpty(expand) ? "" : (string.IsNullOrEmpty(fields) ? "?expand=" : "&expand=") + expand)))
            {
                // return null if issue is not found
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                response.EnsureSuccess();
                res = await response.Content.ReadAsAsync<Issue>(this.jsonFormatters);
            }
            res.UpdateCustomFields(await GetCachedFieldsAsync());
            return res;
        }

        /// <summary>
        /// Edits an issue.
        /// </summary>
        /// <param name="issue">Issue class to update.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task UpdateIssueAsync(Issue issue)
        {
            if (issue == null)
            {
                throw new ArgumentNullException("issue");
            }
            issue.UpdateCustomFields(await GetCachedFieldsAsync());
            //issue.SerializeMode = SerializeMode.Update; // set for trace
            //JsonTrace.WriteRequest(this, issue);
            issue.SerializeMode = SerializeMode.Update; // set for update
//            using (HttpResponseMessage response = await this.client.PutAsJsonAsync(string.Format("rest/api/2/issue/{0}", issue.Key), issue))
            using (HttpResponseMessage response = await this.client.PutAsync($"rest/api/2/issue/{issue.Key}", issue, this.jsonMediaTypeFormatter))
            {
                response.EnsureSuccess();
            }
            issue.ResetAllChanged();
        }

        /// <summary>
        /// Delete an issue. If the issue has subtasks you must set the parameter deleteSubtasks=true to delete the issue. You cannot delete an issue without its subtasks also being deleted.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="deleteSubtasks">A String of true or false indicating that any subtasks should also be deleted. If the issue has no subtasks this parameter is ignored. If the issue has subtasks and this parameter is missing or false, then the issue will not be deleted and an error will be returned.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task DeleteIssueAsync(string issueIdOrKey, bool deleteSubtasks = false)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }

            using (HttpResponseMessage response = await this.client.DeleteAsync(string.Format("rest/api/2/issue/{0}?deleteSubtasks={1}", issueIdOrKey, deleteSubtasks ? "true" : "false" )))
            {
                response.EnsureSuccess();
            }
        }

        /// <summary>
        /// Assigns an issue to a user. 
        /// You can use this resource to assign issues when the user submitting the request has the assign permission but not the edit issue permission. 
        /// If the user is <see cref="User.AutomaticAssignee">User.AutomaticAssignee</see> automatic assignee is used. <see cref="User.EmptyAssignee">User.EmptyAssignee</see> will remove the assignee.
        /// </summary>
        /// <param name="issueIdOrKey">Id or key of the issue.</param>
        /// <param name="userName">User name to assign issue to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task AssignIssueAsync(string issueIdOrKey, string userName)
        {
            if (string.IsNullOrEmpty(issueIdOrKey))
            {
                throw new ArgumentException("The issueIdOrKey is null or empty.");
            }
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("The userName is null or empty.");
            }

            AssignPutRequest req = new AssignPutRequest() { Name = userName };
            using (HttpResponseMessage response = await this.client.PutAsJsonAsync(string.Format("rest/api/2/issue/{0}/assignee", issueIdOrKey), req))
            {
                response.EnsureSuccess();
            }
        }

        #endregion

        #region Search

        /// <summary>
        /// Performs a issue search using JQL.
        /// </summary>
        /// <param name="jql">JQL statement to search.</param>
        /// <param name="startAt">Start index.</param>
        /// <param name="maxResults">Maximum number of results.</param>
        /// <param name="fields">Fields to fill.</param>
        /// <param name="expand">Objects to expand.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IEnumerable<Issue>> GetIssuesFromJqlAsync(string jql, int startAt = 0, int maxResults = 500, string fields = null, string expand = null)
        {
            if (string.IsNullOrEmpty(jql))
            {
                throw new ArgumentException("The jql is null or empty.");
            }

            SearchRequest req = new SearchRequest() { Jql = jql, StartAt = startAt, MaxResults = maxResults, Fields = fields, Expand = expand };
            SearchResult res = null;
            JsonTrace.WriteRequest(this, req);
            using (HttpResponseMessage response = await this.client.PostAsJsonAsync("rest/api/2/search", req))
            {
                // return null if no issue found
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                //var x = response.StatusCode;
                if (!response.IsSuccessStatusCode)
                {
                    Trace.TraceError($"GetIssuesFromJqlAsync({jql}) returns {response.StatusCode.ToString()}");
                    return null;
                }
                res = await response.Content.ReadAsAsync<SearchResult>(this.jsonFormatters);
            }
            IEnumerable<Field> fieldInfo = await GetCachedFieldsAsync();
            foreach (Issue issue in res.Issues)
            {
                issue.UpdateCustomFields(fieldInfo);
            }
            return res.Issues;
        }

        #endregion

        #region Linq

        private IEnumerable<Field> fields;
        internal async Task<IEnumerable<Field>> GetCachedFieldsAsync()
        {
            if (this.fields == null)
            {
                this.fields = await GetFieldsAsync();
            }
            return this.fields;
        }


        internal bool   jqlTest = false;
        internal string jqlQuery = "";
        internal int?   jqlStartAt = null;
        internal int?   jqlMaxResults = null;
        internal IEnumerable<Issue> GetIssuesFromJql(string jql, int? startAt, int? maxResults)
        {
            if (string.IsNullOrEmpty(jql))
            {
                throw new ArgumentException("The jql is null or empty.");
            } 

            // for testing only
            if (this.jqlTest)
            {
                this.jqlQuery = jql;
                this.jqlStartAt = startAt;
                this.jqlMaxResults = maxResults;
                return new List<Issue>();
            }

            return GetIssuesFromJqlAsync(jql, startAt.GetValueOrDefault(0), maxResults.GetValueOrDefault(500)).Result;
        }

        /// <summary>
        /// Performs a issue search using LINQ
        /// </summary>
        /// <remarks>
        /// Methods: OrderBy, OrderByDescending, ThenBy, ThenByDescending, Skip, Take
        /// 
        /// </remarks>
        /// <example>
        /// var issues = (from issue in jira.Issues where issue.Priority == "Major" || issue.Priority == "Minor" select issue OrderBy issue.Version).Skip(5).Take(19);
        /// </example>
        public IOrderedQueryable<Issue> Issues
        {
            get { return new JiraQueryable<Issue>(this.provider); }
        }

        #endregion
    }
}

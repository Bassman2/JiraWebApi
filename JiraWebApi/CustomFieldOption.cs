using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of JIRA options for fixed value custom fields. 
    /// </summary>
    public sealed class CustomFieldOption
    {
        /// <summary>
        /// Initializes a new instance of the CustomFieldOption class.
        /// </summary>
        private CustomFieldOption()
        { }

        /// <summary>
        /// Url of the JIRA REST item.
        /// </summary>
        [JsonProperty("self")]
        public Uri Self { get; private set; }

        /// <summary>
        /// Value of the custom field option.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; private set; } 
    
        /// <summary>
        /// Id of the custom field option.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; private set; }

        /// <summary>
        /// Child custom field option.
        /// </summary>
        [JsonProperty("child")]
        public CustomFieldOption Child { get; private set; }
    }
}


﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of a JIRA issue status. 
    /// </summary>
    public sealed class Status : ComparableElement
    {
        /// <summary>
        /// Initializes a new instance of the Status class.
        /// </summary>
        internal Status()
        { }

        /// <summary>
        /// Description of the JIRA status.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; private set; }

        /// <summary>
        /// Url of the issue status icon.
        /// </summary>
        [JsonProperty("iconUrl")]
        public Uri IconUrl { get; private set; }

        /// <summary>
        /// Returns a string that represents the issue status.
        /// </summary>
        /// <returns>A string that represents the issue status.</returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}

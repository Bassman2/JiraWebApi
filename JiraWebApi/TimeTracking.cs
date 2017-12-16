using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of a JIRA issue time tracking.
    /// </summary>
    public sealed class TimeTracking
    {
        /// <summary>
        /// Initializes a new instance of the TimeTracking class.
        /// </summary>
        public TimeTracking()
        { }

        /// <summary>
        /// Time original estimate.
        /// </summary>
        [JsonProperty("originalEstimate")]
        public string OriginalEstimate { get; set; }

        /// <summary>
        /// Time remaining estimate.
        /// </summary>
        [JsonProperty("remainingEstimate")]
        public string RemainingEstimate { get; set; }

        /// <summary>
        /// Time spent.
        /// </summary>
        [JsonProperty("timeSpent")]
        public string TimeSpent { get; set; }

        /// <summary>
        /// Time original estimate in seconds.
        /// </summary>
        [JsonProperty("originalEstimateSeconds")]
        public long OriginalEstimateSeconds { get; private set; }

        /// <summary>
        /// Time remaining estimate in seconds.
        /// </summary>
        [JsonProperty("remainingEstimateSeconds")]
        public long RemainingEstimateSeconds { get; private set; }

        /// <summary>
        /// Time spent in seconds.
        /// </summary>
        [JsonProperty("timeSpentSeconds")]
        public long TimeSpentSeconds { get; private set; }
    }
}

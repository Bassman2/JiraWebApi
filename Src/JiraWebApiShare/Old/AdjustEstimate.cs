using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Adjust estimate state to add a new worklog entry to an issue.
    /// </summary>
    /// <remarks>Used by AddIssueWorklogsAsync.
    /// </remarks>
    /// <see cref="JiraWebApi.Jira.AddIssueWorklogAsync"/>
    public enum AdjustEstimate
    {
        /// <summary>
        /// Sets the estimate to a specific value.
        /// </summary>
        New,

        /// <summary>
        /// Leaves the estimate as is.
        /// </summary>
        Leave,

        /// <summary>
        /// Specify a specific amount to increase remaining estimate by.
        /// </summary>
        Manual,

        /// <summary>
        /// Default option. Will automatically adjust the value based on the new timeSpent specified on the worklog.
        /// </summary>
        Auto
    }
}

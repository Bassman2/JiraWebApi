using JiraWebApi.Internal;
using JiraWebApi.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{

    /// <summary>
    /// Static class to represent JQL sprint functions.
    /// </summary>
    public static class Sprint
    {
        /// <summary>
        /// Static method to represent the JQL openSprints() function
        /// </summary>
        /// <returns>No result.</returns>
        /// <remarks>For Linq use only.</remarks>
        [JqlFunction("openSprints")]
        public static string[] OpenSprints()
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Static method to represent the JQL closedSprints() function
        /// </summary>
        /// <returns>No result.</returns>
        /// <remarks>For Linq use only.</remarks>
        [JqlFunction("closedSprints")]
        public static string[] ClosedSprints()
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }
    }
}

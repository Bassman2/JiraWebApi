using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of a JIRA permission.
    /// </summary>
    public sealed class Permission
    {
        /// <summary>
        /// Initializes a new instance of the Permission class.
        /// </summary>
        public Permission()
        { }

        /// <summary>
        /// Id of the permission.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
 
        /// <summary>
        /// Key of the permission.
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }

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
            Permission permission = obj as Permission;
            if ((object)permission == null)
            {
                return false;
            }
            return this.Id == permission.Id;
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
        /// <param name="permission1">The first permission to compare, or null.</param>
        /// <param name="permission2">The second permission to compare, or null.</param>
        /// <returns>true if the id of the first permission is equal to the id of the second permission; otherwise, false.</returns>
        public static bool operator ==(Permission permission1, Permission permission2)
        {
            if (ReferenceEquals(permission1, permission2))
            {
                return true;
            }
            if (((object)permission1 == null) || ((object)permission2 == null))
            {
                return false;
            }
            return permission1.Equals(permission2);
        }

        /// <summary>
        /// Compare not equal operator.
        /// </summary>
        /// <param name="permission1">The first permission to compare, or null.</param>
        /// <param name="permission2">The second permission to compare, or null.</param>
        /// <returns>true if the id of the first permission is different from the id of the second permission; otherwise, false.</returns>
        public static bool operator !=(Permission permission1, Permission permission2)
        {
            return !(permission1 == permission2);
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Base class for JIRA items with an Id.
    /// </summary>
    public abstract class Element
    {
        /// <summary>
        /// Url of the JIRA REST item.
        /// </summary>
        [JsonProperty("self")]
        public string Self { get; set; }

        /// <summary>
        /// Id of the JIRA item.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

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
            Element element = obj as Element;
            if ((object)element == null)
            {
                return false;
            }
            return this.Id == element.Id;
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
        /// <param name="element1">The first element to compare, or null.</param>
        /// <param name="element2">The second element to compare, or null.</param>
        /// <returns>true if the id of the first element is equal to the id of the second element; otherwise, false.</returns>
        public static bool operator ==(Element element1, Element element2)
        {
            if (ReferenceEquals(element1, element2))
            {
                return true;
            }
            if (((object)element1 == null) || ((object)element2 == null))
            {
                return false;
            }
            return element1.Equals(element2);
        }

        /// <summary>
        ///  Compare not equal operator.
        /// </summary>
        /// <param name="element1">The first element to compare, or null.</param>
        /// <param name="element2">The second element to compare, or null.</param>
        /// <returns>true if the id of the first element is different from the id of the second element; otherwise, false.</returns>
        public static bool operator !=(Element element1, Element element2)
        {
            return !(element1 == element2);
        }
    }
}

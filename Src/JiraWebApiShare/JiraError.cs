using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace JiraWebApi
{
    // {"errorMessages":[],"errors":{"projectKey":"A project with that project key already exists."}}
    // {"errorMessages":["Field 'priority' is required"],"errors":{}}
    // {"errorMessages":[],"errors":{"title":"'title' is required."}}

    /// <summary>
    /// JIRA error messages.
    /// </summary>
    [Serializable]
    public sealed class JiraError : ISerializable
    {
        internal JiraError()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo to populate with data.</param>
        /// <param name="context">The destination (see System.Runtime.Serialization.StreamingContext) for this serialization.</param>
        private JiraError(SerializationInfo info, StreamingContext context)
        {
            //foreach (SerializationEntry entry in info)
            //{
            //    switch (entry.Name)
            //    {
            //    case "errorMessages":
            //        this.ErrorMessages = ((JArray)entry.Value).Select(t => (string)t).ToList();
            //        break;
            //    case "errors":
            //        this.Errors = ((IEnumerable<KeyValuePair<string, JToken>>)((JObject)entry.Value)).ToDictionary(e => e.Key, e => (string)e.Value);
            //        break;
            //    }
            //}
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo to populate with data.</param>
        /// <param name="context">The destination (see System.Runtime.Serialization.StreamingContext) for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotSupportedException();
        }
                
        /// <summary>
        /// Error messages from the JIRA REST request.
        /// </summary>
        public IEnumerable<string> ErrorMessages { get; internal set; }

        /// <summary>
        /// Errors from the JIRA REST request.
        /// </summary>
        public IDictionary<string, string> Errors { get; internal set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            //var x = this.ErrorMessages != null && this.ErrorMessages.Count() > 0 ? this.ErrorMessages.Aggregate((a, b) => string.Format("{0}\r\n{1}", a, b)) : "";
            //var y = this.Errors != null && this.Errors.Count > 0 ? this.Errors.Select(e => string.Format("{0} : {1}", e.Key, e.Value)).Aggregate((a, b) => string.Format("{0}\r\n{1}", a, b)) : "";

            return string.Format("ErrorMessages: {0}\r\nErrors: {1}",
                this.ErrorMessages != null && this.ErrorMessages.Count() > 0 ? this.ErrorMessages.Aggregate((a, b) => string.Format("{0}\r\n{1}", a, b)) : "null",
                this.Errors != null && this.Errors.Count > 0 ? this.Errors.Select(e => string.Format("{0} : {1}", e.Key, e.Value)).Aggregate((a, b) => string.Format("{0}\r\n{1}", a, b)) : "null");
        }
    }
}

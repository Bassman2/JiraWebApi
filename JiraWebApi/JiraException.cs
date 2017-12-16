using JiraWebApi.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Represents errors that occur during JIRA REST requests.
    /// </summary>
    public class JiraException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the JiraException class.
        /// </summary>
        public JiraException()
            : base()
        { }

        
        /// <summary>
        /// Initializes a new instance of the JiraException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public JiraException(string message) 
            : base()
        {
            this.message = message;
        }

        internal JiraException(HttpResponseMessage response)
        {
            this.StatusCode = response.StatusCode;
            this.HttpError = response.ReasonPhrase;
            if (response.Content.Headers.ContentType.MediaType == "text/html")
            {
                this.HtmlError = response.Content.ReadAsStringAsync().Result;
            }
            else if (response.Content.Headers.ContentType.MediaType == "application/json")
            {
                this.JiraError = response.Content.ReadAsAsync<JiraError>().Result;
            }
            this.message = this.ToString();
        }

        internal JiraException(JiraError jiraError)
        {
            this.StatusCode = HttpStatusCode.OK;
            this.HttpError =  "";
            this.HtmlError = null;
            this.JiraError = jiraError;
            this.message = this.ToString();
        }

        internal JiraException(IEnumerable<string> errorMessages)
        {
            this.StatusCode = HttpStatusCode.OK;
            this.HttpError = "";
            this.HtmlError = null;
            this.JiraError = new JiraError() { ErrorMessages = errorMessages };
            this.message = this.ToString();
        }

        internal JiraException(IDictionary<string, string> errors)
        {
            this.StatusCode = HttpStatusCode.OK;
            this.HttpError = "";
            this.HtmlError = null;
            this.JiraError = new JiraError() { Errors = errors };
            this.message = this.ToString();
        }

        //public IEnumerable<string> ErrorMessages { get; internal set; }

        // <summary>
        // Errors from the JIRA REST request.
        // </summary>
        //public IDictionary<string, string> Errors { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the JiraException class with serialized data.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        /// <exception cref="System.ArgumentNullException">The info parameter is null.</exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException">The class name is null or System.Exception.HResult is zero (0).</exception> 
        [SecuritySafeCritical]
        protected JiraException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the JiraException class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public JiraException(string message, Exception innerException) 
            : base(message, innerException)
        { }

        private string message;

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        public override string Message { get { return this.message; } }

        /// <summary>
        /// Gets the status code of the HTTP response.
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// Gets the reason phrase which typically is sent by servers together with the status code. 
        /// </summary>
        public string HttpError { get; private set; }

        /// <summary>
        /// Gets the html error description delivered by the web server. 
        /// </summary>
        public String HtmlError { get; private set; }

        /// <summary>
        /// Gets the error description delivered by the JIRA server. 
        /// </summary>
        public JiraError JiraError { get; private set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("\r\nStatusCode: {0}\r\nHttpError: {1}\r\n{2}", 
                this.StatusCode.ToString(), 
                this.HttpError, 
                this.JiraError != null ? this.JiraError.ToString() : "");
        }
    }
}

using JiraWebApi.Internal;
using JiraWebApi.Linq;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of a JIRA user. 
    /// </summary>
    public sealed class User
    {
        /// <summary>
        /// Initializes a new instance of the User class.
        /// </summary>
        internal User()
        { }

        /// <summary>
        /// Automatic assignee for AssignIssueAsync.
        /// </summary>
        public static User AutomaticAssignee
        {
            get { return new User() { Name = "-1" }; }
        }

        /// <summary>
        /// Empty assignee for AssignIssueAsync.
        /// </summary>
        public static User EmptyAssignee
        {
            get { return new User() { Name = null }; }
        }

        /// <summary>
        /// Support of the JQL 'currentUser()' operator in LINQ.
        /// </summary>
        /// <returns>Returns always null.</returns>
        /// <remarks>For Linq use only.</remarks>
        /// <remarks>NotSupportedException will not work here because the Linq visitor compiles this method.</remarks>
        [JqlFunction("currentUser")]
        public static User CurrentUser()
        {
            throw new NotSupportedException("Operator only defined to allow Linq comparison."); 
        }

        /// <summary>
        /// Support of the JQL 'membersOf()' operator in LINQ.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns>No result.</returns>
        /// <remarks>For Linq use only.</remarks>
        /// <remarks>NotSupportedException will not work here because the Linq visitor compiles this method.</remarks>
        [JqlFunction("membersOf")]
        public static User[] MembersOf(string groupName)
        {
            throw new NotSupportedException("Operator only defined to allow Linq comparison."); 
        }

        /// <summary>
        /// Url of the REST item.
        /// </summary>
        [JsonPropertyName("self")]
        public Uri Self { get; private set; }

        /// <summary>
        /// Name of the JIRA user.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; internal set; }

        /// <summary>
        /// E-mail address of the JIRA user.
        /// </summary>
        [JsonPropertyName("emailAddress")]
        public string EmailAddress { get; private set; }

        /// <summary>
        /// Avatar URLs of the JIRA user.
        /// </summary>
        [JsonPropertyName("avatarUrls")]
        public AvatarUrls AvatarUrls { get; private set; }

        /// <summary>
        /// Display name of the JIRA user.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; private set; }

        /// <summary>
        /// Signals if the JIRA user is actve.
        /// </summary>
        [JsonPropertyName("active")]
        public bool IsActive { get; private set; }

        /// <summary>
        /// Time zone of the JIRA user.
        /// </summary>
        [JsonPropertyName("timeZone")]
        public string TimeZone { get; private set; }

        /// <summary>
        /// Returns a string that represents the user.
        /// </summary>
        /// <returns>A string that represents the user.</returns>
        public override string ToString()
        {
            return this.Name;
        }

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
            User user = obj as User;
            if ((object)user == null)
            {
                return false;
            }
            return this.Name == user.Name;
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
        /// <param name="user1">The first user to compare, or null.</param>
        /// <param name="user2">The second user to compare, or null.</param>
        /// <returns>true if the first user is equal to the second user; otherwise, false.</returns>
        public static bool operator ==(User user1, User user2)
        {
            if (ReferenceEquals(user1, user2))
            {
                return true;
            }
            if (((object)user1 == null) || ((object)user2 == null))
            {
                return false;
            }
            return user1.Equals(user2);
        }

        /// <summary>
        /// Compare not equal operator.
        /// </summary>
        /// <param name="user1">The first user to compare, or null.</param>
        /// <param name="user2">The second user to compare, or null.</param>
        /// <returns>true if the first user is different from the second user; otherwise, false.</returns>
        public static bool operator !=(User user1, User user2)
        {
            return !(user1 == user2);
        }

        /// <summary>
        /// Compare equal operator.
        /// </summary>
        /// <param name="user">The user to compare, or null.</param>
        /// <param name="name">The user name to compare, or null.</param>
        /// <returns>true if the user is equal to the user name; otherwise, false.</returns>
        public static bool operator ==(User user, string name)
        {
            return user.Name == name;
        }

        /// <summary>
        /// Compare not equal operator
        /// </summary>
        /// <param name="user">The user to compare, or null.</param>
        /// <param name="name">The user name to compare, or null.</param>
        /// <returns>true if the user is different from the user name; otherwise, false.</returns>
        public static bool operator !=(User user, string name)
        {
            return user.Name != name;
        }

        /// <summary>
        /// Support of the JQL 'is null' operator in LINQ.
        /// </summary>
        /// <returns>Not used.</returns>
        public bool IsNull()
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'is not null' operator in LINQ.
        /// </summary>
        /// <returns>Not used.</returns>
        public bool IsNotNull()
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'is empty' operator in LINQ.
        /// </summary>
        /// <returns>Not used.</returns>
        public bool IsEmpty()
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'is not empty' operator in LINQ.
        /// </summary>
        /// <returns>Not used.</returns>
        public bool IsNotEmpty()
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL In operator in LINQ.
        /// </summary>
        /// <param name="values">List to compare with. This can be a combination of string and User parameters.</param>
        /// <returns>Not used.</returns>
        public bool In(params object[] values)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }
        
        /// <summary>
        /// Support of the JQL Not In operator in LINQ.
        /// </summary>
        /// <param name="values">List to compare with. This can be a combination of string and User parameters.</param>
        /// <returns>Not used.</returns>
        public bool NotIn(params object[] values)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'was' operator in LINQ.
        /// </summary>
        /// <param name="user">Item to compare with.</param>
        /// <returns>Not used.</returns>
        public bool Was(object user)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }
        
        /// <summary>
        /// Support of the JQL 'was not' operator in LINQ.
        /// </summary>
        /// <param name="user">Item to compare with.</param>
        /// <returns>Not used.</returns>
        public bool WasNot(object user)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'was in' operator in LINQ.
        /// </summary>
        /// <param name="values">List to compare with.</param>
        /// <returns>Not used.</returns>
        public bool WasIn(params object[] values)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'was not in' operator in LINQ.
        /// </summary>
        /// <param name="values">List to compare with.</param>
        /// <returns>Not used.</returns>
        public bool WasNotIn(params object[] values)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'changed' operator in LINQ.
        /// </summary>
        /// <returns>Not used.</returns>
        public bool Changed()
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }
    }
}

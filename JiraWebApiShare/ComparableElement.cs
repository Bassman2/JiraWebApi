using JiraWebApi.Internal;
using System;
using System.Text.Json.Serialization;

namespace JiraWebApi
{
    /// <summary>
    /// Base class for for compareable Jira items.
    /// </summary>
    public abstract class ComparableElement : Element
    {
        /// <summary>
        /// Name of the JIRA item.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

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
        /// <returns>true if the id of a is the same as the id of b; otherwise, false.</returns>
        public static bool operator ==(ComparableElement element1, ComparableElement element2)
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
        /// Compare not equal operator.
        /// </summary>
        /// <param name="element1">The first element to compare, or null.</param>
        /// <param name="element2">The second element to compare, or null.</param>
        /// <returns>true if the id of a is different from the id of b; otherwise, false.</returns>
        public static bool operator !=(ComparableElement element1, ComparableElement element2)
        {
            return !(element1 == element2);
        }

        /// <summary>
        /// Compare equal operator to allow LINQ compare.
        /// </summary>
        /// <param name="element">The element to compare, or null.</param>
        /// <param name="name">The name to compare, or null.</param>
        /// <returns>true if the name of element is the same name; otherwise, false.</returns>
        public static bool operator ==(ComparableElement element, string name)
        {
            return element.Name == name;
        }

        /// <summary>
        /// Compare not equal operator to allow LINQ compare.
        /// </summary>
        /// <param name="element">The element to compare, or null.</param>
        /// <param name="name">The name to compare, or null</param>
        /// <returns>true if the name of element is different from the name; otherwise, false.</returns>
        public static bool operator !=(ComparableElement element, string name)
        {
            return element.Name != name;
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
        /// Support of the JQL is empty operator in LINQ.
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
        /// Support of the JQL 'in' operator in LINQ.
        /// </summary>
        /// <param name="values">List to compare with.</param>
        /// <returns>Not used.</returns>
        public bool In(params string[] values)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'in' operator in LINQ.
        /// </summary>
        /// <param name="values">List to compare with.</param>
        /// <returns>Not used.</returns>
        public bool In(params ComparableElement[] values)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'not in' operator in LINQ.
        /// </summary>
        /// <param name="values">List to compare with.</param>
        /// <returns>Not used.</returns>
        public bool NotIn(params string[] values)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'not in' operator in LINQ.
        /// </summary>
        /// <param name="values">List to compare with.</param>
        /// <returns>Not used.</returns>
        public bool NotIn(params ComparableElement[] values)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'was' operator in LINQ.
        /// </summary>
        /// <param name="value">Value to compare with.</param>
        /// <returns>Not used.</returns>
        public bool Was(string value)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'was not' operator in LINQ.
        /// </summary>
        /// <param name="value">Value to compare with.</param>
        /// <returns>Not used.</returns>
        public bool WasNot(string value)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'was' operator in LINQ.
        /// </summary>
        /// <param name="value">Value to compare with.</param>
        /// <returns>Not used.</returns>
        public bool Was(ComparableElement value)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'was not' operator in LINQ.
        /// </summary>
        /// <param name="value">Value to compare with.</param>
        /// <returns>Not used.</returns>
        public bool WasNot(ComparableElement value)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'was in' operator in LINQ.
        /// </summary>
        /// <param name="values">Value to compare with.</param>
        /// <returns>Not used.</returns>
        public bool WasIn(params string[] values)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'was in' operator in LINQ.
        /// </summary>
        /// <param name="values">Value to compare with.</param>
        /// <returns>Not used.</returns>
        public bool WasIn(params ComparableElement[] values)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'was not in' operator in LINQ.
        /// </summary>
        /// <param name="values">Value to compare with.</param>
        /// <returns>Not used.</returns>
        public bool WasNotIn(params string[] values)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL 'was not in' operator in LINQ.
        /// </summary>
        /// <param name="values">Values to compare with.</param>
        /// <returns>Not used.</returns>
        public bool WasNotIn(params ComparableElement[] values)
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

        /// <summary>
        /// Returns a string that represents the element.
        /// </summary>
        /// <returns>A string that represents the element.</returns>
        /// <remarks>Do not overwrite! Needed for Linq.</remarks>
        public override string ToString()
        {
            return this.Name;
        }
    }
}

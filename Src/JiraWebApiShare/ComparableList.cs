using JiraWebApi.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Class for compareable Jira lists. 
    /// </summary>
    /// <typeparam name="TSource">The type of elements in the list.</typeparam>
    public class ComparableList<TSource> : IEnumerable<TSource>
    {
        private List<TSource> list;

        /// <summary>
        /// Initializes a new instance of the ComparableList class.
        /// </summary>
        /// <param name="list">IEnumerable to initialize ComparableList</param>
        internal ComparableList(IEnumerable<TSource> list)
        {
            this.list = list == null ? new List<TSource>() : new List<TSource>(list);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A System.Collections.Generic.IEnumerator&lt;T&gt; that can be used to iterate through the collection.</returns>
        public IEnumerator<TSource> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns> An System.Collections.IEnumerator object that can be used to iterate through the collection.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
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
        /// Compare equal operator to allow LINQ compare.
        /// </summary>
        /// <param name="items">The items to compare, or null.</param>
        /// <param name="name">The name to compare, or null.</param>
        /// <returns>true if the names of the items are equal to the name; otherwise, false.</returns>
        public static bool operator ==(ComparableList<TSource> items, string name)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Compare not equal operator to allow LINQ compare.
        /// </summary>
        /// <param name="items">The items to compare, or null.</param>
        /// <param name="name">The name to compare, or null.</param>
        /// <returns>true if the name of element is different from the name; otherwise, false.</returns>
        public static bool operator !=(ComparableList<TSource> items, string name)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Compare greater than operator to allow LINQ compare.
        /// </summary>
        /// <param name="items">The items to compare, or null.</param>
        /// <param name="name">The name to compare, or null.</param>
        /// <returns>true if the name of element is greater than the name in the JIRA sort order; otherwise, false.</returns>
        public static bool operator >(ComparableList<TSource> items, string name)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Compare less than operator to allow LINQ compare.
        /// </summary>
        /// <param name="items">The items to compare, or null.</param>
        /// <param name="name">The name to compare, or null.</param>
        /// <returns>true if the name of element is less than the name in the JIRA sort order; otherwise, false.</returns>
        public static bool operator <(ComparableList<TSource> items, string name)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Compare greater or equal operator to allow LINQ compare.
        /// </summary>
        /// <param name="items">The items to compare, or null.</param>
        /// <param name="name">The name to compare, or null.</param>
        /// <returns>true if the name of element is greater or equal than the name in the JIRA sort order; otherwise, false.</returns>
        public static bool operator >=(ComparableList<TSource> items, string name)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Compare less than or equal operator to allow LINQ compare.
        /// </summary>
        /// <param name="items">The items to compare, or null.</param>
        /// <param name="name">The name to compare, or null.</param>
        /// <returns>true if the name of element is less or equal than the name in the JIRA sort order; otherwise, false.</returns>
        public static bool operator <=(ComparableList<TSource> items, string name)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Compare equal operator to allow LINQ compare.
        /// </summary>
        /// <param name="items">The items to compare, or null.</param>
        /// <param name="item">The item to compare, or null.</param>
        /// <returns>true if the names of the items are equal to the name of the item ; otherwise, false.</returns>
        public static bool operator ==(ComparableList<TSource> items, TSource item)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Compare not equal operator to allow LINQ compare.
        /// </summary>
        /// <param name="items">The items to compare, or null.</param>
        /// <param name="item">The item to compare, or null.</param>
        /// <returns>true if the name of element is different from the name of the item ; otherwise, false.</returns>
        public static bool operator !=(ComparableList<TSource> items, TSource item)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Compare greater than operator to allow LINQ compare.
        /// </summary>
        /// <param name="items">The items to compare, or null.</param>
        /// <param name="item">The item to compare, or null.</param>
        /// <returns>true if the name of element is greater than the name of the item  in the JIRA sort order; otherwise, false.</returns>
        public static bool operator >(ComparableList<TSource> items, TSource item)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Compare less than operator to allow LINQ compare.
        /// </summary>
        /// <param name="items">The items to compare, or null.</param>
        /// <param name="item">The item to compare, or null.</param>
        /// <returns>true if the name of element is less than the name of the item  in the JIRA sort order; otherwise, false.</returns>
        public static bool operator <(ComparableList<TSource> items, TSource item)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Compare greater or equal operator to allow LINQ compare.
        /// </summary>
        /// <param name="items">The items to compare, or null.</param>
        /// <param name="item">The item to compare, or null.</param>
        /// <returns>true if the name of element is greater or equal than the name of the item  in the JIRA sort order; otherwise, false.</returns>
        public static bool operator >=(ComparableList<TSource> items, TSource item)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Compare less than or equal operator to allow LINQ compare.
        /// </summary>
        /// <param name="items">The items to compare, or null.</param>
        /// <param name="item">The item to compare, or null.</param>
        /// <returns>true if the name of element is less or equal than the name of the item in the JIRA sort order; otherwise, false.</returns>
        public static bool operator <=(ComparableList<TSource> items, TSource item)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
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
        /// <param name="values">List to compare with.</param>
        /// <returns>Not used.</returns>
        public bool In(params object[] values)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }

        /// <summary>
        /// Support of the JQL Not In operator in LINQ.
        /// </summary>
        /// <param name="values">List to compare with.</param>
        /// <returns>Not used.</returns>
        public bool NotIn(params object[] values)
        {
            throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
        }
    }
}

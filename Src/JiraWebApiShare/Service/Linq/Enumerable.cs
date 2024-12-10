//namespace JiraWebApi;

///// <summary>
///// Provides a set of static (Shared in Visual Basic) methods for querying objects that implement System.Collections.Generic.IEnumerable&lt;T&gt;.
///// </summary>
//public static class Enumerable
//{
//    /// <summary>
//    /// Creates a JiraWebApi.ToComparableList&lt;T&gt; from an System.Collections.Generic.IEnumerable&lt;T&gt;.
//    /// </summary>
//    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
//    /// <param name="source">The System.Collections.Generic.IEnumerable&lt;T&gt; to create a JiraWebApi.ToComparableList&lt;T&gt; from.</param>
//    /// <returns> A JiraWebApi.ToComparableList&lt;T&gt; that contains elements from the input sequence.</returns>
//    /// <exception cref="System.ArgumentNullException">source is null.</exception>
//    public static ComparableList<TSource> ToComparableList<TSource>(this IEnumerable<TSource> source)
//    {
//        if (source == null)
//        {
//            throw new ArgumentNullException("Source");
//        }
//        return new ComparableList<TSource>(source);
//    }
            
//    /// <summary>
//    /// Support of the JQL In operator in LINQ.
//    /// </summary>
//    /// <param name="source">ElementModel to compare with.</param>
//    /// <param name="values">List to compare with.</param>
//    /// <returns>Not used.</returns>
//    public static bool In(this string source, params object[] values)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Support of the JQL 'in' operator in LINQ.
//    /// </summary>
//    /// <param name="source">ElementModel to compare with.</param>
//    /// <param name="values">List to compare with.</param>
//    /// <returns>Not used.</returns>
//    public static bool In(this DateTime? source, params DateTime[] values)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Support of the JQL 'in' operator in LINQ.
//    /// </summary>
//    /// <param name="source">ElementModel to compare with.</param>
//    /// <param name="values">List to compare with.</param>
//    /// <returns>Not used.</returns>
//    public static bool In(this int source, params int[] values)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Support of the JQL Not In operator in LINQ.
//    /// </summary>
//    /// <param name="source">ElementModel to compare with.</param>
//    /// <param name="values">List to compare with.</param>
//    /// <returns>Not used.</returns>
    
//    public static bool NotIn(this string source, params object[] values)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Support of the JQL 'not in' operator in LINQ.
//    /// </summary>
//    /// <param name="source">ElementModel to compare with.</param>
//    /// <param name="values">List to compare with.</param>
//    /// <returns>Not used.</returns>
//    public static bool NotIn(this DateTime? source, params DateTime[] values)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Support of the JQL 'not in' operator in LINQ.
//    /// </summary>
//    /// <param name="source">ElementModel to compare with.</param>
//    /// <param name="values">List to compare with.</param>
//    /// <returns>Not used.</returns>
//    public static bool NotIn(this int source, params int[] values)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Support of the JQL 'was in' operator in LINQ.
//    /// </summary>
//    /// <param name="source">ElementModel to compare with.</param>
//    /// <param name="values">List to compare with.</param>
//    /// <returns>Not used.</returns>
//    public static bool WasIn(this string source, params object[] values)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Support of the JQL 'was in' operator in LINQ.
//    /// </summary>
//    /// <param name="source">ElementModel to compare with.</param>
//    /// <param name="values">List to compare with.</param>
//    /// <returns>Not used.</returns>
//    public static bool WasIn(this DateTime? source, params DateTime[] values)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }
    
//    /// <summary>
//    /// Support of the JQL 'was not in' operator in LINQ.
//    /// </summary>
//    /// <param name="source">ElementModel to compare with.</param>
//    /// <param name="values">List to compare with.</param>
//    /// <returns>Not used.</returns>
//    public static bool WasNotIn(this string source, params object[] values)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Support of the JQL 'was not in' operator in LINQ.
//    /// </summary>
//    /// <param name="source">ElementModel to compare with.</param>
//    /// <param name="values">List to compare with.</param>
//    /// <returns>Not used.</returns>
//    public static bool WasNotIn(this DateTime? source, params DateTime[] values)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Support of the JQL 'after' operator in LINQ.
//    /// </summary>
//    /// <param name="source">ElementModel to compare with.</param>
//    /// <param name="date">List to compare with.</param>
//    /// <returns>Not used.</returns>
//    /// <example><codeReference>Linq#after</codeReference></example> 
//    public static bool After(this bool source, DateTime date)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }
//    // <example><see cref="Linq#after"/></example> 
//    // 
//    // <code>
//    // var r = from i in jira.Issues where i.Resolution.Was("Open").After(new DateTime(2005, 10, 1)) select i;
//    // </code>
    

//    /// <summary>
//    /// Support of the JQL 'before' operator in LINQ.
//    /// </summary>
//    /// <param name="source">ElementModel to compare with.</param>
//    /// <param name="date">List to compare with.</param>
//    /// <returns>Not used.</returns>
//    /// <example><code>
//    /// var r = from i in jira.Issues where i.Resolution.Was("Open").Before(new DateTime(2005, 10, 1)) select i;
//    /// </code></example>
//    public static bool Before(this bool source, DateTime date)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Support of the JQL 'by' operator in LINQ.
//    /// </summary>
//    /// <param name="source">ElementModel to compare with.</param>
//    /// <param name="user">List to compare with.</param>
//    /// <returns>Not used.</returns>
//    /// <example><code>
//    /// var r = from i in jira.Issues where i.Resolution.Was("Open").By(user) select i;
//    /// </code></example>
//    public static bool By(this bool source, User user)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Support of the JQL 'by' operator in LINQ.
//    /// </summary>
//    /// <param name="source">ElementModel to compare with.</param>
//    /// <param name="userName">List to compare with.</param>
//    /// <returns>Not used.</returns>
//    public static bool By(this bool source, string userName)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Support of the JQL 'during' operator in LINQ.
//    /// </summary>
//    /// <param name="source">ElementModel to compare with.</param>
//    /// <param name="dateFrom">Date from to compare with.</param>
//    /// <param name="dateTo">Date to to compare with.</param>
//    /// <returns>Not used.</returns>
//    /// <example><code>
//    /// var r = from i in jira.Issues where i.Resolution.Was("Open").During(new DateTime(2005, 10, 1), new DateTime(2010, 10, 1)) select i;
//    /// </code></example>
//    public static bool During(this bool source, DateTime dateFrom, DateTime dateTo)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Support of the JQL 'on' operator in LINQ.
//    /// </summary>
//    /// <param name="source">ElementModel to compare with.</param>
//    /// <param name="date">Date to compare with.</param>
//    /// <returns>Not used.</returns>
//    /// <example><code>
//    /// var r = from i in jira.Issues where i.Resolution.Was("Open").On(new DateTime(2005, 10, 1)) select i;
//    /// </code></example>
//    public static bool On(this bool source, DateTime date)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Support of the JQL 'from' operator in LINQ.
//    /// </summary>
//    /// <param name="source">ElementModel to compare with.</param>
//    /// <param name="oldValue">Value to compare with.</param>
//    /// <returns>Not used.</returns>
//    /// <example><code>
//    /// var r = from i in jira.Issues where i.Resolution.Was("Open").On(new DateTime(2005, 10, 1)) select i;
//    /// </code></example>
//    public static bool From(this bool source, object oldValue)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Support of the JQL 'to' operator in LINQ.
//    /// </summary>
//    /// <param name="source">ElementModel to compare with.</param>
//    /// <param name="newValue">New value to compare with.</param>
//    /// <returns>Not used.</returns>
//    /// <example><code>
//    /// var r = from i in jira.Issues where i.Resolution.Changed().From("Open").To("Closed") select i;
//    /// </code></example>
//    public static bool To(this bool source, object newValue)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    #region ToCustomFieldValue

//    /// <summary>
//    /// Case string array to CustomFieldValue.
//    /// </summary>
//    /// <param name="value">Value to cast from.</param>
//    /// <returns>CustomFieldValue to cast to.</returns>
//    public static CustomFieldValue ToCustomFieldValue(this IEnumerable<string> value)
//    {
//        return new CustomFieldValue(value);
//    }

//    /// <summary>
//    /// Case Group array to CustomFieldValue.
//    /// </summary>
//    /// <param name="value">Value to cast from.</param>
//    /// <returns>CustomFieldValue to cast to.</returns>
//    public static CustomFieldValue ToCustomFieldValue(this IEnumerable<Group> value)
//    {
//        return new CustomFieldValue(value);
//    }

//    /// <summary>
//    /// Case User array to CustomFieldValue.
//    /// </summary>
//    /// <param name="value">Value to cast from.</param>
//    /// <returns>CustomFieldValue to cast to.</returns>
//    public static CustomFieldValue ToCustomFieldValue(this IEnumerable<User> value)
//    {
//        return new CustomFieldValue(value);
//    }

//    /// <summary>
//    /// Case Version array to CustomFieldValue.
//    /// </summary>
//    /// <param name="value">Value to cast from.</param>
//    /// <returns>CustomFieldValue to cast to.</returns>
//    public static CustomFieldValue ToCustomFieldValue(this IEnumerable<IssueVersion> value)
//    {
//        return new CustomFieldValue(value);
//    }

//    #endregion
//}

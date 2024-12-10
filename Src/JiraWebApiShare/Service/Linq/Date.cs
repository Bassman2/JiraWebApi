//namespace JiraWebApi;

///// <summary>
///// Static class to represent JQL Date functions.
///// </summary>
//public static class Date
//{
//    internal static string ToJiraRestString(this DateTime dateTime)
//    {
//        // "2013-05-20T14:16:00.000+0200"
//        string dt = dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");
//        dt = dt.Remove(dt.LastIndexOf(':'), 1);
//        return dt;
//    }

//    internal static string ToJiraRestString(this DateTime? dateTime)
//    {
//        return dateTime == null ? string.Empty : dateTime.Value.ToJiraRestString();
//    }

//    /// <summary>
//    /// Static method to represent the JQL currentLogin() function
//    /// </summary>
//    /// <returns>No result.</returns>
//    /// <remarks>For Linq use only.</remarks>
//    [JqlFunction("currentLogin")]
//    public static DateTime CurrentLogin()
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Static method to represent the JQL lastLogin() function
//    /// </summary>
//    /// <returns>No result.</returns>
//    /// <remarks>For Linq use only.</remarks>
//    [JqlFunction("lastLogin")]
//    public static DateTime LastLogin()
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Static method to represent the JQL now() function
//    /// </summary>
//    /// <returns>No result.</returns>
//    /// <remarks>For Linq use only.</remarks>
//    [JqlFunction("now")]
//    public static DateTime Now()
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Static method to represent the JQL startOfDay() function
//    /// </summary>
//    /// <returns>No result.</returns>
//    /// <remarks>For Linq use only.</remarks>
//    [JqlFunction("startOfDay")]
//    public static DateTime StartOfDay()
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Static method to represent the JQL startOfWeek() function
//    /// </summary>
//    /// <returns>No result.</returns>
//    /// <remarks>For Linq use only.</remarks>
//    [JqlFunction("startOfWeek")]
//    public static DateTime StartOfWeek()
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Static method to represent the JQL startOfMonth() function
//    /// </summary>
//    /// <returns>No result.</returns>
//    /// <remarks>For Linq use only.</remarks>
//    [JqlFunction("startOfMonth")]
//    public static DateTime StartOfMonth()
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Static method to represent the JQL startOfYear() function
//    /// </summary>
//    /// <returns>No result.</returns>
//    /// <remarks>For Linq use only.</remarks>
//    [JqlFunction("startOfYear")]
//    public static DateTime StartOfYear()
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Static method to represent the JQL endOfDay() function
//    /// </summary>
//    /// <returns>No result.</returns>
//    /// <remarks>For Linq use only.</remarks>
//    [JqlFunction("endOfDay")]
//    public static DateTime EndOfDay()
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Static method to represent the JQL endOfWeek() function
//    /// </summary>
//    /// <returns>No result.</returns>
//    /// <remarks>For Linq use only.</remarks>
//    [JqlFunction("endOfWeek")]
//    public static DateTime EndOfWeek()
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Static method to represent the JQL endOfMonth() function
//    /// </summary>
//    /// <returns>No result.</returns>
//    /// <remarks>For Linq use only.</remarks>
//    [JqlFunction("endOfMonth")]
//    public static DateTime EndOfMonth()
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Static method to represent the JQL endOfYear() function
//    /// </summary>
//    /// <returns>No result.</returns>
//    /// <remarks>For Linq use only.</remarks>
//    [JqlFunction("endOfYear")]
//    public static DateTime EndOfYear()
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }
//}

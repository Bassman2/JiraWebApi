namespace JiraWebApi;

/// <summary>
/// Abstract class for item which can be sorted wit Linq.
/// </summary>
/// <remarks>
/// Operatores not useable in code. Only for Linq syntax.
/// </remarks>
public abstract class SortableElement : ComparableElementModel
{
    /// <summary>
    /// Compare less than operator to allow LINQ compare.
    /// </summary>
    /// <param name="element1">The first element to compare, or null.</param>
    /// <param name="element2">The second element to compare, or null.</param>
    /// <returns>true if the first operand is less than to the second, false otherwise.</returns>
    /// <remarks>For Linq use only.</remarks>
    public static bool operator <(SortableElement element1, SortableElement element2)
    {
        throw new NotSupportedException("Operator only defined to allow Linq comparison.");
    }

    /// <summary>
    /// Compare less than operator to allow LINQ compare.
    /// </summary>
    /// <param name="element">The element to compare, or null.</param>
    /// <param name="name">The name to compare, or null.</param>
    /// <returns>true if the first operand is less than to the second, false otherwise.</returns>
    /// <remarks>For Linq use only.</remarks>
    public static bool operator <(SortableElement element, string name)
    {
        throw new NotSupportedException("Operator only defined to allow Linq comparison.");
    }

    /// <summary>
    /// Compare less than or equal operator to allow LINQ compare.
    /// </summary>
    /// <param name="element1">The first element to compare, or null.</param>
    /// <param name="element2">The second element to compare, or null.</param>
    /// <returns>true if the first operand is less than or equal to the second, false otherwise.</returns>
    /// <remarks>For Linq use only.</remarks>
    public static bool operator <=(SortableElement element1, SortableElement element2)
    {
        throw new NotSupportedException("Operator only defined to allow Linq comparison.");
    }

    /// <summary>
    /// Compare less than or equal operator to allow LINQ compare.
    /// </summary>
    /// <param name="element">The element to compare, or null.</param>
    /// <param name="name">The name to compare, or null.</param>
    /// <returns>true if the first operand is less than or equal to the second, false otherwise.</returns>
    /// <remarks>For Linq use only.</remarks>
    public static bool operator <=(SortableElement element, string name)
    {
        throw new NotSupportedException("Operator only defined to allow Linq comparison.");
    }

    /// <summary>
    /// Compare greater than operator to allow LINQ compare.
    /// </summary>
    /// <param name="element1">The first element to compare, or null.</param>
    /// <param name="element2">The second element to compare, or null.</param>
    /// <returns>true if the first operand is greater than or equal to the second, false otherwise.</returns>
    /// <remarks>For Linq use only.</remarks>
    public static bool operator >(SortableElement element1, SortableElement element2)
    {
        throw new NotSupportedException("Operator only defined to allow Linq comparison.");
    }

    /// <summary>
    /// Compare greater than operator to allow LINQ compare.
    /// </summary>
    /// <param name="element">The element to compare, or null.</param>
    /// <param name="name">The name to compare, or null.</param>
    /// <returns>true if the first operand is greater than or equal to the second, false otherwise.</returns>
    /// <remarks>For Linq use only.</remarks>
    public static bool operator >(SortableElement element, string name)
    {
        throw new NotSupportedException("Operator only defined to allow Linq comparison.");
    }

    /// <summary>
    /// Compare greater or equal operator to allow LINQ compare.
    /// </summary>
    /// <param name="element1">The first element to compare, or null.</param>
    /// <param name="element2">The second element to compare, or null.</param>
    /// <returns>true if the first operand is greater than or equal to the second, false otherwise.</returns>
    /// <remarks>For Linq use only.</remarks>
    public static bool operator >=(SortableElement element1, SortableElement element2)
    {
        throw new NotSupportedException("Operator only defined to allow Linq comparison.");
    }

    /// <summary>
    /// Compare greater or equal operator to allow LINQ compare.
    /// </summary>
    /// <param name="element">The element to compare, or null.</param>
    /// <param name="name">The name to compare, or null.</param>
    /// <returns>true if the first operand is greater than or equal to the second, false otherwise.</returns>
    /// <remarks>For Linq use only.</remarks>
    public static bool operator >=(SortableElement element, string name)
    {
        throw new NotSupportedException("Operator only defined to allow Linq comparison.");
    }

    
}

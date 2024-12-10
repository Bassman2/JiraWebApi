//namespace JiraWebApi;

///// <summary>
///// Special string to allow Linq to sort this strings.
///// </summary>
//public class SortableString
//{
//    private string value = ""; 

//    /// <summary>
//    /// Initializes a new instance of the SortableString class.
//    /// </summary>
//    public SortableString()
//    { }
    
//    /// <summary>
//    /// Initializes a new instance of the SortableString class.
//    /// </summary>
//    /// <param name="value">Value to initialize with.</param>
//    public SortableString(string value)
//    {
//        this.value = value;
//    }

//    /// <summary>
//    /// Returns a string that represents the sortable string.
//    /// </summary>
//    /// <returns>A string element.</returns>
//    public override string ToString()
//    {
//        return this.value;
//    }

//    /// <summary>
//    /// Support of the JQL 'in' operator in LINQ.
//    /// </summary>
//    /// <param name="values">Values for the 'in' JQL function</param>
//    /// <returns>Not used.</returns>
//    public bool In(params string[] values)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Support of the JQL 'in' operator 'in' LINQ.
//    /// </summary>
//    /// <param name="values">Values for the in JQL function</param>
//    /// <returns>Not used.</returns>
//    public bool In(params ElementModel[] values)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Support of the JQL 'in' operator in LINQ.
//    /// </summary>
//    /// <param name="values">Values for the 'in' JQL function</param>
//    /// <returns>Not used.</returns>
//    public bool In(params Issue[] values)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Support of the JQL 'not in' operator in LINQ.
//    /// </summary>
//    /// <param name="values">Values for the 'not in' JQL function.</param>
//    /// <returns>Not used.</returns>
//    public bool NotIn(params string[] values)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Support of the JQL 'not in' operator in LINQ.
//    /// </summary>
//    /// <param name="values">Values for the 'not in' JQL function.</param>
//    /// <returns>Not used.</returns>
//    public bool NotIn(params ElementModel[] values)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Support of the JQL 'not in' operator in LINQ.
//    /// </summary>
//    /// <param name="values">Values for the 'not in' JQL function.</param>
//    /// <returns>Not used.</returns>
//    public bool NotIn(params Issue[] values)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Determines whether the specified object is equal to the current object.
//    /// </summary>
//    /// <param name="obj">The object to compare with the current object.</param>
//    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
//    public override bool Equals(object? obj)
//    {
//        if (obj is SortableString item)
//        {
//            return this.value == item.value;
//        }
//        return false;
        
//    }

//    /// <summary>
//    /// Serves as a hash function for a particular type. 
//    /// </summary>
//    /// <returns>A hash code for the current Object.</returns>
//    public override int GetHashCode()
//    {
//        return this.value == null ? base.GetHashCode() : this.value.GetHashCode();
//    }

//    /// <summary>
//    /// Compare equal operator.
//    /// </summary>
//    /// <param name="item1">The first element to compare, or null.</param>
//    /// <param name="item2">The second element to compare, or null.</param>
//    /// <returns>true if the id of the first element is equal to the id of the second element; otherwise, false.</returns>
//    public static bool operator ==(SortableString item1, SortableString item2)
//    {
//        if (ReferenceEquals(item1, item2))
//        {
//            return true;
//        }
//        if (((object)item1 == null) || ((object)item2 == null))
//        {
//            return false;
//        }
//        return item1.Equals(item2);
//    }

//    /// <summary>
//    ///  Compare not equal operator.
//    /// </summary>
//    /// <param name="item1">The first element to compare, or null.</param>
//    /// <param name="item2">The second element to compare, or null.</param>
//    /// <returns>true if the id of the first element is different from the id of the second element; otherwise, false.</returns>
//    public static bool operator !=(SortableString item1, SortableString item2)
//    {
//        return !(item1 == item2); 
//    }

//    /// <summary>
//    /// Compare greater than operator to allow LINQ compare.
//    /// </summary>
//    /// <param name="item1">The first element to compare, or null.</param>
//    /// <param name="item2">The second element to compare, or null.</param>
//    /// <returns>Not used</returns>
//    /// <remarks>For Linq use only.</remarks>
//    public static bool operator >(SortableString item1, SortableString item2)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Compare greater or equal operator to allow LINQ compare.
//    /// </summary>
//    /// <param name="item1">The first element to compare, or null.</param>
//    /// <param name="item2">The second element to compare, or null.</param>
//    /// <returns>Not used</returns>
//    /// <remarks>For Linq use only.</remarks>
//    public static bool operator >=(SortableString item1, SortableString item2)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Compare less than operator to allow LINQ compare.
//    /// </summary>
//    /// <param name="item1">The first element to compare, or null.</param>
//    /// <param name="item2">The second element to compare, or null.</param>
//    /// <returns>Not used</returns>
//    /// <remarks>For Linq use only.</remarks>
//    public static bool operator <(SortableString item1, SortableString item2)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Compare less than or equal operator to allow LINQ compare.
//    /// </summary>
//    /// <param name="item1">The first element to compare, or null.</param>
//    /// <param name="item2">The second element to compare, or null.</param>
//    /// <returns>Not used</returns>
//    /// <remarks>For Linq use only.</remarks>
//    public static bool operator <=(SortableString item1, SortableString item2)
//    {
//        throw new NotSupportedException(ExceptionMessages.ForLinqUseOnly);
//    }

//    /// <summary>
//    /// Implicite cast from SortableString to string.
//    /// </summary>
//    /// <param name="item">Item to cast.</param>
//    /// <returns>Casted value.</returns>
//    public static implicit operator string(SortableString item)
//    {
//        return item.value;
//    }

//    /// <summary>
//    /// Implicite cast from string to SortableString.
//    /// </summary>
//    /// <param name="item">Item to cast.</param>
//    /// <returns>Casted value.</returns>
//    public static implicit operator SortableString(string item)
//    {
//        return new SortableString(item);
//    }
//}

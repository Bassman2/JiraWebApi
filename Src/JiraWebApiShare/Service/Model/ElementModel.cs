namespace JiraWebApi.Service.Model;

/// <summary>
/// Base class for JIRA items with an Id.
/// </summary>
public abstract class ElementModel
{
    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    [JsonPropertyName("self")]
    public string? Self { get; set; }

    /// <summary>
    /// Id of the JIRA item.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    ///// <summary>
    ///// Determines whether the specified object is equal to the current object.
    ///// </summary>
    ///// <param name="obj">The object to compare with the current object.</param>
    ///// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    //public override bool Equals(object? obj)
    //{
    //    if (obj is not ElementModel element)
    //    {
    //        return false;
    //    }
    //    return this.Id == element.Id;
    //}

    ///// <summary>
    ///// Serves as a hash function for a particular type. 
    ///// </summary>
    ///// <returns>A hash code for the current Object.</returns>
    //public override int GetHashCode()
    //{
    //    return base.GetHashCode();
    //}

    ///// <summary>
    ///// Compare equal operator.
    ///// </summary>
    ///// <param name="element1">The first element to compare, or null.</param>
    ///// <param name="element2">The second element to compare, or null.</param>
    ///// <returns>true if the id of the first element is equal to the id of the second element; otherwise, false.</returns>
    //public static bool operator ==(ElementModel element1, ElementModel element2)
    //{
    //    if (ReferenceEquals(element1, element2))
    //    {
    //        return true;
    //    }
    //    if (((object)element1 == null) || ((object)element2 == null))
    //    {
    //        return false;
    //    }
    //    return element1.Equals(element2);
    //}

    ///// <summary>
    /////  Compare not equal operator.
    ///// </summary>
    ///// <param name="element1">The first element to compare, or null.</param>
    ///// <param name="element2">The second element to compare, or null.</param>
    ///// <returns>true if the id of the first element is different from the id of the second element; otherwise, false.</returns>
    //public static bool operator !=(ElementModel element1, ElementModel element2)
    //{
    //    return !(element1 == element2);
    //}
}

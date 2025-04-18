﻿namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA version. 
/// </summary>
/// <remarks>
/// IssueVersion has the prefix Issue to separate from System.Version.
/// </remarks>
public class IssueVersion 
{
    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    [JsonPropertyName("self")]
    public Uri? Self { get; set; }

    /// <summary>
    /// Id of the JIRA item.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Name of the JIRA item.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    
    
    /// <summary>
    /// Description of the JIRA version.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Is version archived.
    /// </summary>
    [JsonPropertyName("archived")]
    public bool IsArchived { get; private set; }

    /// <summary>
    /// Is version released.
    /// </summary>
    [JsonPropertyName("released")]
    public bool IsReleased { get; private set; }

    /// <summary>
    /// Release date of the version.
    /// </summary>
    [JsonPropertyName("releaseDate")]
    public DateTime? ReleaseDate { get; private set; }

    /// <summary>
    /// Is version overdue.
    /// </summary>
    [JsonPropertyName("overdue")]
    public bool IsOverdue { get; private set; }

    /// <summary>
    /// User release date of the version.
    /// </summary>
    [JsonPropertyName("userReleaseDate")]
    public DateTime? UserReleaseDate { get; private set; }
  
    /// <summary>
    /// Project to which the version belongs.
    /// </summary>
    [JsonPropertyName("project")]
    public string? ProjectKey { private get; set; }   // to create only
}

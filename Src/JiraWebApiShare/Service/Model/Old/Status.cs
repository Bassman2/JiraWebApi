﻿namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA issue status. 
/// </summary>
public sealed class Status
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

    /// <summary>
    /// Name of the JIRA item.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Description of the JIRA status.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; private set; }

    /// <summary>
    /// Url of the issue status icon.
    /// </summary>
    [JsonPropertyName("iconUrl")]
    public Uri? IconUrl { get; private set; }
}

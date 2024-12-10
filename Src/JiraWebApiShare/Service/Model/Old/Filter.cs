﻿namespace JiraWebApi;

/// <summary>
/// Representation of a JIRA filter.
/// </summary>
public sealed class Filter 
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
    /// Description of the JIRA filter.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Owner name of the JIRA filter.
    /// </summary>
    [JsonPropertyName("owner")]
    public User? Owner { get; set; }

    /// <summary>
    /// JQL string of the JIRA filter.
    /// </summary>
    [JsonPropertyName("jql")]
    public string? Jql { get; set; }

    /// <summary>
    /// Url of the filters view web page.
    /// </summary>
    [JsonPropertyName("viewUrl")]
    public Uri? ViewUrl { get; private set; }

    /// <summary>
    /// Url of the filters search web page.
    /// </summary>
    [JsonPropertyName("searchUrl")]
    public Uri? SearchUrl { get; private set; }

    /// <summary>
    /// Signals if the filter is marked as favourite.
    /// </summary>
    [JsonPropertyName("favourite")]
    public bool IsFavourite { get; set; }

    /// <summary>
    /// Share permissions of the JIRA filter
    /// </summary>
    [JsonPropertyName("sharePermissions")]
    public IEnumerable<Permission>? SharePermissions { get; set; }

    /// <summary>
    /// Subscriptions of the JIRA filter.
    /// </summary>
    [JsonPropertyName("subscriptions")]
    public Subscriptions? Subscriptions { get; set; }
}

namespace JiraWebApi.Service.Model;


/// <summary>
/// Representation of a JIRA filter.
/// </summary>
internal class FilterModel 
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
    public Uri? ViewUrl { get; set; }

    /// <summary>
    /// Url of the filters search web page.
    /// </summary>
    [JsonPropertyName("searchUrl")]
    public Uri? SearchUrl { get; set; }

    /// <summary>
    /// Signals if the filter is marked as favourite.
    /// </summary>
    [JsonPropertyName("favourite")]
    public bool IsFavourite { get; set; }

    /// <summary>
    /// Share permissions of the JIRA filter
    /// </summary>
    [JsonPropertyName("sharePermissions")]
    public IEnumerable<PermissionModel>? SharePermissions { get; set; }

    /// <summary>
    /// Subscriptions of the JIRA filter.
    /// </summary>
    [JsonPropertyName("subscriptions")]
    public SubscriptionsModel? Subscriptions { get; set; }
}

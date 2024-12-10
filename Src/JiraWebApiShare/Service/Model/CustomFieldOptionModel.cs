namespace JiraWebApi.Service.Model;

/// <summary>
/// Representation of JIRA options for fixed value custom fields. 
/// </summary>
internal sealed class CustomFieldOptionModel
{
    

    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    [JsonPropertyName("self")]
    public Uri? Self { get; private set; }

    /// <summary>
    /// Value of the custom field option.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; private set; } 

    /// <summary>
    /// Id of the custom field option.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; private set; }

    /// <summary>
    /// Child custom field option.
    /// </summary>
    [JsonPropertyName("child")]
    public CustomFieldOption? Child { get; private set; }
}


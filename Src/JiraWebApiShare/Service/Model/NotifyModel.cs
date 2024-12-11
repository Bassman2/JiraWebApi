namespace JiraWebApi.Service.Model;

/// <summary>
/// Representatiojn of a JIRA notification e-mail.
/// </summary>
internal sealed class NotifyModel
{
    /// <summary>
    /// Subject of the notification e-mail.
    /// </summary>
    [JsonPropertyName("subject")]
    public string? Subject { get; set; }

    /// <summary>
    /// Text body of the notification e-mail.
    /// </summary>
    [JsonPropertyName("textBody")]
    public string? TextBody { get; set; }

    /// <summary>
    /// HTML body of the notification e-mail.
    /// </summary>
    [JsonPropertyName("htmlBody")]
    public string? HtmlBody { get; set; }

    /// <summary>
    /// Addresses of the notification e-mail.
    /// </summary>
    [JsonPropertyName("to")]
    public NotifyToModel? To { get; set; }

    /// <summary>
    /// Restriction of the notification.
    /// </summary>
    [JsonPropertyName("restrict")]
    public RestrictModel? Restrict { get; set; }
}

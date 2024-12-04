namespace JiraWebApi;

/// <summary>
/// Representatiojn of a JIRA notification e-mail.
/// </summary>
public sealed class Notify
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
    public NotifyTo? To { get; set; }

    /// <summary>
    /// Restriction of the notification.
    /// </summary>
    [JsonPropertyName("restrict")]
    public Restrict? Restrict { get; set; }
}

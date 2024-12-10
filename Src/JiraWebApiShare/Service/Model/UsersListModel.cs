namespace JiraWebApi.Service.Model;

/// <summary>
/// List of users.
/// </summary>
internal sealed class UsersListModel
{
    internal UsersListModel()
    { }

    /// <summary>
    /// Number of users
    /// </summary>
    [JsonPropertyName("size")]
    public int Size { get; private set; }

    /// <summary>
    /// List of users.
    /// </summary>
    [JsonPropertyName("items")]
    public List<User>? Items { get; private set; }

    /// <summary>
    /// Max number of users to get.
    /// </summary>
    [JsonPropertyName("max-results")]
    public int MaxResults { get; private set; }

    /// <summary>
    /// Start index of user get window.
    /// </summary>
    [JsonPropertyName("start-index")]
    public int StartIndex { get; private set; }

    /// <summary>
    /// End index of user get window.
    /// </summary>
    [JsonPropertyName("end-index")]
    public int EndIndex { get; private set; }
}

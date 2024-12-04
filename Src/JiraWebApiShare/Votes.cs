namespace JiraWebApi;

/// <summary>
/// Representation of JIRA issue votes.
/// </summary>
public class Votes
{
    /// <summary>
    /// Initializes a new instance of the Votes class.
    /// </summary>
    public Votes()
    {}

    /// <summary>
    /// Url of the JIRA REST item.
    /// </summary>
    [JsonPropertyName("self")]
    public Uri? Self { get; private set; }

    /// <summary>
    /// Number of votes to the JRA issue.
    /// </summary>
    [JsonPropertyName("votes")]
    public int VoteNum { get; private set; }

    /// <summary>
    /// Signals if someone has voted.
    /// </summary>
    [JsonPropertyName("hasVoted")]
    public bool IsVoted { get; private set; }

    /// <summary>
    /// List of voters.
    /// </summary>
    /// <remarks>Only filled by GetIssueVotesAsync.</remarks>
    [JsonPropertyName("voters")]
    public IEnumerable<User>? Voters { get; private set; }
}

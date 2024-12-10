namespace JiraWebApi.Service.Model;

internal class IssueVersionModel
{
    [JsonPropertyName("self")]
    public string? Self { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("archived")]
    public bool IsArchived { get; set; }

    [JsonPropertyName("released")]
    public bool IsReleased { get; set; }

    [JsonPropertyName("releaseDate")]
    public DateTime? ReleaseDate { get; set; }

    [JsonPropertyName("overdue")]
    public bool IsOverdue { get; set; }

    [JsonPropertyName("userReleaseDate")]
    public DateTime? UserReleaseDate { get; set; }
  
    [JsonPropertyName("project")]
    public string? ProjectKey { get; set; }   // to create only
}

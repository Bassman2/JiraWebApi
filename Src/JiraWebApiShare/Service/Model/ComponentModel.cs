namespace JiraWebApi.Service.Model;

internal class ComponentModel
{
    [JsonPropertyName("self")]
    public required string Self { get; set; }

    [JsonPropertyName("id")]
    public required int Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("assigneeType")]
    public string? AssigneeType { get; set; }

    [JsonPropertyName("assignee")]
    public UserModel? Assignee { get; set; }

    [JsonPropertyName("realAssigneeType")]
    public string? RealAssigneeType { get; set; }

    [JsonPropertyName("realAssignee")]
    public UserModel? RealAssignee { get; set; }

    [JsonPropertyName("isAssigneeTypeValid")]
    public bool IsAssigneeTypeValid { get; set; }

    [JsonPropertyName("project")]
    public string? ProjectKey { get; set; }

    [JsonPropertyName("projectId")]
    public int ProjectId { get; set; }

    [JsonPropertyName("archived")]
    public bool Archived { get; set; }

    [JsonPropertyName("deleted")]
    public bool Deleted { get; set; }
}

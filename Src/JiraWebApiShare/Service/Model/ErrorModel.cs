namespace JiraWebApi.Service.Model;

//// {"errorMessages":[],"errors":{"projectKey":"A project with that project key already exists."}}
//// {"errorMessages":["Field 'priority' is required"],"errors":{}}
//// {"errorMessages":[],"errors":{"title":"'title' is required."}}


internal class ErrorModel
{
    [JsonPropertyName("errorMessages")]
    public IEnumerable<string>? ErrorMessages { get; set; }

    [JsonPropertyName("errors")]
    public IDictionary<string, string>? Errors { get; set; }

    public override string ToString()
    {
        string? errorMesssages = ErrorMessages?.Aggregate("", (a, b) => $"{a}\r\n{b}");
        string? errors = this.Errors?.Select(e => $"{e.Key} : {e.Value}").Aggregate("", (a, b) => $"{a}\r\n{b}");
        return $"ErrorMessages: {errorMesssages}\r\nErrors: {errors}";
    }
}

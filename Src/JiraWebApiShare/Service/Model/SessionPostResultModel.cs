namespace JiraWebApi.Service.Model;

internal class SessionPostResultModel
{
    [JsonPropertyName("session")]
    public SessionModel? Session { get; set; }

    [JsonPropertyName("loginInfo")]
    public LoginInfoModel? LoginInfo { get; set; }

}

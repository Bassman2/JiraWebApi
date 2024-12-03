namespace JiraWebApi.Service.Autenticator;

internal class JiraAutenticator : IAuthenticator
{
    private readonly string? username;
    private readonly string? password;
    
    public JiraAutenticator(string username, string password)
    {
        this.username = username;
        this.password = password;
    }


    public void Authenticate(WebService service, HttpClient client)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return;
        }

        if (service is JsonService jsonService)
        {
            var req = new SessionPostRequestModel() { Username = username, Password = password };
            var _ = jsonService.PostAsJsonAsync<SessionPostRequestModel, SessionPostResultModel>("rest/auth/1/session", req, default).Result;

            //SessionPostRequest req = new SessionPostRequest() { Username = username, Password = password };
            //using (HttpResponseMessage response = client.PostAsJsonAsync("rest/auth/1/session", req).Result)
            //{
            //    response.EnsureSuccess();
            //    SessionPostResult res = response.Content.ReadFromJsonAsync<SessionPostResult>().Result;
            //    //return res.Session;
            //}
        }
        
    }
}

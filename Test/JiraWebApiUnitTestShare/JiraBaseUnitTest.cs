namespace JiraWebApiUnitTest;


public abstract class JiraBaseUnitTest
{
    protected static readonly Uri host = new(Environment.GetEnvironmentVariable("JIRA_HOST")!);
    protected static readonly string login = Environment.GetEnvironmentVariable("JIRA_LOGIN")!;
    protected static readonly string password = Environment.GetEnvironmentVariable("JIRA_PASSWORD")!;
    protected const string testProject = "NAVSUITE";
    
}

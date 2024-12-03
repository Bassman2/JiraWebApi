namespace JiraWebApiUnitTest;

public abstract class JiraBaseUnitTest
{
    protected static readonly Uri host = new(Environment.GetEnvironmentVariable("JIRA_HOST")!);
    protected static readonly string apikey = Environment.GetEnvironmentVariable("JIRA_APIKEY")!;
    protected const string testProject = "NAVSUITE";
    protected const string mainIssue = "NAVSUITE-13";


}

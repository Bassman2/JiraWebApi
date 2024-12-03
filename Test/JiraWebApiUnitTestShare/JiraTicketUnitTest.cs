using JiraWebApi;

namespace JiraWebApiUnitTest;

[TestClass]
public class JiraTicketUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetIssueTypesAsync()
    {
        using var jira = new Jira(host, apikey);

        var res = await jira.GetIssueTypesAsync();

        var issueTypes = res?.ToList();

        Assert.IsNotNull(issueTypes);
    }

    [TestMethod]
    public async Task TestMethodGetProjectsAsync()
    {
        using var jira = new Jira(host, apikey);

        var res = await jira.GetProjectsAsync();

        var projects = res?.ToList();
        Assert.IsNotNull(projects);

        var project = projects.FirstOrDefault(p => p.Key == testProject);
        Assert.IsNotNull(project);
        Assert.AreEqual("https://jira.elektrobit.com/rest/api/2/project/25411", project.Self, nameof(project.Self));
        Assert.AreEqual("25411", project.Id, nameof(project.Id));
        Assert.AreEqual("Navigation Suite", project.Name, nameof(project.Name));
        Assert.AreEqual("NAVSUITE", project.Key, nameof(project.Key));
        Assert.AreEqual(null, project.Description, nameof(project.Description));
        Assert.AreEqual(null, project.Url?.ToString(), nameof(project.Url));
        Assert.AreEqual(null, project.Email, nameof(project.Email));
        Assert.AreEqual(null, project.AssigneeType, nameof(project.AssigneeType));
    }

    [TestMethod]
    public async Task TestMethodCreateTickethAsync()
    {
        using var jira = new Jira(host, apikey);
        
        var issue = await jira.CreateIssueAsync("bug", testProject, "Test Ticket 1", "Description of the issue");

        Assert.IsNotNull(issue);
        StringAssert.StartsWith("NAVSUITE-", issue.Key, nameof(issue.Key));
        Assert.AreEqual(null, issue.IssueType.Name,  nameof(issue.IssueType.Name));
        Assert.AreEqual(null, issue.Summary, nameof(issue.Summary));
        Assert.AreEqual(null, issue.Description, nameof(issue.Description));
    }

    [TestMethod]
    public async Task TestMethodCreateSubTickethAsync()
    {
        using var jira = new Jira(host, apikey);

        var issue = await jira.CreateSubIssueAsync(mainIssue, "sub-task", testProject, "Test Ticket 1", "Description of the issue");

        Assert.IsNotNull(issue);
        StringAssert.StartsWith("NAVSUITE-", issue.Key, nameof(issue.Key));
        Assert.AreEqual(null, issue.IssueType.Name, nameof(issue.IssueType.Name));
        Assert.AreEqual(null, issue.Summary, nameof(issue.Summary));
        Assert.AreEqual(null, issue.Description, nameof(issue.Description));
    }
}

namespace JiraWebApiUnitTest;

[TestClass]
public class JiraTicketUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodCreateTickethAsync()
    {
        using var jira = new Jira(new Uri(host), token);

        var project = (await jira.GetProjectsAsync())?.FirstOrDefault(p => p.Key == testProject || p.Name == testProject);
        var issueType = (await jira.GetIssueTypesAsync())?.FirstOrDefault(t => t.Name == "Bug");

        var issue = await jira.CreateIssueAsync(project!.Id!, issueType!.Id!, login, "Test Ticket 1", "Description of the issue");

        Assert.IsNotNull(issue);
        StringAssert.StartsWith("NAVSUITE-", issue.Key, nameof(issue.Key));

        Assert.AreEqual(null, issue.IssueType?.Name,  nameof(issue.IssueType.Name));
        Assert.AreEqual(null, issue.Summary, nameof(issue.Summary));
        Assert.AreEqual(null, issue.Description, nameof(issue.Description));
    }

    [TestMethod]
    public async Task TestMethodCreateSubTickethAsync()
    {
        using var jira = new Jira(new Uri(host), token);

        var project = (await jira.GetProjectsAsync())?.FirstOrDefault(p => p.Key == testProject || p.Name == testProject);
        var issueType = (await jira.GetIssueTypesAsync())?.FirstOrDefault(t => t.Name == "Bug");
        //var parent = await jira.GetIssueAsync(mainIssue);

        var issue = await jira.CreateSubIssueAsync(mainIssue, project!.Id!, issueType!.Id!, login, "Test Sub Ticket 1", "Description of the sub issue");
                
        Assert.IsNotNull(issue);
        StringAssert.StartsWith("NAVSUITE-", issue.Key, nameof(issue.Key));
        Assert.AreEqual(null, issue.IssueType?.Name, nameof(issue.IssueType.Name));
        Assert.AreEqual(null, issue.Summary, nameof(issue.Summary));
        Assert.AreEqual(null, issue.Description, nameof(issue.Description));
    }
}

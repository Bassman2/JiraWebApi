namespace JiraWebApiUnitTest;

[TestClass]
public class JiraIssueUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodCreateIssueAsync()
    {
        using var jira = new Jira(new Uri(host), token);

        var project = await jira.GetProjectByKeyAsync(testProject);
        Assert.IsNotNull(project);

        var issueType = await jira.GetIssueTypeAsync("Bug");
        Assert.IsNotNull(issueType);

        var issue = await jira.CreateIssueAsync(project!, issueType!, login, "Test Ticket 1", "Description of the issue");

        Assert.IsNotNull(issue);
        StringAssert.StartsWith(issue.Key, "NAVSUITE-", nameof(issue.Key));

        //Assert.AreEqual(null, issue.IssueType?.Name,  nameof(issue.IssueType.Name));
        //Assert.AreEqual(null, issue.Summary, nameof(issue.Summary));
        //Assert.AreEqual(null, issue.Description, nameof(issue.Description));
    }

    [TestMethod]
    public async Task TestMethodCreateProjectIssueAsync()
    {
        using var jira = new Jira(new Uri(host), token);

        var project = await jira.GetProjectByKeyAsync(testProject);
        Assert.IsNotNull(project);

        var issueType = await jira.GetIssueTypeAsync("Bug");
        Assert.IsNotNull(issueType);

        var issue = await project!.CreateIssueAsync(issueType!, login, "Test Ticket 1", "Description of the issue");

        Assert.IsNotNull(issue);
        StringAssert.StartsWith(issue.Key, "NAVSUITE-", nameof(issue.Key));

        //Assert.AreEqual(null, issue.IssueType?.Name, nameof(issue.IssueType.Name));
        //Assert.AreEqual(null, issue.Summary, nameof(issue.Summary));
        //Assert.AreEqual(null, issue.Description, nameof(issue.Description));
    }

    [TestMethod]
    public async Task TestMethodCreateSubIssueAsync()
    {
        using var jira = new Jira(new Uri(host), token);

        var main = await jira.GetIssueAsync(mainIssue);
        Assert.IsNotNull(main);

        var issueType = await jira.GetIssueTypeAsync("sub-task");
        Assert.IsNotNull(issueType);

        var issue = await main!.CreateSubIssueAsync(issueType!, login, "Test Sub Ticket 1", "Description of the sub issue");

        Assert.IsNotNull(issue);
        StringAssert.StartsWith(issue.Key, "NAVSUITE-", nameof(issue.Key));
        //Assert.AreEqual(null, issue.IssueType?.Name, nameof(issue.IssueType.Name));
        //Assert.AreEqual(null, issue.Summary, nameof(issue.Summary));
        //Assert.AreEqual(null, issue.Description, nameof(issue.Description));
    }
}

namespace JiraWebApiUnitTest;

[TestClass]
public class JiraTicketUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodCreateTickethAsync()
    {
        using var jira = new Jira(new Uri(host), token);
        
        //var meta = await jira.GetCreateMetaAsync(testProject, "Bug", "Test Ticket 1", "Description of the issue");

        var issue = await jira.CreateIssueAsync("Bug", testProject, "Test Ticket 1", "Description of the issue");

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

        var issue = await jira.CreateSubIssueAsync(mainIssue, "sub-task", testProject, "Test Ticket 1", "Description of the issue");

        Assert.IsNotNull(issue);
        StringAssert.StartsWith("NAVSUITE-", issue.Key, nameof(issue.Key));
        Assert.AreEqual(null, issue.IssueType?.Name, nameof(issue.IssueType.Name));
        Assert.AreEqual(null, issue.Summary, nameof(issue.Summary));
        Assert.AreEqual(null, issue.Description, nameof(issue.Description));
    }

    //[TestMethod]
    //public async Task TestMethodCloneTickethAsync()
    //{
    //    using var jira = new Jira(host, token);

    //    var issue = await jira.CloneIssueAsync(mainIssue, "sub-task", testProject, "Test Ticket 1", "Description of the issue");

    //    Assert.IsNotNull(issue);
    //    StringAssert.StartsWith("NAVSUITE-", issue.Key, nameof(issue.Key));
    //    Assert.AreEqual(null, issue.IssueType?.Name, nameof(issue.IssueType.Name));
    //    Assert.AreEqual(null, issue.Summary, nameof(issue.Summary));
    //    Assert.AreEqual(null, issue.Description, nameof(issue.Description));
    //}
}

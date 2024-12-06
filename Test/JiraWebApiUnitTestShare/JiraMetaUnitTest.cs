namespace JiraWebApiUnitTest;

[TestClass]
public class JiraMetaUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetCreateMetaAsync()
    {
        using var jira = new Jira(new Uri(host), token);

        var meta = await jira.GetCreateMetaAsync(testProject, "bug");

        Assert.IsNotNull(meta);
        //StringAssert.StartsWith("NAVSUITE-", issue.Key, nameof(issue.Key));

        //Assert.AreEqual(null, issue.IssueType?.Name, nameof(issue.IssueType.Name));
        //Assert.AreEqual(null, issue.Summary, nameof(issue.Summary));
        //Assert.AreEqual(null, issue.Description, nameof(issue.Description));
    }

    [TestMethod]
    public async Task TestMethodGetCreateSubMetaAsync()
    {
        using var jira = new Jira(new Uri(host), token);

        var meta = await jira.GetCreateMetaAsync(testProject, "sub-task");

        Assert.IsNotNull(meta);
        //StringAssert.StartsWith("NAVSUITE-", issue.Key, nameof(issue.Key));

        //Assert.AreEqual(null, issue.IssueType?.Name, nameof(issue.IssueType.Name));
        //Assert.AreEqual(null, issue.Summary, nameof(issue.Summary));
        //Assert.AreEqual(null, issue.Description, nameof(issue.Description));
    }

    [TestMethod]
    public async Task TestMethodGetEditMetaAsync()
    {
        using var jira = new Jira(new Uri(host), token);

        var meta = await jira.GetEditMetaAsync(mainIssue);

        Assert.IsNotNull(meta);
        //StringAssert.StartsWith("NAVSUITE-", issue.Key, nameof(issue.Key));

        //Assert.AreEqual(null, issue.IssueType?.Name, nameof(issue.IssueType.Name));
        //Assert.AreEqual(null, issue.Summary, nameof(issue.Summary));
        //Assert.AreEqual(null, issue.Description, nameof(issue.Description));
    }
}

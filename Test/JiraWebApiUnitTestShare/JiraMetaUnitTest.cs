namespace JiraWebApiUnitTest;

[TestClass]
public class JiraMetaUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetCreateMetaAsync()
    {
        using var jira = new Jira(storeKey, appName);

        var issueType = await jira.GetIssueTypeAsync("bug");
        Assert.IsNotNull(issueType);

        var meta = await jira.GetCreateMetaAsync(testProjectKey, issueType);
        Assert.IsNotNull(meta);
        //StringAssert.StartsWith("NAVSUITE-", issue.Key, nameof(issue.Key));

        //Assert.AreEqual(null, issue.IssueType?.Name, nameof(issue.IssueType.Name));
        //Assert.AreEqual(null, issue.Summary, nameof(issue.Summary));
        //Assert.AreEqual(null, issue.Description, nameof(issue.Description));
    }

    [TestMethod]
    public async Task TestMethodGetCreateSubMetaAsync()
    {
        using var jira = new Jira(storeKey, appName); 

        var issueType = await jira.GetIssueTypeAsync("sub-task");
        Assert.IsNotNull(issueType);

        var meta = await jira.GetCreateMetaAsync(testProjectKey, issueType);

        Assert.IsNotNull(meta);
        //StringAssert.StartsWith("NAVSUITE-", issue.Key, nameof(issue.Key));

        //Assert.AreEqual(null, issue.IssueType?.Name, nameof(issue.IssueType.Name));
        //Assert.AreEqual(null, issue.Summary, nameof(issue.Summary));
        //Assert.AreEqual(null, issue.Description, nameof(issue.Description));
    }

    [TestMethod]
    public async Task TestMethodGetEditMetaAsync()
    {
        using var jira = new Jira(storeKey, appName);

        var meta = await jira.GetEditMetaAsync(mainGroupIssue);

        Assert.IsNotNull(meta);
        //StringAssert.StartsWith("NAVSUITE-", issue.Key, nameof(issue.Key));

        //Assert.AreEqual(null, issue.IssueType?.Name, nameof(issue.IssueType.Name));
        //Assert.AreEqual(null, issue.Summary, nameof(issue.Summary));
        //Assert.AreEqual(null, issue.Description, nameof(issue.Description));
    }
}

namespace JiraWebApiUnitTest;

[TestClass]
public class JiraIssueGetUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetIssueAsync()
    {
        using var jira = new Jira(storeKey);


        var issue = await jira.GetIssueAsync(mainGetIssue);
        Assert.IsNotNull(issue);

        Assert.AreEqual(new Uri(apiUri, "issue/3518039"), issue.Self, nameof(issue.Self));
        Assert.AreEqual(3518039, issue.Id, nameof(issue.Id));
        Assert.AreEqual(mainGetIssue, issue.Key, nameof(issue.Key));

        Assert.AreEqual(null, issue.IssueType?.Name,  nameof(issue.IssueType.Name));
        Assert.AreEqual("Main Test Ticket", issue.Summary, nameof(issue.Summary));
        Assert.AreEqual("Description of the issue", issue.Description, nameof(issue.Description));
    }
}

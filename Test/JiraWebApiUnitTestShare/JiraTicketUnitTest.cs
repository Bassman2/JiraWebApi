namespace JiraWebApiUnitTest;

[TestClass]
public class JiraTicketUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetIssueTypesAsync()
    {
        using var github = new Jira(host, login, password);

        var res = await github.GetIssueTypesAsync();

        var issueTypes = res?.ToList();

        Assert.IsNotNull(issueTypes);
       // Assert.AreEqual(mainBranch, branch.Name, nameof(branch.Name));
    }

    //[TestMethod]
    //public async Task TestMethodCreateTickethAsync()
    //{
    //    using var github = new Jira(host, login, password);

    //    //var branch = await github.GetBranchAsync(testUser, testRepo, mainBranch);

    //    //Assert.IsNotNull(branch);
    //    //Assert.AreEqual(mainBranch, branch.Name, nameof(branch.Name));
    //}

    //[TestMethod]
    //public async Task TestMethodCreateSubTickethAsync()
    //{
    //    using var github = new Jira(host, login, password);

    //    //var branch = await github.GetBranchAsync(testUser, testRepo, mainBranch);

    //    //Assert.IsNotNull(branch);
    //    //Assert.AreEqual(mainBranch, branch.Name, nameof(branch.Name));
    //}
}

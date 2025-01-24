namespace JiraWebApiUnitTest;

[TestClass]
public class JiraIssueTypeUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetIssueTypesAsync()
    {
        using var jira = new Jira(storeKey, appName); 

        var res = await jira.GetIssueTypesAsync();

        var issueTypes = res?.ToList();
        Assert.IsNotNull(issueTypes);

        var issueType = issueTypes.FirstOrDefault(t => t.Name == "Bug");
        Assert.IsNotNull(issueType);
        Assert.AreEqual(new Uri(apiUri, "issuetype/1"), issueType.Self, nameof(issueType.Self));
        Assert.AreEqual(1, issueType.Id, nameof(issueType.Id));
        Assert.AreEqual("Bug", issueType.Name, nameof(issueType.Name));
        Assert.AreEqual("A problem which impairs or prevents the functions of the product.", issueType.Description, nameof(issueType.Description));
        Assert.AreEqual(new Uri(baseUri, "secure/viewavatar?size=xsmall&avatarId=27303&avatarType=issuetype"), issueType.IconUrl, nameof(issueType.IconUrl));
        Assert.AreEqual(false, issueType.IsSubtask, nameof(issueType.IsSubtask));
        Assert.AreEqual(27303, issueType.AvatarId, nameof(issueType.AvatarId));
    }
}

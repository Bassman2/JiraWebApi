namespace JiraWebApiUnitTest;

[TestClass]
public class JiraServerInfoUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetServerInfoAsync()
    {
        using var jira = new Jira(new Uri(host), token);

        var serverInfo = await jira.GetServerInfoAsync();

        Assert.IsNotNull(serverInfo);
        Assert.AreEqual(host, serverInfo.BaseUrl.ToString(), nameof(serverInfo.BaseUrl));
        Assert.AreEqual("9.12.14", serverInfo.Version.ToString(), nameof(serverInfo.Version));
        Assert.AreEqual("Server", serverInfo.DeploymentType, nameof(serverInfo.DeploymentType));
        Assert.AreEqual("02.10.2024", serverInfo.BuildDate.ToShortDateString(), nameof(serverInfo.BuildDate));
        Assert.AreEqual(DateTime.Now.ToShortDateString(), serverInfo.ServerTime.ToShortDateString(), nameof(serverInfo.ServerTime));
        Assert.AreEqual("2d138990723fa5f42e163802569c21f77dd4d489", serverInfo.ScmInfo, nameof(serverInfo.ScmInfo));
        Assert.AreEqual("EB external Jira", serverInfo.ServerTitle, nameof(serverInfo.ServerTitle));
    }
}

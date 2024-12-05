using System.Data;
using WebServiceClient.Store;

namespace JiraWebApiUnitTest;

public abstract class JiraBaseUnitTest
{
    //protected static readonly KeyItem keyItem = KeyStore.Load()["Jira"].
    //protected static readonly string hostPath = Environment.GetEnvironmentVariable("JIRA_HOST")!;
    //protected static readonly Uri host = new(Environment.GetEnvironmentVariable("JIRA_HOST")!);
    //protected static readonly string token = Environment.GetEnvironmentVariable("JIRA_APIKEY")!;
    //protected static readonly string login = Environment.GetEnvironmentVariable("USERNAME")!;

    protected static readonly KeyStore key = KeyStore.Key("jira")!;
    protected static readonly string host = key.Host!;
    protected static readonly string login = key.Login!;
    protected static readonly string token = key.Token!;
    protected static readonly string name = key.Name!;
    protected static readonly string email = key.Email!;

    protected const string testProject = "NAVSUITE";
    protected const string mainIssue = "NAVSUITE-13";

    //static JiraBaseUnitTest()
    //{
    //    var keyStore = KeyStore.Load("jira");
    //    if (keyStore != null)
    //    {
    //        host = new(keyStore.Host!);
    //        hostPath = keyStore.Host!;
    //        token = keyStore.Token!;
    //        login = keyStore.Login!;

    //    }
    //}


    //[ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
    //public static void ClassInitialize(TestContext testContext)
    //{
    //    var keyStore = KeyStore.Load("jira");
    //    if (keyStore != null)
    //    {
    //        host = new(keyStore.Host);
    //        hostPath = keyStore.Host;
    //    }
    //}
}

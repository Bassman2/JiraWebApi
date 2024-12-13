using WebServiceClient.Store;

namespace JiraWebApiUnitTest;

public abstract class JiraBaseUnitTest
{
    protected const string storeKey = "jira";

    protected static readonly string testHost = KeyStore.Key(storeKey)!.Host!;
    protected static readonly string testUserKey = KeyStore.Key(storeKey)!.Login!;
    protected static readonly string testUserDisplayName = KeyStore.Key(storeKey)!.Name!;
    protected static readonly string testUserEmail = KeyStore.Key(storeKey)!.Email!;

    protected static readonly Uri baseUri = new(testHost);
    protected static readonly Uri apiUri = new(baseUri, "rest/api/2/");

    protected const string testProjectKey = "NAVSUITE";
    protected const int testProjectId = 25411;

    protected const int testComponentId = 37767;
    protected const string testComponentName = "Application";

    protected const string mainGroupIssue = "NAVSUITE-13";
    protected const string mainGetIssue = "NAVSUITE-14";


}

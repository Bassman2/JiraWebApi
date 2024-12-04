namespace JiraWebApiUnitTest;

[TestClass]
public class JiraUserUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetUserAsync()
    {
        using var jira = new Jira(host, apikey);

        var user = await jira.GetUserAsync("bs");

        Assert.IsNotNull(user);
        Assert.AreEqual("https://jira.elektrobit.com/rest/api/2/user?username=bs", user.Self?.ToString(), nameof(user.Self));
        Assert.AreEqual("bs", user.Key, nameof(user.Key));
        Assert.AreEqual("bs", user.Name, nameof(user.Name));
        Assert.AreEqual("Ralf.Beckers@elektrobit.com", user.EmailAddress, nameof(user.EmailAddress));
        Assert.AreEqual("https://jira.elektrobit.com/secure/useravatar?size=xsmall&ownerId=bs&avatarId=10209", user.AvatarUrls?.AvatarUrl16?.ToString(), nameof(user.AvatarUrls.AvatarUrl16));
        Assert.AreEqual("https://jira.elektrobit.com/secure/useravatar?size=small&ownerId=bs&avatarId=10209", user.AvatarUrls?.AvatarUrl24?.ToString(), nameof(user.AvatarUrls.AvatarUrl24));
        Assert.AreEqual("https://jira.elektrobit.com/secure/useravatar?size=medium&ownerId=bs&avatarId=10209", user.AvatarUrls?.AvatarUrl32?.ToString(), nameof(user.AvatarUrls.AvatarUrl32));
        Assert.AreEqual("https://jira.elektrobit.com/secure/useravatar?ownerId=bs&avatarId=10209", user.AvatarUrls?.AvatarUrl48?.ToString(), nameof(user.AvatarUrls.AvatarUrl48));
        Assert.AreEqual("Beckers, Ralf", user.DisplayName, nameof(user.DisplayName));
        Assert.AreEqual(true, user.IsActive, nameof(user.IsActive));
        Assert.AreEqual(false, user.IsDeleted, nameof(user.IsDeleted));
        Assert.AreEqual("Europe/Berlin", user.TimeZone, nameof(user.TimeZone));
        Assert.AreEqual("en_UK", user.Locale, nameof(user.Locale));
    }

    [TestMethod]
    public async Task TestMethodGetCurrentUserAsync()
    {
        using var jira = new Jira(host, apikey);

        var user = await jira.GetCurrentUserAsync();

        Assert.IsNotNull(user);
        Assert.AreEqual("https://jira.elektrobit.com/rest/api/2/user?username=bs", user.Self?.ToString(), nameof(user.Self));
        Assert.AreEqual("bs", user.Key, nameof(user.Key));
        Assert.AreEqual("bs", user.Name, nameof(user.Name));
        Assert.AreEqual("Ralf.Beckers@elektrobit.com", user.EmailAddress, nameof(user.EmailAddress));
        Assert.AreEqual("https://jira.elektrobit.com/secure/useravatar?size=xsmall&ownerId=bs&avatarId=10209", user.AvatarUrls?.AvatarUrl16?.ToString(), nameof(user.AvatarUrls.AvatarUrl16));
        Assert.AreEqual("https://jira.elektrobit.com/secure/useravatar?size=small&ownerId=bs&avatarId=10209", user.AvatarUrls?.AvatarUrl24?.ToString(), nameof(user.AvatarUrls.AvatarUrl24));
        Assert.AreEqual("https://jira.elektrobit.com/secure/useravatar?size=medium&ownerId=bs&avatarId=10209", user.AvatarUrls?.AvatarUrl32?.ToString(), nameof(user.AvatarUrls.AvatarUrl32));
        Assert.AreEqual("https://jira.elektrobit.com/secure/useravatar?ownerId=bs&avatarId=10209", user.AvatarUrls?.AvatarUrl48?.ToString(), nameof(user.AvatarUrls.AvatarUrl48));
        Assert.AreEqual("Beckers, Ralf", user.DisplayName, nameof(user.DisplayName));
        Assert.AreEqual(true, user.IsActive, nameof(user.IsActive));
        Assert.AreEqual(false, user.IsDeleted, nameof(user.IsDeleted));
        Assert.AreEqual("Europe/Berlin", user.TimeZone, nameof(user.TimeZone));
        Assert.AreEqual("en_UK", user.Locale, nameof(user.Locale));
    }
}

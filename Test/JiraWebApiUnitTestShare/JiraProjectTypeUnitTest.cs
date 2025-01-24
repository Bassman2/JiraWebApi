namespace JiraWebApiUnitTest;

[TestClass]
public class JiraProjectTypeUnitTest : JiraBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetProjectTypesAsync()
    {
        using var jira = new Jira(storeKey, appName);

        var list = await jira.GetProjectTypesAsync();

        var projectTypes = list?.ToList();
        Assert.IsNotNull(projectTypes);

        var projectType = projectTypes.FirstOrDefault();
        Assert.IsNotNull(projectType);
        Assert.AreEqual("software", projectType.Key, nameof(projectType.Key));
        Assert.AreEqual("Software", projectType.FormattedKey, nameof(projectType.FormattedKey));
        Assert.AreEqual("jira.project.type.software.description", projectType.DescriptionI18nKey, nameof(projectType.DescriptionI18nKey));
        Assert.AreEqual("PHN2ZyB3aWR0aD0iNzIiIGhlaWdodD0iNzIiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyI+PGcgZmlsbD0ibm9uZSIgZmlsbC1ydWxlPSJldmVub2RkIj48Y2lyY2xlIGZpbGw9IiNFRDZGMDAiIGN4PSIzNiIgY3k9IjM2IiByPSIzNiIvPjxwYXRoIGQ9Ik0yOS42OCA0OS42MTdhMy4xOTQgMy4xOTQgMCAwMS0yLjI2My0uOTM3TDE3LjExMyAzOC4zNzVhMy44MyAzLjgzIDAgMDEwLTUuNGwxMC4wNC0xMC4wNGEzLjIwOSAzLjIwOSAwIDAxNC41MjggMCAzLjIwNiAzLjIwNiAwIDAxMCA0LjUyOGwtOC4yMTUgOC4yMTMgOC40NzkgOC40OGEzLjIwMSAzLjIwMSAwIDAxLTIuMjY0IDUuNDZNNTAuNjYzIDM3LjQ5NmwuMDE2LjAxNi0uMDE2LS4wMTZ6bS04LjAzNSAxMi4xMmEzLjE5OCAzLjE5OCAwIDAxLTIuMjYyLTUuNDYxbDguNDc3LTguNDgtOC4yMS04LjIxNGEzLjIgMy4yIDAgMDEwLTQuNTI2IDMuMiAzLjIgMCAwMTQuNTIyLjAwMmwxMC4wNCAxMC4wNGEzLjc3NCAzLjc3NCAwIDAxMS4xMiAyLjY5IDMuNzg2IDMuNzg2IDAgMDEtMS4xMiAyLjcwOEw0NC44OSA0OC42OGEzLjE4NiAzLjE4NiAwIDAxLTIuMjYyLjkzN3oiIGZpbGw9IiNGRkYiLz48L2c+PC9zdmc+", projectType.Icon, nameof(projectType.Icon));
        Assert.AreEqual("#ED6F00", projectType.Color, nameof(projectType.Color));
    }

    [TestMethod]
    public async Task TestMethodGetProjectTypeAsync()
    {
        using var jira = new Jira(storeKey, appName);

        var projectType = await jira.GetProjectTypeAsync("software");

        Assert.IsNotNull(projectType);
        Assert.AreEqual("software", projectType.Key, nameof(projectType.Key));
        Assert.AreEqual("Software", projectType.FormattedKey, nameof(projectType.FormattedKey));
        Assert.AreEqual("jira.project.type.software.description", projectType.DescriptionI18nKey, nameof(projectType.DescriptionI18nKey));
        Assert.AreEqual("PHN2ZyB3aWR0aD0iNzIiIGhlaWdodD0iNzIiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyI+PGcgZmlsbD0ibm9uZSIgZmlsbC1ydWxlPSJldmVub2RkIj48Y2lyY2xlIGZpbGw9IiNFRDZGMDAiIGN4PSIzNiIgY3k9IjM2IiByPSIzNiIvPjxwYXRoIGQ9Ik0yOS42OCA0OS42MTdhMy4xOTQgMy4xOTQgMCAwMS0yLjI2My0uOTM3TDE3LjExMyAzOC4zNzVhMy44MyAzLjgzIDAgMDEwLTUuNGwxMC4wNC0xMC4wNGEzLjIwOSAzLjIwOSAwIDAxNC41MjggMCAzLjIwNiAzLjIwNiAwIDAxMCA0LjUyOGwtOC4yMTUgOC4yMTMgOC40NzkgOC40OGEzLjIwMSAzLjIwMSAwIDAxLTIuMjY0IDUuNDZNNTAuNjYzIDM3LjQ5NmwuMDE2LjAxNi0uMDE2LS4wMTZ6bS04LjAzNSAxMi4xMmEzLjE5OCAzLjE5OCAwIDAxLTIuMjYyLTUuNDYxbDguNDc3LTguNDgtOC4yMS04LjIxNGEzLjIgMy4yIDAgMDEwLTQuNTI2IDMuMiAzLjIgMCAwMTQuNTIyLjAwMmwxMC4wNCAxMC4wNGEzLjc3NCAzLjc3NCAwIDAxMS4xMiAyLjY5IDMuNzg2IDMuNzg2IDAgMDEtMS4xMiAyLjcwOEw0NC44OSA0OC42OGEzLjE4NiAzLjE4NiAwIDAxLTIuMjYyLjkzN3oiIGZpbGw9IiNGRkYiLz48L2c+PC9zdmc+", projectType.Icon, nameof(projectType.Icon));
        Assert.AreEqual("#ED6F00", projectType.Color, nameof(projectType.Color));
    }

    [TestMethod]
    public async Task TestMethodGetAccessibleProjectTypeAsync()
    {
        using var jira = new Jira(storeKey, appName);

        var projectType = await jira.GetAccessibleProjectTypeAsync("software");

        Assert.IsNotNull(projectType);
        Assert.AreEqual("software", projectType.Key, nameof(projectType.Key));
        Assert.AreEqual("Software", projectType.FormattedKey, nameof(projectType.FormattedKey));
        Assert.AreEqual("jira.project.type.software.description", projectType.DescriptionI18nKey, nameof(projectType.DescriptionI18nKey));
        Assert.AreEqual("PHN2ZyB3aWR0aD0iNzIiIGhlaWdodD0iNzIiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyI+PGcgZmlsbD0ibm9uZSIgZmlsbC1ydWxlPSJldmVub2RkIj48Y2lyY2xlIGZpbGw9IiNFRDZGMDAiIGN4PSIzNiIgY3k9IjM2IiByPSIzNiIvPjxwYXRoIGQ9Ik0yOS42OCA0OS42MTdhMy4xOTQgMy4xOTQgMCAwMS0yLjI2My0uOTM3TDE3LjExMyAzOC4zNzVhMy44MyAzLjgzIDAgMDEwLTUuNGwxMC4wNC0xMC4wNGEzLjIwOSAzLjIwOSAwIDAxNC41MjggMCAzLjIwNiAzLjIwNiAwIDAxMCA0LjUyOGwtOC4yMTUgOC4yMTMgOC40NzkgOC40OGEzLjIwMSAzLjIwMSAwIDAxLTIuMjY0IDUuNDZNNTAuNjYzIDM3LjQ5NmwuMDE2LjAxNi0uMDE2LS4wMTZ6bS04LjAzNSAxMi4xMmEzLjE5OCAzLjE5OCAwIDAxLTIuMjYyLTUuNDYxbDguNDc3LTguNDgtOC4yMS04LjIxNGEzLjIgMy4yIDAgMDEwLTQuNTI2IDMuMiAzLjIgMCAwMTQuNTIyLjAwMmwxMC4wNCAxMC4wNGEzLjc3NCAzLjc3NCAwIDAxMS4xMiAyLjY5IDMuNzg2IDMuNzg2IDAgMDEtMS4xMiAyLjcwOEw0NC44OSA0OC42OGEzLjE4NiAzLjE4NiAwIDAxLTIuMjYyLjkzN3oiIGZpbGw9IiNGRkYiLz48L2c+PC9zdmc+", projectType.Icon, nameof(projectType.Icon));
        Assert.AreEqual("#ED6F00", projectType.Color, nameof(projectType.Color));
    }
}

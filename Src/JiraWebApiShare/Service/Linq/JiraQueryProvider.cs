//namespace JiraWebApi.Service.Linq;

//internal class JiraQueryProvider: IQueryProvider
//{
//    private readonly JiraService jira;

//    /// <summary>
//    /// Initializes a new instance of the JiraQueryProvider class with a specific jira.
//    /// </summary>
//    internal JiraQueryProvider(JiraService jira)
//    {
//        this.jira = jira;
//    }

//    public IQueryable<T> CreateQuery<T>(Expression expression)
//    {
//        return new JiraQueryable<T>(this, expression);
//    }

//    public IQueryable CreateQuery(Expression expression)
//    {
//        throw new NotImplementedException();
//    }

//    [RequiresUnreferencedCode("Calls JiraWebApi.Service.Linq.JiraQueryProvider.Execute(Expression, Boolean)")]
//    [RequiresDynamicCode("Calls JiraWebApi.Service.Linq.JiraQueryProvider.Execute(Expression, Boolean)")]
//    public T? Execute<T>(Expression expression)
//    {
//        bool isEnumerable = (typeof(T).Name == "IEnumerable`1");

//        return (T?)this.Execute(expression, isEnumerable);
//    }

//    [RequiresUnreferencedCode("Calls JiraWebApi.Service.Linq.JiraQueryProvider.Execute(Expression, Boolean)")]
//    [RequiresDynamicCode("Calls JiraWebApi.Service.Linq.JiraQueryProvider.Execute(Expression, Boolean)")]
//    public object? Execute(Expression expression)
//    {
//        return Execute(expression, true);
//    }

//    [RequiresUnreferencedCode("Calls System.Linq.Queryable.AsQueryable<TElement>()")]
//    [RequiresDynamicCode("Calls System.Linq.Queryable.AsQueryable<TElement>()")]
//    private object? Execute(Expression expression, bool isEnumerable)
//    {
//        IEnumerable<Field?>? fields = this.jira.GetCachedFieldsAsync().Result;
//        var visitor = new JqlExpressionVisitor();
//        visitor.ParseExpression(expression, fields!);

//        IQueryable<Issue>? issues = jira.GetIssuesFromJql(visitor.Jql, visitor.StartAt, visitor.MaxResults)?.AsQueryable();

//        return JqlExpressionVisitor.ModifyExpression(expression, issues, isEnumerable);
//    }
//}

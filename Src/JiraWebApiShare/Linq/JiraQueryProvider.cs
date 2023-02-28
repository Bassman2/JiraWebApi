using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace JiraWebApi.Linq
{
    internal class JiraQueryProvider: IQueryProvider
    {
        private readonly Jira jira;

        /// <summary>
        /// Initializes a new instance of the JiraQueryProvider class with a specific jira.
        /// </summary>
        internal JiraQueryProvider(Jira jira)
        {
            this.jira = jira;
        }

        public IQueryable<T> CreateQuery<T>(Expression expression)
        {
            return new JiraQueryable<T>(this, expression);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public T Execute<T>(Expression expression)
        {
            bool isEnumerable = (typeof(T).Name == "IEnumerable`1");

            return (T)this.Execute(expression, isEnumerable);
        }

        public object Execute(Expression expression)
        {
            return Execute(expression, true);
        }

        private object Execute(Expression expression, bool isEnumerable)
        {
            IEnumerable<Field> fields = null; // this.jira.GetCachedFieldsAsync().Result;
            JqlExpressionVisitor visitor = new JqlExpressionVisitor();
            visitor.ParseExpression(expression, fields);

            IQueryable<Issue> issues = this.jira.GetIssuesFromJql(visitor.Jql, visitor.StartAt, visitor.MaxResults).AsQueryable();

            return JqlExpressionVisitor.ModifyExpression(expression, issues, isEnumerable);
        }
    }
}

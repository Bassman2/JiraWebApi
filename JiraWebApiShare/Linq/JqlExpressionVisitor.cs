using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Globalization;

namespace JiraWebApi.Linq
{
    internal class JqlExpressionVisitor: ExpressionVisitor 
    {
        private IEnumerable<Field> fields;
        private StringBuilder jql;
        private static readonly CultureInfo cultureInfo = new CultureInfo("en-US");
        private bool isFirst = true;

        /// <summary>
        /// Initializes a new instance of the JqlExpressionVisitor class with specific fields.
        /// </summary>
        public JqlExpressionVisitor()
        {
            this.jql = new StringBuilder();
        }

        public string Jql 
        { 
            get { return this.jql.ToString(); } 
        }

        public int? StartAt { get; private set; }

        public int? MaxResults { get; private set; }

        public void ParseExpression(Expression expression, IEnumerable<Field> fields)
        {
            this.fields = fields;
            //expression = new SubtreeEvaluator(new Nominator().Nominate(expression)).Evaluate(expression);
                
            Visit(expression);
        }

        public static object ModifyExpression(Expression expression, IQueryable<Issue> issues, bool isEnumerable)
        {
            if (isEnumerable)
            {
                return issues;
            }
            else
            {
                var treeCopier = new ExpressionTreeModifier(issues);
                Expression newExpressionTree = treeCopier.Visit(expression);

                return issues.Provider.Execute(newExpressionTree);
            }
        }
                
        #region expression modifier

        /// <summary>
        /// Evaluates and replaces sub-trees when first candidate is reached (top-down)
        /// </summary>
        private class SubtreeEvaluator : ExpressionVisitor
        {
            private HashSet<Expression> candidates;

            public SubtreeEvaluator(HashSet<Expression> candidates)
            {
                this.candidates = candidates;
            }

            public Expression Evaluate(Expression expression)
            {
                return this.Visit(expression);
            }

            public override Expression Visit(Expression expression)
            {
                if (expression == null)
                {
                    return null;
                }

                JsonTrace.WriteLine("SubtreeEvaluator.Visit " + expression.ToString());

                if (this.candidates.Contains(expression))
                {
                    if (expression.NodeType == ExpressionType.Constant)
                    {
                        return expression;
                    }
                    LambdaExpression lambda = Expression.Lambda(expression);
                    MethodCallExpression methodCallExpression = lambda.Body as MethodCallExpression; 
                    if (methodCallExpression != null)
                    {
                        IEnumerable<JqlFunctionAttribute> attrs = (IEnumerable<JqlFunctionAttribute>)methodCallExpression.Method.GetCustomAttributes(typeof(JqlFunctionAttribute));
                        if (attrs != null && attrs.Count() > 0)
                        {
                            JqlFunctionAttribute attr = attrs.First();
                            return base.Visit(expression);
                        }
                    }
                    try
                    {
                        Delegate fn = lambda.Compile();
                        Expression exp = Expression.Constant(fn.DynamicInvoke(null), expression.Type);
                        JsonTrace.WriteLine("SubtreeEvaluator.Compile: " + expression + " to " + exp);
                        return exp;
                    }
                    catch (NotSupportedException)
                    { }
                }
                return base.Visit(expression);
            }
        }

        /// <summary>
        /// Performs bottom-up analysis to determine which nodes can possibly
        /// be part of an evaluated sub-tree.
        /// </summary>
        private class Nominator : ExpressionVisitor
        {
            private HashSet<Expression> candidates;
            private bool cannotBeEvaluated;

            public Nominator()
            { }

            public HashSet<Expression> Nominate(Expression expression)
            {
                this.candidates = new HashSet<Expression>();
                this.Visit(expression);
                return this.candidates;
            }

            public override Expression Visit(Expression expression)
            {
                if (expression != null)
                {
                    bool saveCannotBeEvaluated = this.cannotBeEvaluated;
                    this.cannotBeEvaluated = false;
                    base.Visit(expression);
                    JsonTrace.WriteLine("Nominator.Visit " + expression.ToString());
                    if (!this.cannotBeEvaluated)
                    {
                        if (expression.NodeType != ExpressionType.Parameter)
                        {
                            JsonTrace.WriteLine("Nominator.Add (" + expression.NodeType + ") " + expression.ToString());
                            this.candidates.Add(expression);
                        }
                        else
                        {
                            this.cannotBeEvaluated = true;
                        }
                    }
                    this.cannotBeEvaluated |= saveCannotBeEvaluated;
                }
                return expression;
            }
        }

        private class ExpressionTreeModifier : ExpressionVisitor
        {
            private readonly IQueryable<Issue> queryableIssues;

            /// <summary>
            /// Initializes a new instance of the ExpressionTreeModifier class with specific queryableIssues.
            /// </summary>
            public ExpressionTreeModifier(IQueryable<Issue> queryableIssues)
            {
                this.queryableIssues = queryableIssues;
            }

            protected override Expression VisitConstant(ConstantExpression node)
            {
                if (node.Type == typeof(JiraQueryable<Issue>))
                {
                    return Expression.Constant(this.queryableIssues);
                }
                else
                {
                    return node;
                }
            }

            protected override Expression VisitMethodCall(MethodCallExpression node)
            {
                if (node.Method.Name == "Where"
                    || node.Method.Name == "Take"
                    || node.Method.Name == "OrderBy"
                    || node.Method.Name == "OrderByDescending"
                    || node.Method.Name == "ThenBy"
                    || node.Method.Name == "ThenByDescending")
                {
                    return Expression.Constant(this.queryableIssues);
                }

                return base.VisitMethodCall(node);
            }

        }

        #endregion

        #region Help

        private void ResetNumerate()
        {
            this.isFirst = true;
        }

        private void Numerate()
        {
            if (!this.isFirst)
            {
                this.jql.Append(", ");
            }
            this.isFirst = false;
        }
        
        #endregion

        private static Expression StripQuotes(Expression node)
        {
            while (node.NodeType == ExpressionType.Quote)
            {
                node = ((UnaryExpression)node).Operand;
            }
            return node;
        }

        private JqlFieldAttribute GetFieldInfo(Expression expression)
        {
            switch (expression.NodeType)
            {
            // get JqlFieldAttribute from default field
            case ExpressionType.MemberAccess:
                {
                    MemberExpression memberExpression = expression as MemberExpression;
                    PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;
                    if (propertyInfo != null)
                    {
                        return propertyInfo.GetCustomAttributes(typeof(JqlFieldAttribute), true).Select(o => o as JqlFieldAttribute).First();
                    }
                    throw new Exception("Unknown error!");
                }
            // get JqlFieldAttribute from a converted field
            case ExpressionType.Convert:
                {
                    UnaryExpression unaryExpression = expression as UnaryExpression;
                    MemberExpression memberExpression = unaryExpression.Operand as MemberExpression;
                    if (memberExpression != null)
                    {
                        PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;
                        if (propertyInfo != null)
                        {
                            return propertyInfo.GetCustomAttributes(typeof(JqlFieldAttribute), true).Select(o => o as JqlFieldAttribute).First();
                        }
                    }
                    throw new Exception("Unknown error!");
                }
            // create JqlFieldAttribute from a custom field
            case ExpressionType.Call:
                {
                    string name = "unknown";
                    MethodCallExpression methodCallExpression = expression as MethodCallExpression;
                    ConstantExpression constantExpression = methodCallExpression.Arguments[0] as ConstantExpression;
                    if (constantExpression != null)
                    {
                        name = "\"" + constantExpression.Value as string + "\"";
                    }
                    return new JqlFieldAttribute(name, JqlFieldCompare.Contains);
                }
            }

            throw new Exception("Unknown error!");
        }

        //protected override Expression VisitUnary(UnaryExpression node)
        //{
        //    if (node.NodeType == ExpressionType.Quote)
        //    {
        //        return this.Visit(node.Operand);
        //    }
        //    throw new NotSupportedException(string.Format("The unary operator '{0}' is not supported", node.NodeType));
        //}

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            ResetNumerate();
            switch (node.Method.Name)
            {
            case "Where":
                LambdaExpression lambda = (LambdaExpression)StripQuotes(node.Arguments[1]);
                this.Visit(lambda.Body);
                return node;
            case "IsEmpty":
                Visit(node.Object);
                this.jql.Append(" is empty");
                return node;
            case "IsNotEmpty":
                Visit(node.Object);
                this.jql.Append(" is not empty");
                return node;
            case "IsNull":
                Visit(node.Object);
                this.jql.Append(" is null");
                return node;
            case "IsNotNull":
                Visit(node.Object);
                this.jql.Append(" is not null");
                return node;
            case "In":
                if (node.Object != null)
                {
                    Visit(node.Object);
                    this.jql.Append(" in (");
                    foreach (Expression expression in node.Arguments)
                    {
                        Visit(expression);
                    }
                    this.jql.Append(")");
                }
                else
                {
                    Visit(node.Arguments[0]);
                    this.jql.Append(" in (");
                    Visit(node.Arguments[1]);
                    this.jql.Append(")");
                }
                return node;
            case "NotIn":
                if (node.Object != null)
                {
                    Visit(node.Object);
                    this.jql.Append(" not in (");
                    foreach (Expression expression in node.Arguments)
                    {
                        Visit(expression);
                    }
                    this.jql.Append(")");
                }
                else
                {
                    Visit(node.Arguments[0]);
                    this.jql.Append(" not in (");
                    Visit(node.Arguments[1]);
                    this.jql.Append(")");
                }
                return node;
            case "WasIn":
                Visit(node.Object);
                this.jql.Append(" was in (");
                foreach (Expression expression in node.Arguments)
                {
                    Visit(expression);
                }
                this.jql.Append(")");
                return node;
            case "WasNotIn":
                Visit(node.Object);
                this.jql.Append(" was not in (");
                foreach (Expression expression in node.Arguments)
                {
                    Visit(expression);
                }
                this.jql.Append(")");
                return node;
            case "Was":
                Visit(node.Object);
                this.jql.Append(" was ");
                //if (node.Arguments != null && node.Arguments.Count == 1)
                {
                    Visit(node.Arguments.First());
                }
                return node;
            case "WasNot":
                Visit(node.Object);
                this.jql.Append(" was not ");
                //if (node.Arguments != null && node.Arguments.Count == 1)
                {
                    Visit(node.Arguments.First());
                }
                return node;
            case "Changed":
                Visit(node.Object);
                this.jql.Append(" changed");
                if (node.Arguments != null && node.Arguments.Count > 0)
                {
                    this.jql.Append(" ");
                    Visit(node.Arguments[0]);
                }
                return node;
            case "During":
                Visit(node.Arguments[0]);
                this.jql.Append(" during (");
                Visit(node.Arguments[1]);
                Visit(node.Arguments[2]);
                this.jql.Append(")");
                return node;
            case "After":
                Visit(node.Arguments[0]);
                this.jql.Append(" after ");
                Visit(node.Arguments[1]);
                return node;
            case "Before":
                Visit(node.Arguments[0]);
                this.jql.Append(" before ");
                Visit(node.Arguments[1]);
                return node;
            case "By":
                Visit(node.Arguments[0]);
                this.jql.Append(" by ");
                Visit(node.Arguments[1]);
                return node;
            case "On":
                Visit(node.Arguments[0]);
                this.jql.Append(" on ");
                Visit(node.Arguments[1]);
                return node;
            case "From":
                Visit(node.Arguments[0]);
                this.jql.Append(" from ");
                Visit(node.Arguments[1]);
                return node;
            case "To":
                Visit(node.Arguments[0]);
                this.jql.Append(" to ");
                Visit(node.Arguments[1]);
                return node;
            case "OrderBy":
                Visit(node.Arguments[0]);
                this.jql.Append(" order by ");
                Visit(node.Arguments[1]);
                return node;
            case "OrderByDescending":
                Visit(node.Arguments[0]);
                this.jql.Append(" order by ");
                Visit(node.Arguments[1]);
                this.jql.Append(" desc");
                return node;
            case "ThenBy":
                Visit(node.Arguments[0]);
                this.jql.Append(", ");
                Visit(node.Arguments[1]);
                return node;
            case "ThenByDescending":
                Visit(node.Arguments[0]);
                this.jql.Append(", ");
                Visit(node.Arguments[1]);
                this.jql.Append(" desc");
                return node;
            case "Skip":
                this.StartAt = (int)((ConstantExpression)node.Arguments[1]).Value;
                Visit(node.Arguments[0]);
                return node;
            case "Take":
                this.MaxResults = (int)((ConstantExpression)node.Arguments[1]).Value;
                Visit(node.Arguments[0]);
                return node;

            case "MembersOf":
                this.jql.Append("membersOf(");
                if (node.Arguments != null && node.Arguments.Count > 0)
                {
                    Visit(node.Arguments[0]);
                }
                this.jql.Append(")");
                return node;
            case "CurrentUser":
                this.jql.Append("currentUser()");
                return node;

            case "ReleasedVersions":
                this.jql.Append("releasedVersions()");
                return node;
            case "LatestReleasedVersion":
                this.jql.Append("latestReleasedVersion()");
                return node;
            case "UnreleasedVersions":
                this.jql.Append("unreleasedVersions()");
                return node;
            case "EarliestUnreleasedVersion":
                this.jql.Append("earliestUnreleasedVersion()");
                return node;

            case "ComponentsLeadByUser":
                this.jql.Append("componentsLeadByUser()");
                return node;

            case "CurrentLogin":
                this.jql.Append("currentLogin()");
                return node;
            case "LastLogin":
                this.jql.Append("lastLogin()");
                return node;
            case "Now":
                this.jql.Append("now()");
                return node;
            case "StartOfDay":
                this.jql.Append("startOfDay()");
                return node;
            case "StartOfWeek":
                this.jql.Append("startOfWeek()");
                return node;
            case "StartOfMonth":
                this.jql.Append("startOfMonth()");
                return node;
            case "StartOfYear":
                this.jql.Append("startOfYear()");
                return node;
            case "EndOfDay":
                this.jql.Append("endOfDay()");
                return node;
            case "EndOfWeek":
                this.jql.Append("endOfWeek()");
                return node;
            case "EndOfMonth":
                this.jql.Append("endOfMonth()");
                return node;
            case "EndOfYear":
                this.jql.Append("endOfYear()");
                return node;

            case "IssueHistory":
                this.jql.Append("issueHistory()");
                return node;
            case "LinkedIssues":
                this.jql.Append("linkedIssues()");
                return node;
            case "VotedIssues":
                this.jql.Append("votedIssues()");
                return node;
            case "WatchedIssues":
                this.jql.Append("watchedIssues()");
                return node;

            case "ProjectsLeadByUser":
                this.jql.Append("projectsLeadByUser()");
                return node;
            case "ProjectsWhereUserHasPermission":
                this.jql.Append("projectsWhereUserHasPermission()");
                return node;
            case "ProjectsWhereUserHasRole":
                this.jql.Append("projectsWhereUserHasRole()");
                return node;

            case "OpenSprints":
                this.jql.Append("openSprints()");
                return node;
            case "ClosedSprints":
                this.jql.Append("closedSprints()");
                return node;
            }
            throw new NotSupportedException(string.Format("The method '{0}' is not supported", node.Method.Name));
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            ConstantExpression rightExpression = null;
            JqlFieldAttribute field = null;

            switch (node.NodeType)
            {
            case ExpressionType.And:
            case ExpressionType.AndAlso:
                this.jql.Append("(");
                this.Visit(node.Left);
                this.jql.Append(" AND ");
                this.Visit(node.Right);
                this.jql.Append(")");
                break;
            case ExpressionType.Or:
            case ExpressionType.OrElse:
                this.jql.Append("(");
                this.Visit(node.Left);
                this.jql.Append(" OR");
                this.Visit(node.Right);
                this.jql.Append(")");
                break;
            case ExpressionType.Equal:
                field = GetFieldInfo(node.Left);
                this.jql.Append(field.Name);
                if (node.Right.NodeType == ExpressionType.Convert)
                {
                    UnaryExpression unaryExpression = node.Right as UnaryExpression;
                    rightExpression = unaryExpression.Operand as ConstantExpression;
                }
                else
                {
                    rightExpression = node.Right as ConstantExpression;
                }
                if (rightExpression != null && rightExpression.Value == null && rightExpression.Type == typeof(DateTime?))
                {
                    this.jql.Append(" is empty");
                }
                else if (rightExpression != null && rightExpression.Value == null)
                {
                    this.jql.Append(" is null");
                }
                else if (rightExpression != null && rightExpression.Value.GetType() == typeof(string) && ((string)rightExpression.Value) == "")
                {
                    this.jql.Append(" is empty");
                }
                else
                {
                    if ((field.Compare & JqlFieldCompare.Contains) == JqlFieldCompare.Contains)
                    {
                        this.jql.Append(" ~ ");
                    }
                    else
                    {
                        this.jql.Append(" = ");
                    }
                    this.Visit(node.Right);
                }
                break;
            case ExpressionType.NotEqual:
                field = GetFieldInfo(node.Left);
                this.jql.Append(field.Name);
                if (node.Right.NodeType == ExpressionType.Convert)
                {
                    UnaryExpression unaryExpression = node.Right as UnaryExpression;
                    rightExpression = unaryExpression.Operand as ConstantExpression;
                }
                else
                {
                    rightExpression = node.Right as ConstantExpression;
                }
                if (rightExpression != null && rightExpression.Value == null && rightExpression.Type == typeof(DateTime?))
                {
                    this.jql.Append(" is not empty");
                }
                else if (rightExpression != null && rightExpression.Value == null)
                {
                    this.jql.Append(" is not null");
                }
                else if (rightExpression != null && rightExpression.Value.GetType() == typeof(string) && ((string)rightExpression.Value) == "")
                {
                    this.jql.Append(" is not empty");
                }
                else
                {
                    if ((field.Compare & JqlFieldCompare.Contains) == JqlFieldCompare.Contains)
                    {
                        this.jql.Append(" !~ ");
                    }
                    else
                    {
                        this.jql.Append(" != ");
                    }
                    this.Visit(node.Right);
                }
                break;
            case ExpressionType.LessThan:
                this.Visit(node.Left);
                this.jql.Append(" < ");
                this.Visit(node.Right);
                break;
            case ExpressionType.LessThanOrEqual:
                this.Visit(node.Left);
                this.jql.Append(" <= ");
                this.Visit(node.Right);
                break;
            case ExpressionType.GreaterThan:
                this.Visit(node.Left);
                this.jql.Append(" > ");
                this.Visit(node.Right);
                break;
            case ExpressionType.GreaterThanOrEqual:
                this.Visit(node.Left);
                this.jql.Append(" >= ");
                this.Visit(node.Right);
                break;
            default:
                throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported", node.NodeType));
            }
            return node;
        }

        protected override Expression VisitNew(NewExpression node)
        {
            var args = node.Arguments.Select(e => (e as ConstantExpression).Value);
            Visit(Expression.Constant(node.Constructor.Invoke(args.ToArray())));
            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            IQueryable q = node.Value as IQueryable;
            if (node.Value is IQueryable)
            {
                // do nothing
            }
            else if (node.Value == null)
            {
            }
            else
            {
                if (node.Value.GetType().IsArray)
                {
                    if (node.Value.GetType() == typeof(DateTime[]))
                    {
                        foreach (DateTime item in (DateTime[])node.Value)
                        {
                            Numerate();
                            this.jql.Append("\"");
                            this.jql.Append(item.ToString("yyyy/MM/dd", cultureInfo));
                            this.jql.Append("\"");
                        }
                    }
                    else if (node.Value.GetType() == typeof(int[]))
                    {
                        foreach (int item in (int[])node.Value)
                        {
                            Numerate();
                            this.jql.Append(item);
                        }
                    }
                    else
                    {
                        foreach (object item in (object[])node.Value)
                        {
                            Numerate();
                            this.jql.Append("\"");
                            this.jql.Append(item);
                            this.jql.Append("\"");
                        }
                    }
                }
                else
                {
                    Numerate();
                    switch (Type.GetTypeCode(node.Value.GetType()))
                    {
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                        this.jql.Append(node.Value.ToString());
                        break;
                    case TypeCode.DateTime:
                        this.jql.Append("\"");
                        this.jql.Append(((DateTime)node.Value).ToString("yyyy/MM/dd", cultureInfo));
                        this.jql.Append("\"");
                        break;
                    default:
                        this.jql.Append("\"");
                        this.jql.Append(node.Value);
                        this.jql.Append("\"");
                        break;
                    }
                }

            }
            return node;
        }



        protected override Expression VisitMember(MemberExpression node)
        {
            PropertyInfo propertyInfo = node.Member as PropertyInfo;
            if (propertyInfo != null)
            {
                // get property with JqlFieldAttribute
                var attributes = propertyInfo.GetCustomAttributes(typeof(JqlFieldAttribute), true);
                JqlFieldAttribute field = attributes[0] as JqlFieldAttribute;
                this.jql.Append(field.Name);
            }
            else
            {
                LambdaExpression lambda = Expression.Lambda(node);
                Delegate fn = lambda.Compile();
                Expression expression = Expression.Constant(fn.DynamicInvoke(null));
                Visit(expression);
            }

            return node;
        }
       
    }
}

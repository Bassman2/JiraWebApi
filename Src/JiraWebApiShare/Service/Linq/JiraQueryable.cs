using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace JiraWebApi.Linq
{
    internal class JiraQueryable<T>: IOrderedQueryable<T>, IQueryable<T>
    {
        /// <summary>
        /// Initializes a new instance of the JiraQueryable class with specific provider and expression.
        /// </summary>
        internal JiraQueryable(JiraQueryProvider provider, Expression? expression = null)
        {
            this.Provider = provider;
            this.Expression = expression ?? Expression.Constant(this);
        }
        
        public Expression Expression { get; private set; }

        public IQueryProvider Provider { get; private set; }

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public IEnumerator<T> GetEnumerator()
        {
            //return ((IEnumerable<T>)this.Provider.Execute(this.Expression)).GetEnumerator();
            return this.Provider.Execute<IEnumerable<T>>(this.Expression).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            //return ((IEnumerable)this.Provider.Execute(this.Expression)).GetEnumerator();
            return this.Provider.Execute<IEnumerable>(this.Expression).GetEnumerator();
        }
    }
}

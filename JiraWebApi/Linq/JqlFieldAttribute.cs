using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi.Linq
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class JqlFieldAttribute : Attribute
    {
        public string Name { get; set; }

        public JqlFieldCompare Compare { get; set; }

        /// <summary>
        /// Initializes a new instance of the JqlFieldAttribute class.
        /// </summary>
        public JqlFieldAttribute(string name, JqlFieldCompare compare)
        {
            this.Name = name;
            this.Compare = compare;
        }
    }
}

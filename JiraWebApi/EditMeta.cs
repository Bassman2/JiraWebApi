using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi
{
    /// <summary>
    /// Representation of meta information for issue editing.
    /// </summary>
    public sealed class EditMeta
    {
        /// <summary>
        /// Initializes a new instance of the EditMeta class.
        /// </summary>
        private EditMeta()
        { }

        /// <summary>
        /// Available fields for issue editing.
        /// </summary>
        [JsonProperty("fields")]
        public Fields Fields { get; private set; }
    }
}

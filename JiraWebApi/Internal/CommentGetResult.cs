using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi.Internal
{
    internal class CommentGetResult
    {
        [JsonProperty(PropertyName = "startAt")]
        public int StartAt { get; set; }

        [JsonProperty(PropertyName = "maxResults")]
        public int MaxResults { get; set; }

        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "comments")]
        public IEnumerable<Comment> Comments { get; set; }
    }
}

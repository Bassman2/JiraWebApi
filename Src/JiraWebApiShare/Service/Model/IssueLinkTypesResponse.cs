using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JiraWebApi.Internal
{
    internal class IssueLinkTypesRespnse
    {
        [JsonPropertyName("issueLinkTypes")]
        public IEnumerable<IssueLinkType> IssueLinkTypes { get; set; }
    }
}

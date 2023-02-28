using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JiraWebApi.Internal
{
    internal static class MessageEnsure
    {
        internal static void EnsureSuccess(this HttpResponseMessage response)
        {
            JsonTrace.WriteResponse(response);
            if (!response.IsSuccessStatusCode)
            {
                throw new JiraException(response);
            }
        }
    }
}

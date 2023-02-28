using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;

namespace JiraWebApi
{
    internal static class JsonTrace 
    {
        private static TraceSwitch traceSwitch = new TraceSwitch("JiraWebApi", "JiraWebApi Trace Switch", "Info");

        
        public static void WriteRequest(Jira jira, object value)
        {
            if (traceSwitch.TraceInfo)
            {
                //MethodBase mb = new StackTrace().GetFrame(1).GetMethod();
                //string function = mb.Name;
                //if (function == "MoveNext")
                //{
                //    function = mb.DeclaringType.ToString().Split(new char[] { '<', '>' })[1];
                //}
                //StringWriter writer = new StringWriter();
                //JsonSerializer ser = new JsonSerializer() { Context = new StreamingContext(StreamingContextStates.All, jira) };
                //ser.Serialize(writer, value);
                //string json = writer.ToString();
                //writer.Close();
                //Debug.WriteLine(string.Format("{0} request: {1}", function, json));
            }
        }

        
        public static void WriteResponse(HttpResponseMessage response)
        {
            if (traceSwitch.TraceInfo)
            {
                MethodBase mb = new StackTrace().GetFrame(2).GetMethod();
                string function = mb.Name;
                if (function == "MoveNext")
                {
                    function = mb.DeclaringType.ToString().Split(new char[] { '<' , '>' })[1];
                }
                string msg = response.Content.ReadAsStringAsync().Result;
                Debug.WriteLine(string.Format("{0} response: {1}", function, msg));
            }
        }
        
        public static void WriteLine(string value)
        {
            Debug.WriteLineIf(traceSwitch.TraceInfo, value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Faircode.Platform.Remoting
{
    public sealed class ServiceContext
    {
        public string Route { get; private set; }
        public IDictionary<string, string> Header { get; private set; }
        public object Body { get; set; }
        public ServiceContext(string route)
        {
            if (string.IsNullOrWhiteSpace(route))
            {
                throw new ArgumentNullException(nameof(route));
            }
            Route = route;
            Header = new Dictionary<string, string>();
        }

        public void AddHeader(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException($"{nameof(key)} can't be Null or empty");
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{nameof(value)} can't be Null or empty");
            }

            if (Header.ContainsKey(key))
            {
                throw new ArgumentException("Given key already found in the header");
            }

            Header.Add(key, value);
        }

        public override string ToString()
        {
            StringBuilder headerStr = new StringBuilder();

            if (Header != null)
            {
                foreach (KeyValuePair<string, string> keyValuePair in Header)
                {
                    headerStr.Append($"Key: {keyValuePair.Key} , Value {keyValuePair.Value} {Environment.NewLine}");
                }
            }

            string bodystr = Body == null ? string.Empty : JsonConvert.SerializeObject(Body);
            return $"Route: {Route} {Environment.NewLine} Headers :{headerStr.ToString()} {Environment.NewLine}, Body {bodystr}";
        }
    }
}

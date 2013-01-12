using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Utility
{
    public static class HttpHelpers
    {
        public static Dictionary<string, string> ParseQueryString(string query)
        {
            // Removal optional ordinal ?
            if (query.StartsWith("?"))
            {
                query = query.Substring(1);
            }

            Dictionary<string, string> parsed = new Dictionary<string, string>();
            string[] parts = query.Split('&');

            foreach (string part in parts)
            {
                string[] components = part.Split('=');

                if (components.Length > 1)
                {
                    parsed.Add(components[0], components[1]);
                }
            }

            return parsed;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Splitwise.Utility
{
    public static class HttpHelpers
    {
        public static Dictionary<string, string> ParseQueryString(string query)
        {
            // Do a non-greedy regex replace of everything up-to and including
            // the '?' with the empty string.
            query = Regex.Replace(query, @"^.*?\?", "");

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

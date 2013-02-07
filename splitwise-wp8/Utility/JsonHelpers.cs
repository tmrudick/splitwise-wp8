using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Utility
{
    public static class JsonHelpers
    {
        public static bool AddIfNotDefault<T>(this JObject obj, string property, T value) {
            if (EqualityComparer<T>.Default.Equals(value, default(T))) {
                return false;
            }

            obj[property] = JToken.FromObject(value);

            return true;
        }
    }
}

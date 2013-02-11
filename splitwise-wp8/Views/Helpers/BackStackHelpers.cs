using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Views.Helpers
{
    internal static class BackStackHelpers
    {
        internal static bool ShouldHideFromBackStack(IDictionary<string, string> queryParams)
        {
            bool hideFromBackStack = false;

            if (queryParams.ContainsKey("HideFromBackStack"))
            {
                string value = queryParams["HideFromBackStack"].ToLowerInvariant();
                hideFromBackStack = (value == "true");
            }

            return hideFromBackStack;
        }
    }
}

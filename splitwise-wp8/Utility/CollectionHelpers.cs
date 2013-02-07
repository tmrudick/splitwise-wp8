using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Utility
{

    /// <summary>
    /// Extension methods to make working with collections simpler.
    /// </summary>
    public static class CollectionHelpers
    {
        /// <summary>
        /// Add multiple elements to an ObservrableCollection, firing the add events for each
        /// in order.
        /// </summary>
        /// <param name="collection">The collection to add elements to.</param>
        /// <param name="range">The elements to add.</param>
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> range)
        {
            foreach (T element in range)
            {
                collection.Add(element);
            }
        }

        /// <summary>
        /// Convert a non-generic IList to a generic IEnumerable&lt;T&gt;.
        /// </summary>
        /// <typeparam name="T">The type to cast each element to.</typeparam>
        /// <param name="list">The list to transform into a new IEnumerable.</param>
        /// <returns>A new IEnumerable of type T.</returns>
        public static IEnumerable<T> ToIEnumerable<T>(this IList list)
        {
            foreach (T element in list)
            {
                yield return element;
            }
        }
    }
}

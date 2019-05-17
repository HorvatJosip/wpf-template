using System.Collections.Generic;
using System.Linq;

namespace Template.Core
{
    /// <summary>
    /// Extensions for collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Checks if collection is null or has no elements.
        /// </summary>
        /// <typeparam name="T">Type used in collection.</typeparam>
        /// <param name="collection">Collection to check.</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
            => collection == null || collection.Count() == 0;

        /// <summary>
        /// Picks a random element from the collection
        /// </summary>
        /// <typeparam name="T">Type used in collection.</typeparam>
        /// <param name="collection">Collection from which the element will be picked.</param>
        /// <returns></returns>
        public static T RandomElement<T>(this IEnumerable<T> collection)
            => collection.IsNullOrEmpty() 
                ? default(T) 
                : collection.ElementAt(Utils.Rng.Next(collection.Count()));
    }
}

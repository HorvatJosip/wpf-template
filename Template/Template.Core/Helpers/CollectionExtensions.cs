using System;
using System.Collections;
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

        /// <summary>
        /// Finds index of item that matches the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type used in collection.</typeparam>
        /// <param name="collection">Collection to test.</param>
        /// <param name="predicate">Test that is used for finding the index.</param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> collection, Predicate<T> predicate)
        {
            for (int i = 0; i < collection.Count(); i++)
                if (predicate(collection.ElementAt(i)))
                    return i;

            return -1;
        }

        /// <summary>
        /// If a record at the given key exists, it updates it, otherwise,
        /// a new record is added.
        /// </summary>
        /// <param name="dictionary">Dictionary to update.</param>
        /// <param name="key">Key at which the value should be placed.</param>
        /// <param name="value">Value to add or update.</param>
        public static void AddOrUpdate(this IDictionary dictionary, object key, object value)
        {
            if (dictionary.Contains(key))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);
        }
    }
}

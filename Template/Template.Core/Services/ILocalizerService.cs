namespace Template.Core
{
    public interface ILocalizerService
    {
        /// <summary>
        /// Gets a localized string by key.
        /// </summary>
        /// <param name="key">Key used to get a localized string.</param>
        /// <returns></returns>
        string this[string key] { get; }

        /// <summary>
        /// Gets a localized string by key from specific dictionary.
        /// </summary>
        /// <param name="dictionary">Name of the dictionary to use 
        /// (can be partial, it is checked against the dictionary's source property).</param>
        /// <param name="key">Key used to get a localized string.</param>
        /// <returns></returns>
        string this[string dictionary, string key] { get; }

        /// <summary>
        /// Gets a localized string from an array at a specific index by key.
        /// </summary>
        /// <param name="key">Key of the array.</param>
        /// <param name="index">Index of the string in the array.</param>
        /// <returns></returns>
        string this[string key, int index] { get; }

        /// <summary>
        /// Gets a localized string from an array at a specific index
        /// by key from specific dictionary.
        /// </summary>
        /// <param name="dictionary">Name of the dictionary to use 
        /// (can be partial, it is checked against the dictionary's source property).</param>
        /// <param name="key">Key of the array.</param>
        /// <param name="index">Index of the string in the array.</param>
        /// <returns></returns>
        string this[string dictionaryName, string key, int index] { get; }

        /// <summary>
        /// Tries to change culture to the specified one.
        /// </summary>
        /// <param name="culture">Culture identifier (e.g. en-US).</param>
        /// <returns></returns>
        bool ChangeLanguage(string culture);
    }
}

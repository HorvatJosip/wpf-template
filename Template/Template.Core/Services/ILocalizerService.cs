namespace Template.Core
{
    public interface ILocalizerService
    {
        /// <summary>
        /// Gets a localized string by key.
        /// </summary>
        /// <param name="key">Key used to get a localized string.</param>
        /// <returns></returns>
        string Get(string key);

        /// <summary>
        /// Gets a localized string by key from specific dictionary.
        /// </summary>
        /// <param name="dictionary">Name of the dictionary to use 
        /// (can be partial, it is checked against the dictionary's source property).</param>
        /// <param name="key">Key used to get a localized string.</param>
        /// <returns></returns>
        string Get(string dictionary, string key);

        /// <summary>
        /// Tries to change culture to the specified one.
        /// </summary>
        /// <param name="culture">Culture identifier (e.g. en-US).</param>
        /// <returns></returns>
        bool ChangeLanguage(string culture);
    }
}

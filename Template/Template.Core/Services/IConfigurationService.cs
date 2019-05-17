namespace Template.Core
{
    /// <summary>
    /// Used for working with configuration.
    /// </summary>
    public interface IConfigurationService
    {
        /// <summary>
        /// Gets a string from the configuration.
        /// </summary>
        /// <param name="key">Key used for getting a string from configuration.</param>
        /// <returns></returns>
        string Get(string key);

        /// <summary>
        /// Gets a value from the configuration.
        /// </summary>
        /// <typeparam name="T">Type of value to get.</typeparam>
        /// <param name="key">Key used to get the value from configuration.</param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// Sets a value in the configuration by key.
        /// </summary>
        /// <param name="key">Key used to identify the setting.</param>
        /// <param name="value">Value to store under the given key.</param>
        void Set(string key, object value);
    }
}
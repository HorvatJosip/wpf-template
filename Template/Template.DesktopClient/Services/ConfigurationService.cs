using Template.Core;
using static Template.DesktopClient.Properties.Settings;

namespace Template.DesktopClient
{
    /// <summary>
    /// Implementation of <see cref="IConfigurationService"/>.
    /// </summary>
    public class ConfigurationService : IConfigurationService
    {
        /// <summary>
        /// Implementation of <see cref="IConfigurationService.Get(string)"/>.
        /// </summary>
        public string Get(string key) => Get<string>(key);

        /// <summary>
        /// Implementation of <see cref="IConfigurationService.Get{T}(string)"/>.
        /// </summary>
        public T Get<T>(string key) => Try.Get(() => (T)Default[key]);

        /// <summary>
        /// Implementation of <see cref="IConfigurationService.Set(string, object)"/>.
        /// </summary>
        public void Set(string key, object value)
        {
            if (Try.To(() => Default[key] = value))
                Default.Save();
        }
    }
}

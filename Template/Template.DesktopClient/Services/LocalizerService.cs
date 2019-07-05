using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows;
using Template.Core;

namespace Template.DesktopClient
{
    /// <summary>
    /// Implementation of <see cref="ILocalizerService"/>.
    /// </summary>
    public class LocalizerService : ILocalizerService
    {
        private List<ResourceDictionary> dictionaries = new List<ResourceDictionary>();

        public LocalizerService()
        {
            foreach (var dictionary in Application.Current.Resources.MergedDictionaries)
                if (dictionary.Source.OriginalString.Contains("Localization"))
                    dictionaries.Add(dictionary);
        }

        /// <summary>
        /// Implementation of <see cref="ILocalizerService.ChangeLanguage(string)"/>.
        /// </summary>
        public bool ChangeLanguage(string culture)
        {
            foreach (var dictionary in dictionaries)
            {
                var uri = dictionary.Source;

                var localizationIndex = uri.OriginalString.IndexOf("Localization");
                var left = uri.OriginalString.IndexOf('/', localizationIndex) + 1;
                var right = uri.OriginalString.IndexOf('/', left);

                var currentCulture = uri.OriginalString.Substring(left, right - left);

                if (currentCulture == culture)
                    continue;

                var newUri = uri.IsAbsoluteUri
                    ? new Uri(uri.AbsolutePath.Replace(currentCulture, culture))
                    : new Uri(string.Format("{0}{1}{2}",
                        uri.OriginalString.Substring(0, left),
                        culture,
                        uri.OriginalString.Substring(right)
                    ), UriKind.Relative);

                try { dictionary.Source = newUri; }
                catch { return false; }
            }

            var cultureInfo = new CultureInfo(culture);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            return true;
        }

        /// <summary>
        /// Implementation of <see cref="ILocalizerService.Get(string)"/>.
        /// </summary>
        public string Get(string key)
        {
            foreach (var dictionary in dictionaries)
                if (dictionary.Contains(key))
                    return dictionary[key].ToString();

            return null;
        }

        /// <summary>
        /// Implementation of <see cref="ILocalizerService.Get(string, string)"/>.
        /// </summary>
        public string Get(string dictionaryName, string key)
        {
            foreach (var dictionary in dictionaries)
                if(dictionary.Source.OriginalString.Contains(dictionaryName) && dictionary.Contains(key))
                    return dictionary[key].ToString();

            return null;
        }
    }
}

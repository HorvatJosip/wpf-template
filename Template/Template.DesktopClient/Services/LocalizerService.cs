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

        private string GetItem(string key, string dictionary = null, int? index = null)
        {
            ResourceDictionary dic = null;

            if (dictionary != null)
                dic = dictionaries.Find(d => d.Source.OriginalString.Contains(dictionary));

            if (dic == null || !dic.Contains(key))
                foreach (var d in dictionaries)
                    if (d.Contains(key))
                    {
                        dic = d;
                        break;
                    }

            if (dic == null)
                return null;

            var item = dic[key];

            if (index.HasValue)
                item = (item as string[])[index.Value];

            return item?.ToString();
        }

        /// <summary>
        /// Implementation of <see cref="ILocalizerService.this[string]"/>.
        /// </summary>
        public string this[string key] => GetItem(key);

        /// <summary>
        /// Implementation of <see cref="ILocalizerService.this[string, string]"/>.
        /// </summary>
        public string this[string dictionaryName, string key] => GetItem(key, dictionaryName);

        /// <summary>
        /// Implementation of <see cref="ILocalizerService.this[string, string, int]"/>.
        /// </summary>
        public string this[string dictionaryName, string key, int index] => GetItem(key, dictionaryName, index);

        /// <summary>
        /// Implementation of <see cref="ILocalizerService.this[string, int]"/>.
        /// </summary>
        public string this[string key, int index] => GetItem(key, null, index);
    }
}

using System;
using System.Globalization;
using System.Windows.Data;

namespace Template.DesktopClient
{
    /// <summary>
    /// Used for converting <see cref="Core.Page"/> into <see cref="BasePage{VM}"/> path.
    /// </summary>
    [ValueConversion(typeof(Core.Page), typeof(Uri))]
    public class PageEnumToPageConverter : BaseConverter<PageEnumToPageConverter>
    {
        /// <summary>
        /// If the <paramref name="value"/> is a <see cref="Core.Page"/>, a <see cref="Uri"/>
        /// is returned that looks inside the Pages folder for a page with name corresponding to
        /// the page name with Page suffix.
        /// </summary>
        /// <param name="value">A <see cref="Core.Page"/>.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If we received a page...
            if (value is Core.Page page)
                // Return a relative uri to the page
                return new Uri($"../Pages/{page}Page.xaml", UriKind.Relative);

            // Otherwise, return null
            return null;
        }

        /// <exception cref="NotImplementedException"/>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

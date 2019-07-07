using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Template.DesktopClient
{
    /// <summary>
    /// Used for converting <see cref="string"/> into <see cref="Visibility"/>.
    /// </summary>
    [ValueConversion(typeof(string), typeof(Visibility))]
    public class StringToVisibilityConverter : BaseConverter<StringToVisibilityConverter>
    {
        /// <summary>
        /// Checks a string if it's null or empty. If it is, <see cref="Visibility.Collapsed"/> is returned.
        /// Otherwise, <see cref="Visibility.Visible"/> is returned.
        /// <para>In order to change this default behaviour, you can pass in a specific string as parameter.
        /// The capitalization doesn't matter.</para>
        /// <para>"hide" - this will return <see cref="Visibility.Hidden"/> instead of <see cref="Visibility.Collapsed"/>
        /// when there is no text in the string.</para>
        /// <para>"invert" - this will invert the behavior (when there is some text, it will return
        /// <see cref="Visibility.Collapsed"/>, but when there isn't any, it will return <see cref="Visibility.Visible"/>).</para>
        /// <para>"hide and invert" - this will combine the "hide" and "invert" options. So, when there is some text,
        /// it will return <see cref="Visibility.Hidden"/> and when there isn't any,
        /// it will return <see cref="Visibility.Visible"/>).</para>
        /// </summary>
        /// <param name="value">A <see cref="string"/>.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">String that indicates the behavior or null to keep the default one.</param>
        /// <param name="culture">Not used.</param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If we received a string...
            if (value is string str)
            {
                // Check the values
                var empty = string.IsNullOrEmpty(str);
                var param = parameter?.ToString().ToLower();

                // Determine what to return based on the values
                switch (param)
                {
                    case "hide":
                        return empty ? Visibility.Hidden : Visibility.Visible;

                    case "hide and invert":
                        return empty ? Visibility.Visible : Visibility.Hidden;

                    case "invert":
                        return empty ? Visibility.Visible : Visibility.Collapsed;

                    default:
                        return empty ? Visibility.Collapsed : Visibility.Visible;
                }
            }

            // Otherwise, return collapsed
            return Visibility.Collapsed;
        }

        /// <exception cref="NotImplementedException"/>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

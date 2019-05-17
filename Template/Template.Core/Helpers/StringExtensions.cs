namespace Template.Core
{
    /// <summary>
    /// Extensions for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Shortened way for writing <see cref="string.IsNullOrEmpty(string)"/>.
        /// </summary>
        /// <param name="str">String to check if it's null or empty.</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
            => string.IsNullOrEmpty(str);

        /// <summary>
        /// Shortened way for writing <see cref="string.IsNullOrWhiteSpace(string)"/>.
        /// </summary>
        /// <param name="str">String to check if it's null or contains only whitespace.</param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
            => string.IsNullOrWhiteSpace(str);
    }
}

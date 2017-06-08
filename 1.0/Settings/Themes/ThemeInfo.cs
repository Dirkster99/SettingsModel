namespace Settings.Themes
{
    using System;

    /// <summary>
    /// Describes a WPF theme by its name and Uri source.
    /// </summary>
    internal class ThemeInfo : IThemeInfo
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="themeName"></param>
        /// <param name="themeSource"></param>
        public ThemeInfo(string themeName,
                         Uri themeSource)
        {
            DisplayName = themeName;
            ThemeSource = new Uri(themeSource.OriginalString, UriKind.Relative);
        }

        /// <summary>
        /// Gets the displayable (localized) name for this theme.
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Gets the Uri source for this theme.
        /// </summary>
        public Uri ThemeSource { get; private set; }
    }
}

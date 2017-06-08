namespace Settings.Themes
{
    using System;

    /// <summary>
    /// Describes a WPF theme by its name and Uri source.
    /// </summary>
    public interface IThemeInfo
    {
        /// <summary>
        /// Gets the displayable (localized) name for this theme.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Gets the Uri source for this theme.
        /// </summary>
        Uri ThemeSource { get; }
    }
}

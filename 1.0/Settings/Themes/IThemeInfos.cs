namespace Settings.Themes
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface to a class that manages a set of theminfo entries.
    /// </summary>
    public interface IThemeInfos
    {
        /// <summary>
        /// Add another theme entry by its name and Uri source.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="themeSource"></param>
        void AddThemeInfo(string name, Uri themeSource);

        /// <summary>
        /// Retrieve an existing theme entry by its Uri source.
        /// Returns null if theme is not present.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IThemeInfo GetThemeInfo(Uri themeSource);

        /// <summary>
        /// Remove an existing theme entry by its Uri source.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IThemeInfo RemoveThemeInfo(Uri themeSource);

        /// <summary>
        /// Enumerate through all themes
        /// </summary>
        /// <returns></returns>
        IEnumerable<IThemeInfo> GetThemeInfos();
    }
}

namespace Settings.Themes
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Manages a set of theminfo entries.
    /// </summary>
    internal class ThemeInfos : Settings.Themes.IThemeInfos
    {
        Dictionary<Uri, ThemeInfo> mDic = new Dictionary<Uri, ThemeInfo>();

        /// <summary>
        /// Add another theme entry by its name and Uri source.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="themeSource"></param>
        public void AddThemeInfo(string name, Uri themeSource)
        {
            mDic.Add(themeSource, new ThemeInfo(name, themeSource));
        }

        /// <summary>
        /// Retrieve an existing theme entry by its Uri source.
        /// Returns null if theme is not present.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IThemeInfo GetThemeInfo(Uri source)
        {
            ThemeInfo ret = null;
            mDic.TryGetValue(source, out ret);

            return ret;
        }

        /// <summary>
        /// Remove an existing theme entry by its Uri source.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IThemeInfo RemoveThemeInfo(Uri source)
        {
            ThemeInfo ret = null;
            if (mDic.TryGetValue(source, out ret) == true)
            {
                mDic.Remove(source);
                return ret;
            }

            return ret;
        }

        /// <summary>
        /// Enumerate through all themes
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IThemeInfo> GetThemeInfos()
        {
            foreach (var item in mDic.Values)
                yield return item;
        }
    }
}

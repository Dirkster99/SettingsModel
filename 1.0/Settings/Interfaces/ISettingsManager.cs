namespace Settings.Interfaces
{
    using Settings.ProgramSettings;
    using Settings.Themes;
    using System.Collections.Generic;
    using System.Windows.Media;
    using System.Xml.Serialization;

    public interface ISettingsManager : IOptionsPanel
    {
        #region properties
        /// <summary>
        /// Gets the <seealso cref="IProfile"/> object that describes the current session data
        /// (e.g.: window size, font size etc.)
        /// </summary>
        Settings.Interfaces.IProfile SessionData { get; }

        /// <summary>
        /// Gets the default icon size for the application.
        /// </summary>
        int DefaultIconSize { get; }

        /// <summary>
        /// Gets the maximum icon size that should be used in this application.
        /// </summary>
        int IconSizeMin { get; }

        /// <summary>
        /// Gets the minimum font size that should be used in this application.
        /// </summary>
        int IconSizeMax { get; }

        /// <summary>
        /// Gets the default font size for the application.
        /// </summary>
        int DefaultFontSize { get; }

        /// <summary>
        /// Gets the maximum font size that should be used in this application.
        /// </summary>
        int FontSizeMin { get; }

        /// <summary>
        /// Gets the default icon size for the application.
        /// </summary>
        int FontSizeMax { get; }

        /// <summary>
        /// Gets the default font size for the application.
        /// </summary>
        int DefaultFixedFontSize { get; }

        /// <summary>
        /// Gets the default accent color for this application.
        /// </summary>
        Color DefaultAccentColor { get; }

        /// <summary>
        /// Gets the internal name and Uri source for all available themes.
        /// </summary>
        IThemeInfos Themes { get; }
        #endregion properties

        #region methods
        /// <summary>
        /// Determine whether program options are valid and correct
        /// settings if they appear to be invalid on current system.
        /// </summary>
        void CheckSettingsOnLoad(double SystemParameters_VirtualScreenLeft, double SystemParameters_VirtualScreenTop);

        /// <summary>
        /// Save program options into persistence.
        /// See <seealso cref="SaveOptions"/> to save program options on program end.
        /// </summary>
        /// <param name="sessionDataFileName"></param>
        /// <returns></returns>
        void LoadSessionData(string sessionDataFileName);

        /// <summary>
        /// Save program options into persistence.
        /// See <seealso cref="LoadOptions"/> to load program options on program start.
        /// </summary>
        /// <param name="sessionDataFileName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SaveSessionData(string sessionDataFileName, Settings.Interfaces.IProfile model);

        /// <summary>
        /// Get a list of all supported languages for this application.
        /// </summary>
        /// <returns></returns>
        IEnumerable<LanguageCollection> GetSupportedLanguages();
        #endregion methods
    }
}

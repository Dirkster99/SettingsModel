namespace SettingsModelWPFDemo.Models
{
    using FirstFloor.ModernUI.Presentation;
    using Settings.Interfaces;
    using Settings.UserProfile;
    using SettingsModel.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;

    /// <summary>
    /// Class contains all methods necessary to initialize the applications settings model.
    /// </summary>
    internal static class SettingDefaults
    {
        private const string _LightThemeName = "light";
        private const string _DarkThemeName = "dark";

        /// <summary>
        /// Create a minimal set of available themes.
        /// </summary>
        /// <param name="settings"></param>
        public static void CreateThemes(ISettingsManager settings)
        {
            settings.Themes.AddThemeInfo(_LightThemeName, AppearanceManager.DarkThemeSource);
            settings.Themes.AddThemeInfo(_DarkThemeName, AppearanceManager.LightThemeSource);
        }

        /// <summary>
        /// Create the minimal settings model that should be used for every locult application.
        /// This model does not include advanced features like theming etc...
        /// </summary>
        /// <param name="settings"></param>
        public static void CreateGeneralSettings(IEngine optsEngine)
        {
            const string groupName = "Options";

            optsEngine.AddOption(groupName, "ReloadOpenFilesFromLastSession", typeof(bool), false, true);
            optsEngine.AddOption(groupName, "SourceFilePath", typeof(string), false, @"C:\temp\source\");
            optsEngine.AddOption(groupName, "LanguageSelected", typeof(string), false, "en-US");

            var schema = optsEngine.AddListOption<string>(groupName, "BookmarkedFolders", typeof(string), false, new List<string>());
            schema.List_AddValue("Desktop", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            schema.List_AddValue("Documents", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            schema.List_AddValue("Music", Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
            schema.List_AddValue("Pictures", Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            schema.List_AddValue("Videos", Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
        }

        /// <summary>
        /// Create the minimal settings model that should be used for every locult application.
        /// </summary>
        /// <param name="settings"></param>
        public static void CreateAppearanceSettings(IEngine options, ISettingsManager settings)
        {
            const string groupName = "Appearance";

            options.AddOption(groupName, "ThemeDisplayName", typeof(string), false, _LightThemeName);
            options.AddOption(groupName, "AccentColor", typeof(Color), false,  settings.DefaultAccentColor);

            options.AddOption(groupName, "DefaultIconSize", typeof(int), false, settings.DefaultIconSize);
            options.AddOption(groupName, "DefaultFontSize", typeof(int), false, settings.DefaultFontSize);
            options.AddOption(groupName, "FixedFontSize", typeof(int), false, settings.DefaultFixedFontSize);

            // Ceate default window position, width and height for this session
            settings.SessionData.UpdateInsertWindowPosSize(
                settings.SessionData.MainWindowName, new ViewPosSizeModel(100, 100, 800, 700, false));
        }
    }
}

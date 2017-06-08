namespace SettingsModelWPFDemo.ViewModels.SettingPages
{
    using FirstFloor.ModernUI.Presentation;
    using Settings.Interfaces;
    using Settings.Themes;
    using SettingsModel.Interfaces;
    using SettingsModelWPFDemo.ViewModels.Base.SettingPages;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class AppearanceViewModel : SettingsPageBaseViewModel
    {
        #region fields
////        private const string FontSmall = "small";
////        private const string FontLarge = "large";

////        // 9 accent colors from metro design principles
////        private Color[] accentColors = new Color[]{
////            Color.FromRgb(0x33, 0x99, 0xff),   // blue
////            Color.FromRgb(0x00, 0xab, 0xa9),   // teal
////            Color.FromRgb(0x33, 0x99, 0x33),   // green
////            Color.FromRgb(0x8c, 0xbf, 0x26),   // lime
////            Color.FromRgb(0xf0, 0x96, 0x09),   // orange
////            Color.FromRgb(0xff, 0x45, 0x00),   // orange red
////            Color.FromRgb(0xe5, 0x14, 0x00),   // red
////            Color.FromRgb(0xff, 0x00, 0x97),   // magenta
////            Color.FromRgb(0xa2, 0x00, 0xff),   // purple            
////        };

        /* 20 accent colors from Windows Phone 8
        private Color[] accentColors = new Color[]{
            Color.FromRgb(0xa4, 0xc4, 0x00),   // lime
            Color.FromRgb(0x60, 0xa9, 0x17),   // green
            Color.FromRgb(0x00, 0x8a, 0x00),   // emerald
            Color.FromRgb(0x00, 0xab, 0xa9),   // teal
            Color.FromRgb(0x1b, 0xa1, 0xe2),   // cyan
            Color.FromRgb(0x00, 0x50, 0xef),   // cobalt
            Color.FromRgb(0x6a, 0x00, 0xff),   // indigo
            Color.FromRgb(0xaa, 0x00, 0xff),   // violet
            Color.FromRgb(0xf4, 0x72, 0xd0),   // pink
            Color.FromRgb(0xd8, 0x00, 0x73),   // magenta
            Color.FromRgb(0xa2, 0x00, 0x25),   // crimson
            Color.FromRgb(0xe5, 0x14, 0x00),   // red
            Color.FromRgb(0xfa, 0x68, 0x00),   // orange
            Color.FromRgb(0xf0, 0xa3, 0x0a),   // amber
            Color.FromRgb(0xe3, 0xc8, 0x00),   // yellow
            Color.FromRgb(0x82, 0x5a, 0x2c),   // brown
            Color.FromRgb(0x6d, 0x87, 0x64),   // olive
            Color.FromRgb(0x64, 0x76, 0x87),   // steel
            Color.FromRgb(0x76, 0x60, 0x8a),   // mauve
            Color.FromRgb(0x87, 0x79, 0x4e),   // taupe
        };
***/
        private int _PointIconSize = 32;
        private int _PointFontSize = 14;
        private int _PointFixedFontSize = 12;
        private Color mNextAkzentColor;
        private bool mIsOpenColorPicker = false;

        private Color selectedAccentColor;
        private LinkCollection themes = new LinkCollection();
        private Link selectedTheme;
////        private string selectedFontSize;
        #endregion fields

        #region contructors
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="themeModels"></param>
        public AppearanceViewModel(IThemeInfos themeModels) :
            base(Local.Strings.STR_Appearance_SETTINGS_Caption)
        {
            InitObject(themeModels);
        }
        #endregion contructors

        #region properties
        #region IconSize
        public int PointIconSize
        {
            get
            {
                return _PointIconSize;
            }

            set
            {
                if (_PointIconSize != value)
                {
                    _PointIconSize = value;
                    IsDirty = true;
                    RaisePropertyChanged(() => PointIconSize);
                }
            }
        }

        /// <summary>
        /// Gets the minimum size an icon should have
        /// </summary>
        public int PointIconSizeMin { get; private set; }

        /// <summary>
        /// Gets the maximum size an icon should have
        /// </summary>
        public int PointIconSizeMax { get; private set; }
        #endregion IconSize

        #region FontSize
        /// <summary>
        /// Gets/sets a value that determines the size of the displayed font.
        /// </summary>
        public int PointFontSize
        {
            get
            {
                return _PointFontSize;
            }

            set
            {
                if (_PointFontSize != value)
                {
                    _PointFontSize = value;
                    IsDirty = true;
                    UpdateFontSizes();
                    RaisePropertyChanged(() => PointFontSize);
                }
            }
        }

        /// <summary>
        /// Gets/sets a value that determines the size of the displayed font.
        /// </summary>
        public int PointFixedFontSize
        {
            get
            {
                return _PointFixedFontSize;
            }

            set
            {
                if (_PointFixedFontSize != value)
                {
                    _PointFixedFontSize = value;
                    IsDirty = true;
                    UpdateFontSizes();
                    RaisePropertyChanged(() => PointFixedFontSize);
                }
            }
        }
        
        /// <summary>
        /// Gets the minimum font size an icon should have
        /// </summary>
        public int PointFontSizeMin { get; private set; }

        /// <summary>
        /// Gets the maximum font size an icon should have
        /// </summary>
        public int PointFontSizeMax { get; private set; }
        #endregion FontSize
        #endregion properties

        /// <summary>
        /// Sync Accent Color and theme selection when they are being edit.
        /// </summary>
        private void SyncThemeAndColor()
        {
            // synchronizes the selected viewmodel theme with the actual theme used by the appearance manager.
            this.SelectedTheme = this.themes.FirstOrDefault(l => l.Source.Equals(AppearanceManager.Current.ThemeSource));

            // and make sure accent color is up-to-date
            this.NextAkzentColor = this.SelectedAccentColor = AppearanceManager.Current.AccentColor;
        }

        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "AccentColor")
            {
                SyncThemeAndColor();
            }
            else
            if (e.PropertyName == "ThemeSource" )
            {
                var color = SelectedAccentColor;
                SyncThemeAndColor();
                NextAkzentColor = SelectedAccentColor = color;
            }
        }

        public LinkCollection Themes
        {
            get { return this.themes; }
        }

////        public string[] FontSizes
////        {
////            get { return new string[] { FontSmall, FontLarge }; }
////        }

////        public Color[] AccentColors
////        {
////            get { return this.accentColors; }
////        }

        public Link SelectedTheme
        {
            get { return this.selectedTheme; }
            set
            {
                if (this.selectedTheme != value)
                {
                    this.selectedTheme = value;
                    RaisePropertyChanged(() => SelectedTheme);

                    // and update the actual theme
                    var color = AppearanceManager.Current.AccentColor;
                    AppearanceManager.Current.ThemeSource = value.Source;
                    AppearanceManager.Current.AccentColor = selectedAccentColor = NextAkzentColor = color;
                }
            }
        }

////        public string SelectedFontSize
////        {
////            get { return this.selectedFontSize; }
////            set
////            {
////                if (this.selectedFontSize != value)
////                {
////                    this.selectedFontSize = value;
////                    RaisePropertyChanged(() => SelectedFontSize);
////
////                    AppearanceManager.Current.FontSize = value == FontLarge ? FontSize.Large : FontSize.Small;
////
////                    // Assign new values to corresponding viewmodel values
////                    double val = (double)Application.Current.Resources[AppearanceManager.KeyDefaultFontSize];
////                    PointFontSize = Convert.ToInt32( Math.Abs( Math.Round(val)));
////                    PointFixedFontSize = Convert.ToInt32( Math.Abs( Math.Round( Convert.ToDouble(Application.Current.Resources[AppearanceManager.KeyFixedFontSize]))));
////                }
////            }
////        }

        public Color SelectedAccentColor
        {
            get { return this.selectedAccentColor; }
            set
            {
                if (this.selectedAccentColor != value)
                {
                    this.selectedAccentColor = value;
                    RaisePropertyChanged(() => SelectedAccentColor);
                }
            }
        }

        public Color NextAkzentColor
        {
            get
            {
                return mNextAkzentColor;
            }

            set
            {
                if (mNextAkzentColor != value)
                {
                    mNextAkzentColor = value;
                    RaisePropertyChanged(() => NextAkzentColor);
                }
            }
        }

        public bool IsOpenColorPicker
        {
            get
            {
                return mIsOpenColorPicker;
            }

            set
            {
                if (mIsOpenColorPicker != value)
                {
                    // Sync into accent color when color picker is closed
                    if (mIsOpenColorPicker == true && value == false)
                    {
                        AppearanceManager.Current.AccentColor = this.NextAkzentColor;
                        this.SelectedAccentColor = this.NextAkzentColor;
                        SyncThemeAndColor();
                    }

                    mIsOpenColorPicker = value;
                    RaisePropertyChanged(() => IsOpenColorPicker);
                }
            }
        }

        #region methods
        /// <summary>
        /// Applies the current model based option values to the actual WPF environment
        /// (via FirstFloor.AppearanceManager(...))
        /// </summary>
        /// <param name="options"></param>
        public override void ApplyOptionsFromModel(IEngine options)
        {
            var theme = options.GetOptionValue<string>("Appearance", "ThemeDisplayName");

            var themeDisplayName = ConvertThemeNameToLocalizedString(theme);
            var nextTheme = this.themes.FirstOrDefault(l => string.Compare(l.DisplayName, themeDisplayName, true) == 0);

            if (nextTheme != null)
                this.SelectedTheme = nextTheme;

            this.SelectedAccentColor = options.GetOptionValue<Color>("Appearance", "AccentColor");
            AppearanceManager.Current.AccentColor = this.SelectedAccentColor;

            // get copy of actual value and min, max value definitions from model into viewmodel
            PointIconSize = options.GetOptionValue<int>("Appearance", "DefaultIconSize");
            PointFontSize = options.GetOptionValue<int>("Appearance", "DefaultFontSize");
            PointFixedFontSize = options.GetOptionValue<int>("Appearance", "FixedFontSize");

            // Set defaults based on defaults from settings manager
            var settings = GetService<ISettingsManager>();
            this.PointFontSizeMin = settings.FontSizeMin;
            this.PointFontSizeMax = settings.FontSizeMax;
            this.PointIconSizeMin = settings.IconSizeMin;
            this.PointIconSizeMax = settings.IconSizeMax;

            SyncThemeAndColor();
            UpdateFontSizes();
        }

        /// <summary>
        /// Save changed settings back to model for further
        /// application and persistence in file system.
        /// </summary>
        /// <param name="settingData"></param>
        public override void SaveOptionsToModel(IEngine options)
        {
            var settings = GetService<ISettingsManager>();
            settings.Themes.GetThemeInfo(this.SelectedTheme.Source);

            if (this.SelectedTheme != null)
            {
                var themeName = ConvertLocalizedStringToThemeName(this.SelectedTheme.DisplayName);
                options.SetOptionValue("Appearance", "ThemeDisplayName", themeName);
            }

            options.SetOptionValue("Appearance", "AccentColor", this.selectedAccentColor);

            options.SetOptionValue("Appearance", "DefaultIconSize", (int)PointIconSize);
            options.SetOptionValue("Appearance", "DefaultFontSize", (int)PointFontSize);
            options.SetOptionValue("Appearance", "FixedFontSize", (int)PointFixedFontSize);
        }

        /// <summary>
        /// Initialize class members and properties from <paramref name="themeModels"/> settings.
        /// </summary>
        /// <param name="themeModels"></param>
        private void InitObject(IThemeInfos themeModels)
        {
            try
            {
                // add the default themes
                foreach (var item in themeModels.GetThemeInfos())
                {
                    var displayName = ConvertThemeNameToLocalizedString(item.DisplayName);

                    this.themes.Add(new Link { DisplayName = displayName, Source = item.ThemeSource });
                }

                ////this.SelectedFontSize = AppearanceManager.Current.FontSize == FontSize.Large ? FontLarge : FontSmall;
                SyncThemeAndColor();

                AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;
            }
            catch
            {
            }
        }

        /// <summary>
        /// Update current fontsize values to view
        /// </summary>
        private void UpdateFontSizes()
        {
            Application.Current.Resources[AppearanceManager.KeyDefaultFontSize] = (double)PointFontSize;
            Application.Current.Resources[AppearanceManager.KeyFixedFontSize]   = (double)PointFixedFontSize;
        }

        /// <summary>
        /// Convert a theme model name into a localized string.
        /// </summary>
        /// <param name="themeName"></param>
        /// <returns></returns>
        private string ConvertThemeNameToLocalizedString(string themeName)
        {
            switch (themeName)
            {
                case "dark":
                    return Local.Strings.STR_DARK_THEME;

                case "light":
                    return Local.Strings.STR_LIGHT_THEME;
            }

            throw new NotImplementedException("Unknown theme name from model:" + themeName);
        }

        /// <summary>
        /// Convert a localized string into a theme model name.
        /// </summary>
        /// <param name="themeName"></param>
        /// <returns></returns>
        private string ConvertLocalizedStringToThemeName(string localizedSTring)
        {
            if(Local.Strings.STR_DARK_THEME == localizedSTring)
                return "dark";
            else
            {
                if(Local.Strings.STR_LIGHT_THEME == localizedSTring)
                    return "light";
            }

            throw new NotImplementedException("Unknown theme name from model:" + localizedSTring);
        }
        #endregion methods
    }
}

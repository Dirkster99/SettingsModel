namespace SettingsModelWPFDemo.ViewModels.SettingPages
{
    using Settings.Interfaces;
    using Settings.ProgramSettings;
    using SettingsModel.Interfaces;
    using SettingsModelWPFDemo.ViewModels.Base;
    using SettingsModelWPFDemo.ViewModels.Base.SettingPages;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    public class GeneralViewModel : SettingsPageBaseViewModel
    {
        #region fields
        private bool mReloadFilesOnAppStart = true;

        private LanguageCollection mLanguageSelected;
        private string mBookmarkSelected;
        private ICommand mAddFolderCommand;
        private ICommand mRemoveFolderCommand;
        #endregion fields

        #region constructors
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="settingsManager"></param>
        public GeneralViewModel()
            : base(Local.Strings.STR_GENERAL_SETTINGS_Caption)
        {
        }

        ////protected GeneralSettingsViewModel()
        ////    : base()
        ////{
        ////}
        #endregion constructors

        #region properties
        #region Language Localization Support
        /// <summary>
        /// Get list of GUI languages supported in this application.
        /// </summary>
        public ObservableCollection<LanguageCollection> Languages { get; private set; }

        /// <summary>
        /// Get/set language application language.
        /// </summary>
        public LanguageCollection LanguageSelected
        {
            get
            {
                return mLanguageSelected;
            }

            set
            {
                if (mLanguageSelected != value)
                {
                    mLanguageSelected = value;
                    RaisePropertyChanged(() => LanguageSelected);
                    IsDirty = true;
                }
            }
        }
        #endregion Language Localization Support

        #region Bookmark Support
        public ObservableCollection<string> Bookmarks { get; private set; }

        public string BookmarkSelected
        {
            get
            {
                return mBookmarkSelected;
            }

            set
            {
                if (mBookmarkSelected != value)
                {
                    mBookmarkSelected = value;
                    RaisePropertyChanged(() => BookmarkSelected);
                    IsDirty = true;
                }
            }
        }

        /// <summary>
        /// Gets a command that expects a string commandparemter
        /// and adds that string from the collection of bookmarked
        /// folders.
        /// </summary>
        public ICommand AddFolderCommand
        {
            get
            {
                if (mAddFolderCommand == null)
                {
                    mAddFolderCommand = new RelayCommand<object>((p) =>
                    {
                        string path = p as string;

                        if (path == null)
                            return;

                        var items = Bookmarks.SingleOrDefault(item => string.Compare(item, path, true) == 0);

                        // Make list of paths a list of unique paths
                        if (items != null)
                        {
                            if (items.Count() > 0)
                                return;
                        }

                        Bookmarks.Add(path);
                    });
                }

                return mAddFolderCommand;
            }
        }

        /// <summary>
        /// Gets a command that expects a string commandparemter
        /// and removes that string from the collection of bookmarked
        /// folders.
        /// </summary>
        public ICommand RemoveFolderCommand
        {
            get
            {
                if (mRemoveFolderCommand == null)
                {
                    mRemoveFolderCommand = new RelayCommand<object>((p) =>
                    {
                        string path = p as string;

                        if (path == null)
                            return;

                        Bookmarks.Remove(path);
                    });
                }

                return mRemoveFolderCommand;
            }
        }
        #endregion Bookmark Support

        /// <summary>
        /// Gets/sets the ReloadOption setting.
        /// </summary>
        public bool ReloadFilesOnAppStart
        {
            get
            {
                return mReloadFilesOnAppStart;
            }

            set
            {
                if (mReloadFilesOnAppStart != value)
                {
                    mReloadFilesOnAppStart = value;
                    RaisePropertyChanged(() => ReloadFilesOnAppStart);
                    IsDirty = true;
                }
            }
        }
        #endregion properties

        #region methods
        /// <summary>
        /// Save changed settings back to model for further
        /// application and persistence in file system.
        /// </summary>
        /// <param name="settingData"></param>
        public override void SaveOptionsToModel(IEngine optionsEngine)
        {
            var group = optionsEngine.GetOptionGroup("Options");

            SaveOptionsToModel(group);
        }

        /// <summary>
        /// Applies the current model based option values to the actual WPF environment
        /// </summary>
        /// <param name="options"></param>
        public override void ApplyOptionsFromModel(IEngine optionsEngine)
        {
            var optGroup = optionsEngine.GetOptionGroup("Options");

            mReloadFilesOnAppStart = optGroup.GetValue<bool>("ReloadOpenFilesFromLastSession");

            var settings = GetService<ISettingsManager>();

            // Initialize localization settings
            if (Languages == null)
                Languages = new ObservableCollection<LanguageCollection>(settings.GetSupportedLanguages());

            // Set default language to make sure app neutral is selected and available for sure
            // (this is a fallback if all else fails)
            try
            {
                var langOpt = optGroup.GetValue<string>("LanguageSelected");
                LanguageSelected = Languages.FirstOrDefault(lang => lang.BCP47 == langOpt);

                if (LanguageSelected == null)
                {
                    if (Languages.Count > 0)
                        LanguageSelected = Languages[0];
                }
            }
            catch
            {
            }

            try
            {
                Bookmarks = new ObservableCollection<string>();
                var opt = optGroup.GetOptionDefinition("BookmarkedFolders");

                foreach (var item in opt.List_GetListOfKeyValues())
	            {
                    string sValue = item.Value as string;

                    if (sValue != null)
                        Bookmarks.Add(sValue);
	            }

                if (Bookmarks.Count() > 0)
                    BookmarkSelected = Bookmarks[0];
             }
            catch
            {
            }
        }

        /// <summary>
        /// Save changed settings back to model for further
        /// application and persistence in file system.
        /// </summary>
        /// <param name="settingData"></param>
        public void SaveOptionsToModel(IOptionGroup optGroup)
        {
            optGroup.SetValue("ReloadOpenFilesFromLastSession", ReloadFilesOnAppStart);

            if (LanguageSelected != null)
                optGroup.SetValue("LanguageSelected", LanguageSelected.BCP47);

            var opt = optGroup.GetOptionDefinition("BookmarkedFolders");
            optGroup.List_Clear("BookmarkedFolders");

            if (Bookmarks.Count > 0)
            {
                var schema = optGroup.GetOptionDefinition("BookmarkedFolders");

                foreach (var item in Bookmarks)
                {
                    schema.List_AddValue(item, item);
                }
            }
        }
        #endregion methods
    }
}

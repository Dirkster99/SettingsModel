namespace SettingsModelWPFDemo.ViewModels.Base.SettingPages
{
    using SettingsModel.Interfaces;

    public abstract class SettingsPageBaseViewModel : DocumentViewModelBase
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="displayTitle"></param>
        public SettingsPageBaseViewModel(string displayTitle)
            : this()
        {
            DisplayTitle = displayTitle;
        }

        /// <summary>
        /// Claass constructor
        /// </summary>
        protected SettingsPageBaseViewModel()
            : base()
        {

        }

        /// <summary>
        /// Gets a localizable string for display of a title.
        /// </summary>
        public string DisplayTitle { get; private set; }

        /// <summary>
        /// Applies the current model based option values to the actual WPF environment
        /// </summary>
        /// <param name="options"></param>
        public abstract void ApplyOptionsFromModel(IEngine options);

        /// <summary>
        /// Save changed settings back to model for further
        /// application and persistence in file system.
        /// </summary>
        /// <param name="settingData"></param>
        public abstract void SaveOptionsToModel(IEngine optionsOptionGroup);
    }
}

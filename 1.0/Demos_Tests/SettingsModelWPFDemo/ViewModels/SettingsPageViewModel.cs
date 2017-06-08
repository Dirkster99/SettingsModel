namespace SettingsModelWPFDemo.ViewModels
{
    using Settings.Interfaces;
    using SettingsModelWPFDemo.ViewModels.Base.SettingPages;
    using SettingsModelWPFDemo.ViewModels.SettingPages;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Defines an interface that supports commands to be executed on load
    /// and unload of controls in presentation layer.
    /// </summary>
    public interface LoadUnloadCommands
    {
        /// <summary>
        /// Execute this command to acquire resources and compute data for display.
        /// </summary>
        void LoadedCommand();

        /// <summary>
        /// Execute this command to free resources and store data
        /// results from display manipulation.
        /// </summary>
        void UnloadedCommand();
    }

    /// <summary>
    /// Implements a viewmodel that manages a settings view with all its sub-setting pages.
    /// </summary>
    public class SettingsPageViewModel : Base.ViewModels.ViewModelBase, LoadUnloadCommands
    {
        #region fields
        private ObservableCollection<SettingsPageBaseViewModel> mPages = null;
        private SettingsPageBaseViewModel mSelectedPage = null;

        private bool IsEditingSettings = false;
        #endregion fields

        #region constructors
        public SettingsPageViewModel()
        {
            mPages = new ObservableCollection<SettingsPageBaseViewModel>();
        }
        #endregion constructors

        #region properties
        /// <summary>
        /// Gets the collection that contains all settings pages thata re available for manipulation.
        /// </summary>
        public ObservableCollection<SettingsPageBaseViewModel> Pages
        {
            get
            {
                return mPages;
            }
        }

        /// <summary>
        /// Gets the currently selected settings page.
        /// </summary>
        public SettingsPageBaseViewModel SelectedPage
        {
            get
            {
                return mSelectedPage;
            }

            set
            {
                if (mSelectedPage != value)
                {
                    mSelectedPage = value;
                    RaisePropertyChanged(() => SelectedPage);
                }
            }
        }

        /// <summary>
        /// Execute to acquire resources and compute data for display.
        /// </summary>
        public void LoadedCommand()
        {
            Pages.Clear();
            LoadData();
        }

        /// <summary>
        /// Execute this to free resources and store data
        /// results from display manipulation in model.
        /// </summary>
        public void UnloadedCommand()
        {
            SaveAllDataToModel();
            Pages.Clear();
        }
        #endregion properties

        #region methods
        /// <summary>
        /// Unloads settings data from viewmodel into
        /// model if data is being edited at time of call.
        /// </summary>
        public void SaveDataToModelOnEditing()
        {
            if (IsEditingSettings == true)
                SaveAllDataToModel();
        }

        #region Load Data From Model
        /// <summary>
        /// Setup viewmodel for edtiting by initializing
        /// viewmodels from model data and selecting a default page.
        /// </summary>
        private void LoadData()
        {
            var settings = GetService<ISettingsManager>();

            var List = new List<SettingsPageBaseViewModel>()
            {
                new GeneralViewModel(),
                new AppearanceViewModel(settings.Themes),
                new AboutViewModel()
            };

            for (int i = 0; i < List.Count; i++)
            {
                LoadFromModel(List[i]);
                Pages.Add(List[i]);
            }

            SelectedPage = List[0];
            IsEditingSettings = true;
        }

        /// <summary>
        /// Applies the current appearance settings to the view.
        /// </summary>
        /// <param name="vm"></param>
        private void LoadFromModel(SettingsPageBaseViewModel vm)
        {
            if (vm != null)
            {
                var options = GetService<ISettingsManager>();
                vm.ApplyOptionsFromModel(options.Options);
            }
        }
        #endregion Load Data From Model

        #region Save Data To Model
        private void SaveAllDataToModel()
        {
            foreach (var item in Pages)
            {
                SaveToModel(item);
            }

            Pages.Clear();

            IsEditingSettings = false;
        }

        private void SaveToModel(SettingsPageBaseViewModel vm)
        {
            if (vm != null)
            {
                var options = GetService<ISettingsManager>();
                vm.SaveOptionsToModel(options.Options);
            }
        }
        #endregion Save Data To Model
        #endregion methods
    }
}

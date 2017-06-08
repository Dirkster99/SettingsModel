namespace SettingsModelWPFDemo.ViewModels
{
    using SettingsModelWPFDemo.ViewModels.Base.ViewModels;
    using System.Windows.Media;

    /// <summary>
    /// Class holds main entry points (methods and properties) relevant to this application.
    /// This class is likely to be bound to the datacontext of the MainWindow or Shell object.
    /// </summary>
    public class AppViewModel : ViewModelBase
    {
        #region fields
        protected static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly AppLifeCycleViewModel mAppLifeCycle = null;
        private readonly SettingsPageViewModel mSettingsPage  = null;
        #endregion fields

        #region constructors
        /// <summary>
        /// Class constrcutor from parameters.
        /// </summary>
        /// <param name="lifeCylceModel"></param>
        public AppViewModel(AppLifeCycleViewModel lifeCylceModel,
                            bool reloadLastSolutionOnStartup = false,
                            string solutionFilename = null)
            : this()
        {
            mAppLifeCycle = lifeCylceModel;
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        public AppViewModel()
        {
            mSettingsPage = new SettingsPageViewModel();
            mAppLifeCycle = new AppLifeCycleViewModel();
        }
        #endregion constructors

        #region properties
        /// <summary>
        /// Gets an object that contains all properties and methods that are relevant to
        /// the standard life cycle of an application.
        /// </summary>
        public AppLifeCycleViewModel AppLifeCycle
        {
            get
            {
                return mAppLifeCycle;
            }
        }

        public SettingsPageViewModel SettingsPage
        {
            get
            {
                return mSettingsPage;
            }
        }
        #endregion properties

        #region methods
        /// <summary>
        /// Save application setting on application exit.
        /// </summary>
        internal void SaveConfigOnAppClosed()
        {
            // Save setting information from viewmodel to model if settings
            // are currently edited (important for saving correct states on app shut down)
            mSettingsPage.SaveDataToModelOnEditing();

            // Save settings and do some other mechanics
            AppLifeCycle.SaveConfigOnAppClosed();
        }
        #endregion methods
    }
}

namespace SettingsModelWPFDemo.ViewModels.SettingPages
{
    using SettingsModel.Interfaces;
    using SettingsModelWPFDemo.ViewModels.Base;
    using SettingsModelWPFDemo.ViewModels.Base.SettingPages;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Windows.Input;

    /// <summary>
    /// Implements a viewmodel that drives an about view that shows
    /// relevant information about this application.
    /// </summary>
    public class AboutViewModel : SettingsPageBaseViewModel
    {
        #region fields
        private ICommand mBrowseAboutCommand;
        #endregion fields

        #region constructors
        /// <summary>
        /// Class Constructor
        /// </summary>
        public AboutViewModel()
            : base(Local.Strings.STR_About_SETTINGS_Caption)
        {
        }
        #endregion constructors

        #region properties
        /// <summary>
        /// Get application version for display in About view.
        /// </summary>
        public string AppVersion
        {
            get
            {
                return Assembly.GetEntryAssembly().GetName().Version.ToString();
            }
        }

        /// <summary>
        /// Gets the assembly copyright.
        /// </summary>
        /// <value>The assembly copyright.</value>
        public string AssemblyCopyright
        {
            get
            {
                // Get all Copyright attributes on this assembly
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

                // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return string.Empty;

                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        /// <summary>
        /// Gets a hyperlink to a project related web page.
        /// </summary>
        public Uri ProjectHyperlink
        {
            get
            {
                return new Uri("https://github.com/Dirkster99/SettingsModel");
            }
        }

        /// <summary>
        /// Gets a command that opens the browser with the Uri
        /// target given in the <seealso cref="CommandParameter"/>.
        /// </summary>
        public ICommand BrowseAboutCommand
        {
            get
            {
                if (mBrowseAboutCommand == null)
                {
                    mBrowseAboutCommand = new RelayCommand<object>((p) =>
                    {
                        var targetUri = p as Uri;

                        if (targetUri != null)
                            Hyperlink_CommandNavigateTo(targetUri);
                    });
                }

                return mBrowseAboutCommand;
            }
        }

        /// <summary>
        /// Get list of modules (referenced from EntryAssembly) and their version for display in About view.
        /// </summary>
        public SortedList<string, string> Modules
        {
            get
            {
                SortedList<string, string> l = new SortedList<string, string>();

                var name = Assembly.GetEntryAssembly().FullName;

                foreach (AssemblyName assembly in Assembly.GetEntryAssembly().GetReferencedAssemblies())
                {
                    try
                    {
                        string val;

                        if (l.TryGetValue(assembly.Name, out val) == false)
                            l.Add(assembly.Name, string.Format("{0}, {1}={2}", assembly.Name,
                                                                               Local.Strings.STR_ABOUT_Version,
                                                                               assembly.Version));
                    }
                    catch (System.Exception)
                    {
                    }
                }

                return l;
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
            // About ViewModel does not display any model related information
        }

        /// <summary>
        /// Applies the current model based option values to the actual WPF environment
        /// </summary>
        /// <param name="options"></param>
        public override void ApplyOptionsFromModel(IEngine optionsEngine)
        {
            // About ViewModel does not display any model related information
        }

        /// <summary>
        /// Process command when a hyperlink has been clicked.
        /// Start a web browser and let it browse to where this points to...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Hyperlink_CommandNavigateTo(Uri navigateUri)
        {
            try
            {
                Process.Start(new ProcessStartInfo(navigateUri.AbsoluteUri));
            }
            catch
            {
            }
        }
        #endregion methods
    }
}

namespace Settings.Interfaces
{
    using Settings.UserProfile;

    /// <summary>
    /// Define an interface to implement the model of the user profile part
    /// of the application. Typically, users have implicit run-time
    /// settings that should be re-activated when the application
    /// is re-started at a later point in time (e.g. window size and position).
    /// 
    /// This interface organizes these per user specific profile settings
    /// and is responsible for their storage (at program end) and
    /// retrieval at the start-up of the application.
    /// </summary>
    public interface IProfile
    {
        #region properties
        /// <summary>
        /// Gets the path of the last file used or empty string if file does not exists on disk.
        /// </summary>
        string GetLastActivePath();

        /// <summary>
        /// Remember the last active solution file name and path of last session.
        /// 
        /// This can be useful when selecting active document in next session or
        /// determining a useful default path when there is no document currently open.
        /// </summary>
        string LastActiveSolution { get; set; }

        /// <summary>
        /// Remember the last active path and name of last active document.
        /// 
        /// This can be useful when selecting active document in next session or
        /// determining a useful default path when there is no document currently open.
        /// </summary>
        string LastActiveTargetFile { get; set; }

        /// <summary>
        /// Gets the key name of the MainWindow item in the collection.
        /// Ths name can be used as key in the WindowPosSz property
        /// to read and write MainWindow position and size information.
        /// </summary>
        string MainWindowName { get; }

        /// <summary>
        /// Gets a collection of window position and size items.
        /// </summary>
        SerializableDictionary<string, ViewPosSizeModel> WindowPosSz { get; }
        #endregion properties

        #region methods
        /// <summary>
        /// Checks the MainWindow for visibility when re-starting application
        /// (with different screen configuration).
        /// </summary>
        /// <param name="SystemParameters_VirtualScreenLeft"></param>
        /// <param name="SystemParameters_VirtualScreenTop"></param>
        void CheckSettingsOnLoad(double SystemParameters_VirtualScreenLeft, double SystemParameters_VirtualScreenTop);

        /// <summary>
        /// Updates or inserts the requested window pos size item in the collection.
        /// </summary>
        /// <param name="windowName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        void UpdateInsertWindowPosSize(string windowName, ViewPosSizeModel model);
        #endregion methods
    }
}

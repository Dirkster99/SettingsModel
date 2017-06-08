namespace SettingsModelWPFDemo.ViewModels.Base.SettingPages
{
    using System;

    /// <summary>
    /// This interface declares base events and properties of documents being displayed.
    /// These documents can tell their parent via events when and if they become dirty.
    /// PArents can then react to this fact before disposing of the changed document object.
    /// </summary>
    public interface IDocumentDirtyChangedBase
    {
        #region properties
        /// <summary>
        /// Gets/set the dirty state of a document.
        /// </summary>
        bool IsDirty { get; set; }
        #endregion properties
    }
}

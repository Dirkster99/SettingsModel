namespace SettingsModelWPFDemo.ViewModels.Base.SettingPages
{
    using SettingsModelWPFDemo.ViewModels.Base.ViewModels;
    using System;

    public class DocumentViewModelBase : ViewModelBase, IDocumentBase, System.IDisposable
    {
        #region fields
        protected bool mDisposed = false;

        private bool _IsDirty = false;
        #endregion fields

        #region constructors
        /// <summary>
        /// Class constructor
        /// </summary>
        public DocumentViewModelBase()
        {
        }
        #endregion constructors

        #region properties
        /// <summary>
        /// Gets/set the dirty state of a document.
        /// </summary>
        public virtual bool IsDirty
        {
            get
            {
                return _IsDirty;
            }

            set
            {
                if (_IsDirty != value)
                {
                    _IsDirty = value;
                    RaisePropertyChanged(() => IsDirty);
                }
            }
        }
        #endregion properties

        #region methods
        /// <summary>
        /// Standard dispose method of the <seealso cref="IDisposable" /> interface.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Source: http://www.codeproject.com/Articles/15360/Implementing-IDisposable-and-the-Dispose-Pattern-P
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            mDisposed = true;
        }
        #endregion methods
    }
}

namespace Settings.ProgramSettings
{
    using Settings.Interfaces;
    using SettingsModel.Interfaces;
    using SettingsModel.Models;

    internal class OptionsPanel : IOptionsPanel
    {
        private IEngine _Query = null;

        public OptionsPanel()
        {
            _Query = Factory.CreateEngine();
        }

        /// <summary>
        /// Gets the options <seealso cref="IEngine"/> used to manage program options.
        /// </summary>
        public IEngine Options
        {
            get
            {
                return _Query;
            }

            private set
            {
                _Query = value;
            }
        }
    }
}

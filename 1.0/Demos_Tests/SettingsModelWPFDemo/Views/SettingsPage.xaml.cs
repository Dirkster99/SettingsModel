namespace SettingsModelWPFDemo.Views
{
    using FirstFloor.ModernUI.Windows;
    using SettingsModelWPFDemo.ViewModels;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : UserControl, IContent
    {

        public SettingsPage()
        {
            InitializeComponent();

            DataContextChanged += SettingsPage_DataContextChanged;
        }

        void SettingsPage_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var vm = e.NewValue as LoadUnloadCommands;

            if (vm != null)
            {
                DataContextChanged -= SettingsPage_DataContextChanged;
                vm.LoadedCommand();
                DataContextChanged += SettingsPage_DataContextChanged;
            }
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            ////Console.WriteLine("OnFragmentNavigation");
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            ////Console.WriteLine("OnNavigatedFrom");
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            //// This is implemented with DataContext == null on first event ever so we use DataContextChanged instead
            ////Console.WriteLine("OnNavigatedTo with datacontext == null");

            if (DataContext != null)
            {
                var vm = DataContext as LoadUnloadCommands;

                if (vm != null)
                    vm.LoadedCommand();
            }
        }

        /// <summary>
        /// https://github.com/firstfloorsoftware/mui/wiki/Handle-navigation-events
        /// </summary>
        /// <param name="e"></param>
        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            var vm = DataContext as LoadUnloadCommands;

            if (vm != null)
                vm.UnloadedCommand();
        }
    }
}

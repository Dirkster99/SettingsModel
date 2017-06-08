namespace SettingsModelWPFDemo.Behavior
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Source:
    /// http://stackoverflow.com/questions/1034374/drag-and-drop-in-mvvm-with-scatterview
    /// http://social.msdn.microsoft.com/Forums/de-DE/wpf/thread/21bed380-c485-44fb-8741-f9245524d0ae
    /// 
    /// Attached behaviour to implement the Text changed event via delegate command binding or routed commands.
    /// </summary>
    public static class TextBoxKeyEntered
    {
        // Field of attached ICommand property
        private static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(TextBoxKeyEntered),
                new PropertyMetadata(null, OnCommandChange));

        /// <summary>
        /// Setter method of the attached DropCommand <seealso cref="ICommand"/> property
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value"></param>
        public static void SetCommand(DependencyObject source, ICommand value)
        {
            source.SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Getter method of the attached DropCommand <seealso cref="ICommand"/> property
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ICommand GetCommand(DependencyObject source)
        {
            return (ICommand)source.GetValue(CommandProperty);
        }

        /// <summary>
        /// This method is hooked in the definition of the <seealso cref="DropCommandProperty"/>.
        /// It is called whenever the attached property changes - in our case the event of binding
        /// and unbinding the property to a sink is what we are looking for.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnCommandChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = d as TextBox;	                // Remove the handler if it exist to avoid memory leaks
            uiElement.KeyUp -= uiElement_KeyUp;

            var command = e.NewValue as ICommand;
            if (command != null)
            {
                // the property is attached so we attach the Drop event handler
                uiElement.KeyUp += uiElement_KeyUp;
            }
        }

        /// <summary>
        /// This method is called when the registered event occurs. The sender should be the control
        /// on which this behaviour is attached - so we convert the sender into a <seealso cref="UIElement"/>
        /// and receive the Command through the <seealso cref="GetCommand"/> getter listed above.
        /// 
        /// The <paramref name="e"/> parameter contains the standard data,
        /// which is unpacked and send upon the bound command.
        /// 
        /// This implementation supports binding of delegate commands and routed commands.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void uiElement_KeyUp(object sender, KeyEventArgs e)
        {
            // Filter for enter key events here
            if (e.Key != Key.Enter)
                return;

            // Sender should be this class or a descendent of it
            var fwElement = sender as TextBox;

            // Sanity check just in case this was somehow send by something else
            if (fwElement == null)
                return;

            // Handle right mouse click event if there is a command attached for this
            ICommand command = GetCommand(fwElement);

            object commandParameter = fwElement.Text;

            if (command != null)
            {
                // Check whether this attached behaviour is bound to a RoutedCommand
                if (command is RoutedCommand)
                {
                    // Execute the routed command
                    (command as RoutedCommand).Execute(commandParameter, fwElement);
                    e.Handled = true;
                }
                else
                {
                    // Execute the Command as bound delegate
                    command.Execute(commandParameter);
                    e.Handled = true;
                }
            }
        }
    }
}

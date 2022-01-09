using ArbreSoft.DietManager.Presentation.Commands;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ArbreSoft.DietManager.Presentation.Controls
{
    /// <summary>
    /// Interaction logic for SearchTextBox.xaml
    /// </summary>
    public partial class SearchTextBox : UserControl
    {
        public SearchTextBox()
        {
            InitializeComponent();
        }

        public event EventHandler<string> QueryChanged;

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(SearchTextBox), new FrameworkPropertyMetadata(string.Empty));

        public string Text
        {
            get { return GetValue(TextProperty).ToString(); }
            set { SetValue(TextProperty, value); }
        }

        public ICommand ClearSearch => new Command(sender => OnClearSearch(sender));

        private void OnClearSearch(object sender)
        {
            (sender as TextBox).Text = string.Empty;
            QueryChanged?.Invoke(this, string.Empty);
        }

        private void SearchBox_KeyUp(object sender, KeyboardEventArgs eventArgs)
        {
            QueryChanged?.Invoke(this, (sender as TextBox)?.Text);
        }
    }
}

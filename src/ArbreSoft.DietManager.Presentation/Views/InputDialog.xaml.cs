using System.Windows;

namespace ArbreSoft.DietManager.Presentation.Views
{
    /// <summary>
    /// Interaction logic for InputDialog.xaml
    /// </summary>
    public partial class InputDialog : Window
    {
        public InputDialog(string dialogTitle, string labelText)
        {
            InitializeComponent();
            Title = dialogTitle;
            Label.Text = labelText;
        }

        public string ResponseText
        {
            get { return ResponseTextBox.Text; }
            set { ResponseTextBox.Text = value; }
        }


        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}

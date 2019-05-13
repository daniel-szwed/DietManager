using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DietManager.Controls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class FloatInput : UserControl
    {
        public FloatInput()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(String), typeof(FloatInput), new FrameworkPropertyMetadata(string.Empty));

        public String Text
        {
            get { return GetValue(TextProperty).ToString(); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(String), typeof(FloatInput), new FrameworkPropertyMetadata(string.Empty));

        public String Label
        {
            get { return GetValue(LabelProperty).ToString(); }
            set { SetValue(LabelProperty, value); }
        }

        private void CustomTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            textBox?.SelectAll();
        }

        private void CustomTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!(e.Key == Key.Decimal || e.Key == Key.OemComma))
                return;
            var textBox = sender as TextBox;
            textBox.Text = textBox.Text.Replace(',','.');
            textBox.CaretIndex = textBox.Text.Length;
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;

namespace ArbreSoft.DietManager.Presentation.Controls
{
    /// <summary>
    /// Interaction logic for StringInput.xaml
    /// </summary>
    public partial class StringInput : UserControl
    {
        public StringInput()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(String), typeof(StringInput), new FrameworkPropertyMetadata(string.Empty));

        public String Text
        {
            get { return GetValue(TextProperty).ToString(); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(String), typeof(StringInput), new FrameworkPropertyMetadata(string.Empty));

        public String Label
        {
            get { return GetValue(LabelProperty).ToString(); }
            set { SetValue(LabelProperty, value); }
        }
    }
}

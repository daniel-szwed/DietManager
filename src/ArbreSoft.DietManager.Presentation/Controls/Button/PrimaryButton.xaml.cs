using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ArbreSoft.DietManager.Presentation.Controls.Button
{
    /// <summary>
    /// Interaction logic for Button.xaml
    /// </summary>
    public partial class PrimaryButton : UserControl
    {
        public PrimaryButton()
        {
            InitializeComponent();

            Foreground = Brushes.White;
        }

        public static DependencyProperty TextProperty = 
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(PrimaryButton), new FrameworkPropertyMetadata(string.Empty));
        public static DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(string), typeof(PrimaryButton), new FrameworkPropertyMetadata(string.Empty));
        public static DependencyProperty IconVisibilityProperty =
            DependencyProperty.Register(nameof(IconVisibility), typeof(Visibility), typeof(PrimaryButton), new FrameworkPropertyMetadata(Visibility.Visible));
        public static DependencyProperty IconSizeProperty =
            DependencyProperty.Register(nameof(IconSize), typeof(int), typeof(PrimaryButton), new FrameworkPropertyMetadata(24));
        public static DependencyProperty ClickCommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(PrimaryButton), new FrameworkPropertyMetadata(default(ICommand)));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string Icon
        {
            get => (string)(GetValue(IconProperty));
            set
            {
                SetValue(IconProperty, value);
                if (string.IsNullOrEmpty(value))
                {
                    IconVisibility = Visibility.Collapsed;
                    IconSize = 0;
                }
                else
                {
                    IconVisibility = Visibility.Visible;
                    IconSize = 24;
                }
            }
        }

        public Visibility IconVisibility
        {
            get => (Visibility)(GetValue(IconVisibilityProperty));
            set => SetValue(IconVisibilityProperty, value); 
        }

        public int IconSize
        {
            get => (int)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }

        public event RoutedEventHandler Click;

        public void Button_Click(object sender, RoutedEventArgs args)
        {
            if(Click != null)
            {
                Click(sender, args);
            }
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(ClickCommandProperty);
            set => SetValue(ClickCommandProperty, value);
        }
    }
}

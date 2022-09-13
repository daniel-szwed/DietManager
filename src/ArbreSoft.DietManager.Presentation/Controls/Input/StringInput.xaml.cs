using ArbreSoft.DietManager.Presentation.Validators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ArbreSoft.DietManager.Presentation.Controls.Input
{
    /// <summary>
    /// Interaction logic for StringInput.xaml
    /// </summary>
    public partial class StringInput : UserControl, INotifyDataErrorInfo
    {
        public StringInput()
        {
            InitializeComponent();
            IsValid = true;
        }

        public static readonly DependencyProperty LabelProperty = 
            DependencyProperty.Register(nameof(Label), typeof(string), typeof(StringInput), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty ValueProperty = 
            DependencyProperty.Register(nameof(Value), typeof(string), typeof(StringInput), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty ValidatorsProperty = 
            DependencyProperty.Register(nameof(Validators), typeof(IEnumerable<IValidator>), typeof(StringInput), new FrameworkPropertyMetadata(Enumerable.Empty<IValidator>()));
        public static readonly DependencyProperty ErrorsMarginProperty =
            DependencyProperty.Register(nameof(ErrorsMargin), typeof(Thickness), typeof(StringInput), new FrameworkPropertyMetadata(default(Thickness)));
        public static readonly DependencyProperty IsValidProperty = 
            DependencyProperty.Register(nameof(IsValid), typeof(bool), typeof(StringInput), new FrameworkPropertyMetadata(true));

        public string Label
        {
            get => GetValue(LabelProperty).ToString();
            set => SetValue(LabelProperty, value); 
        }

        public string Value
        {
            get => GetValue(ValueProperty).ToString();
            set => SetValue(ValueProperty, value);
        }

        public IEnumerable<IValidator> Validators
        {
            get => (IEnumerable<IValidator>)GetValue(ValidatorsProperty);
            set => SetValue(ValidatorsProperty, value);
        }

        public Thickness ErrorsMargin
        {
            get => (Thickness)GetValue(MarginProperty);
            set => SetValue(MarginProperty, value);
        }

        public bool IsValid
        {
            get => (bool)(GetValue(IsValidProperty));
            set => SetValue(IsValidProperty, value); 
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Validate(nameof(Value), (sender as TextBox).Text);
        }

        #region DataErrorInfo stuff
        private readonly Dictionary<string, ICollection<string>> _propertyNameToErrors = new();
        public bool HasErrors => _propertyNameToErrors.Values.Any(errors => errors.Any());
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (_propertyNameToErrors.TryGetValue(propertyName, out var errors))
            {
                return errors;
            }

            return Enumerable.Empty<string>();
        }

        private void Validate(string propertyName, string value)
        {
            ClearErrors(propertyName);
            RunValidators(propertyName, value);
            if (_propertyNameToErrors[propertyName].Any())
            {
                ErrorsMargin = new Thickness(0, 0, 0, 16 * _propertyNameToErrors[propertyName].Count());
                IsValid = false;
            }
            else
            {
                ErrorsMargin = default(Thickness);
                IsValid = true;
            }
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void ClearErrors(string propertyName)
        {
            if (_propertyNameToErrors.ContainsKey(propertyName))
            {
                _propertyNameToErrors[propertyName].Clear();
            }
            else
            {
                _propertyNameToErrors.Add(propertyName, new HashSet<string>());
            }
        }

        private void RunValidators(string propertyName, string value)
        {
            foreach (var validator in Validators ?? Enumerable.Empty<IValidator>())
            {
                var result = validator.Validate(value);
                if (!string.IsNullOrEmpty(result))
                {
                    _propertyNameToErrors[propertyName].Add(result);
                }
            }
        }
        #endregion
    }
}

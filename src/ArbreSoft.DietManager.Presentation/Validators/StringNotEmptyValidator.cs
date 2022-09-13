using System;

namespace ArbreSoft.DietManager.Presentation.Validators
{
    public class StringNotEmptyValidator : IValidator
    {
        public bool IsValid(object value) => !IsInvalid(value);

        public string Validate(object value)
        {
            if (string.IsNullOrEmpty(value.ToString()) || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return "Value can not be empty";
            }

            return null;
        }

        private bool IsInvalid(object value) =>
            string.IsNullOrEmpty(value.ToString())
            || string.IsNullOrWhiteSpace(value.ToString());
    }
}

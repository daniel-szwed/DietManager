namespace ArbreSoft.DietManager.Presentation.Validators
{
    public class FloatParseValidator : IValidator
    {
        public bool IsValid(object value) => float.TryParse(value.ToString(), out _);

        public string Validate(object value)
        {
            if (!IsValid(value))
            {
                return "Value must be a number";
            }

            return null;
        }
    }
}

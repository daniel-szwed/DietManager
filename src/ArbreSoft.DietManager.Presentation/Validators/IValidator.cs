namespace ArbreSoft.DietManager.Presentation.Validators
{
    public interface IValidator
    {
        bool IsValid(object value);
        string Validate(object value);
    }
}

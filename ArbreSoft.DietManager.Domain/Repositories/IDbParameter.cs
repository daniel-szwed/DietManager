namespace ArbreSoft.DietManager.Domain.Repositories
{
    public interface IDbParameter
    {
        public string ParameterName { get; }
        public object Value { get; }
    }
}

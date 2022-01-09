using ArbreSoft.DietManager.Domain.Repositories;

namespace ArbreSoft.DietManager.Application
{
    public class DbParamerer : IDbParameter
    {
        public DbParamerer(string ParameterName, object Value)
        {
            this.ParameterName = ParameterName;
            this.Value = Value;
        }

        public string ParameterName { get; }
        public object Value { get; }
    }
}

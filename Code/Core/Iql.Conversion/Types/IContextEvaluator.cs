namespace Iql.Data.Evaluation
{
    public interface IContextEvaluator
    {
        ResolvedValue ResolveVariable(string path);
        ResolvedValue ResolveProperty(object entity, string propertyName);
    }

    public class ResolvedValue
    {
        public object Value { get; }
        public bool Success { get; }
        public bool CanBeLiteral { get; }

        public ResolvedValue(object value, bool success, bool canBeLiteral = true)
        {
            Value = value;
            Success = success;
            CanBeLiteral = canBeLiteral;
        }
    }
}
namespace Iql.Data.Evaluation
{
    public interface IContextEvaluator
    {
        object ResolveVariable(string path);
        object ResolveProperty(object entity, string propertyName);
    }
}
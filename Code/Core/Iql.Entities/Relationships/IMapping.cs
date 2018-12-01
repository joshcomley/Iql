namespace Iql.Entities.Relationships
{
    public interface IMapping<T>
    {
        T Container { get; set; }
        IqlLambdaExpression Expression { get; set; }
        bool UseForFiltering { get; set; }
    }
}
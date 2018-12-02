namespace Iql.Entities.Relationships
{
    public interface IMapping<T>
    {
        IRelationshipDetail Container { get; }
        T Property { get; set; }
        IqlLambdaExpression Expression { get; set; }
        bool UseForFiltering { get; set; }
    }
}
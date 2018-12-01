namespace Iql.Entities.Relationships
{
    public interface IMapping<T>
    {
        T Container { get; set; }
        IqlExpression Expression { get; set; }
    }
}
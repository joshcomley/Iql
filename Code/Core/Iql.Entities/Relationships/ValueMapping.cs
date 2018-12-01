namespace Iql.Entities.Relationships
{
    public class ValueMapping : IMapping<IProperty>
    {
        public IProperty Container { get; set; }
        public IqlExpression Expression { get; set; }
    }
}
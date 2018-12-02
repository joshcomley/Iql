namespace Iql.Entities.Relationships
{
    public class ValueMapping : Mapping<IProperty>
    {
        public ValueMapping(IRelationshipDetail container) : base(container)
        {
        }

        public override void SetValue(object entity, object value)
        {
            Property.SetValue(entity, value);
        }
    }
}
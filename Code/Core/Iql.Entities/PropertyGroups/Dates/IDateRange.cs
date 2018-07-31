namespace Iql.Entities.PropertyGroups.Dates
{
    public interface IDateRange : IPropertyGroup, IConfigurable<IDateRange>
    {
        IProperty StartDateProperty { get; set; }
        IProperty EndDateProperty { get; set; }
        DateRangePropertyKind GetPropertyKind(IProperty property);
    }
}
using System.Linq;

namespace Iql.Entities.PropertyGroups.Dates
{
    public class DateRange : PropertyGroupBase<IDateRange>, IDateRange
    {
        public IProperty StartDateProperty { get; set; }
        public IProperty EndDateProperty { get; set; }
        public override IEntityConfiguration EntityConfiguration =>
            (StartDateProperty ?? EndDateProperty)?.EntityConfiguration;

        public DateRange(IProperty startDateProperty, IProperty endDateProperty, string key = null) : base(null, key)
        {
            StartDateProperty = startDateProperty;
            EndDateProperty = endDateProperty;
        }

#if !TypeScript
        public DateRange() : base(null, null)
        {
        }
#endif

        public override PropertyKind Kind
        {
            get => PropertyKind.SimpleCollection;
            set { }
        }

        public DateRangePropertyKind GetPropertyKind(IProperty property)
        {
            if (property == StartDateProperty)
            {
                return DateRangePropertyKind.StartDate;
            }

            if (property == EndDateProperty)
            {
                return DateRangePropertyKind.EndDate;
            }

            return DateRangePropertyKind.None;
        }

        public override IPropertyGroup[] GetGroupProperties()
        {
            return new[] {StartDateProperty, EndDateProperty}.Where(p => !Equals(null, p)).ToArray();
        }
    }
}
using System.Linq;

namespace Iql.Entities.PropertyGroups.Dates
{
    public class DateRange : SimplePropertyGroupBase<IDateRange>, IDateRange
    {
        protected override string ResolveName()
        {
            var parts = new[] {StartDateProperty?.Name, EndDateProperty?.Name}.Where(_ =>
                !string.IsNullOrWhiteSpace(_)).ToArray();
            return parts.Length == 0
                ? "DateRange"
                : string.Join(" - ", parts);
        }

        public IProperty StartDateProperty { get; set; }
        public IProperty EndDateProperty { get; set; }
        public override IqlPropertyGroupKind GroupKind { get; } = IqlPropertyGroupKind.DateRange;

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

        public override PropertyGroupMetadata[] GetPropertyGroupMetadata()
        {
            return new PropertyGroupMetadata[]
            {
                new PropertyGroupMetadata(StartDateProperty, PropertySearchKind.None),
                new PropertyGroupMetadata(EndDateProperty, PropertySearchKind.None),
            };
        }

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
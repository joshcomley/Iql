using System.Linq;

namespace Iql.Entities.Geography
{
    public class GeographicPoint : SimplePropertyGroupBase<IGeographicPoint>, IGeographicPoint
    {
        protected override string ResolveName()
        {
            return "Location";
        }

        public override PropertyKind Kind
        {
            get => PropertyKind.SimpleCollection;
            set { }
        }

        public override IqlPropertyGroupKind GroupKind { get; } = IqlPropertyGroupKind.GeographicPoint;
        public override IEntityConfiguration EntityConfiguration => (LongitudeProperty ?? LatitudeProperty)?.EntityConfiguration;
        public IProperty LongitudeProperty { get; set; }
        public IProperty LatitudeProperty { get; set; }

        public GeographicPoint(IProperty longitudeProperty = null, IProperty latitudeProperty = null, string key = null) : base(null, key)
        {
            LongitudeProperty = longitudeProperty;
            LatitudeProperty = latitudeProperty;
        }

#if !TypeScript
        public GeographicPoint() : base(null, null)
        {
        }
#endif

        public override PropertyGroupMetadata[] GetPropertyGroupMetadata()
        {
            return new PropertyGroupMetadata[]
            {
                new PropertyGroupMetadata(LongitudeProperty, PropertySearchKind.None),
                new PropertyGroupMetadata(LatitudeProperty, PropertySearchKind.None),
            };
        }

        public override IPropertyGroup[] GetGroupProperties()
        {
            return new[] { LongitudeProperty, LatitudeProperty }.Where(_ => _ != null).ToArray();
        }
    }
}
﻿using System.Linq;

namespace Iql.Entities.Geography
{
    public class GeographicPoint : SimplePropertyGroupBase<IGeographicPoint>, IGeographicPoint
    {
        public override IProperty PrimaryProperty
        {
            get { return LatitudeProperty; }
        }

        protected override string ResolveName()
        {
            return "Location";
        }

        public override IqlPropertyKind Kind
        {
            get => IqlPropertyKind.SimpleCollection;
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
                new PropertyGroupMetadata(LongitudeProperty, IqlPropertySearchKind.None),
                new PropertyGroupMetadata(LatitudeProperty, IqlPropertySearchKind.None),
            };
        }

        public override IPropertyGroup[] GetGroupProperties()
        {
            return new[] { LongitudeProperty, LatitudeProperty }.Where(_ => _ != null).ToArray();
        }
    }
}
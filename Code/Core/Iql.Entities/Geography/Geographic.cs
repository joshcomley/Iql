namespace Iql.Entities.Geography
{
    public class Geographic : PropertyGroupBase<IGeographic>, IGeographic
    {
        public override PropertyKind Kind
        {
            get => PropertyKind.SimpleCollection;
            set { }
        }
        public override IEntityConfiguration EntityConfiguration => (LongitudeProperty ?? LatitudeProperty)?.EntityConfiguration;
        public IProperty LongitudeProperty { get; set; }
        public IProperty LatitudeProperty { get; set; }

        public Geographic(IProperty longitudeProperty = null, IProperty latitudeProperty = null, string key = null) : base(null, key)
        {
            LongitudeProperty = longitudeProperty;
            LatitudeProperty = latitudeProperty;
        }

#if !TypeScript
        public Geographic() : base(null, null)
        {
        }
#endif
        public override IPropertyGroup[] GetGroupProperties()
        {
            return new[] { LongitudeProperty, LatitudeProperty };
        }
    }
}
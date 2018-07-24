namespace Iql.Entities.Geography
{
    public class Geographic : PropertyGroupBase<Geographic>, IGeographic
    {
        public override PropertyKind Kind
        {
            get => PropertyKind.SimpleCollection;
            set { }
        }
        public override IEntityConfiguration EntityConfiguration => (LongitudeProperty ?? LatitudeProperty)?.EntityConfiguration;
        public string Key { get; set; }
        public IProperty LongitudeProperty { get; set; }
        public IProperty LatitudeProperty { get; set; }

        public Geographic(IProperty longitudeProperty = null, IProperty latitudeProperty = null, string key = null):base(null)
        {
            LongitudeProperty = longitudeProperty;
            LatitudeProperty = latitudeProperty;
            Key = key;
        }

#if !TypeScript
        public Geographic():base(null)
        {
        }
#endif
        public override IPropertyGroup[] GetProperties()
        {
            return new[] { LongitudeProperty, LatitudeProperty };
        }
    }
}
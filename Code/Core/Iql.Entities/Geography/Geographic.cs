namespace Iql.Entities.Geography
{
    public class Geographic : IGeographic
    {
        public IEntityConfiguration EntityConfiguration => (LongitudeProperty ?? LatitudeProperty)?.EntityConfiguration;
        public string Key { get; set; }
        public IProperty LongitudeProperty { get; set; }
        public IProperty LatitudeProperty { get; set; }

        public Geographic(IProperty longitudeProperty = null, IProperty latitudeProperty = null, string key = null)
        {
            LongitudeProperty = longitudeProperty;
            LatitudeProperty = latitudeProperty;
            Key = key;
        }

#if !TypeScript
        public Geographic()
        {

        }
#endif
        public IPropertyGroup[] GetProperties()
        {
            return new[] { LongitudeProperty, LatitudeProperty };
        }
    }
}
namespace Iql.Entities.Geography
{
    public interface IGeographic
    {
        IEntityConfiguration EntityConfiguration { get; }
        string Key { get; set; }
        IProperty LongitudeProperty { get; set; }
        IProperty LatitudeProperty { get; set; }
    }
}
namespace Iql.Entities
{
    public interface IPropertyGroup : IEntityConfigurationItem
    {
        IPropertyGroup[] GetProperties();
    }
    //public interface IPropertyGrouping
    //{
    //    IProperty[] GetProperties();
    //}
}
using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;

namespace Iql.Entities
{
    public interface IPropertyGroup : IConfiguration, IPropertyContainer
    {
        PropertyKind Kind { get; set; }
        IRuleCollection<IBinaryRule> ValidationRules { get; set; }
        IRuleCollection<IDisplayRule> DisplayRules { get; set; }
    }

    public interface IPropertyContainer : IMetadata, IEntityConfigurationItem
    {
        IPropertyGroup[] GetGroupProperties();
    }

    //public interface IPropertyGrouping
    //{
    //    IProperty[] GetProperties();
    //}
}
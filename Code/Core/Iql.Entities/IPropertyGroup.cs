using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;

namespace Iql.Entities
{
    public interface IPropertyGroup : IEntityConfigurationItem, IConfiguration
    {
        IRuleCollection<IBinaryRule> ValidationRules { get; set; }
        IRuleCollection<IDisplayRule> DisplayRules { get; set; }
        IPropertyGroup[] GetProperties();
    }
    //public interface IPropertyGrouping
    //{
    //    IProperty[] GetProperties();
    //}
}
using System.Collections.Generic;
using Iql.Entities.DisplayFormatting;
using Iql.Entities.Relationships;
using Iql.Entities.Rules;

namespace Iql.Entities
{
    public interface IEntityMetadata : IMetadata
    {
        IDisplayFormatting DisplayFormatting { get; }
        IRuleCollection<IBinaryRule> EntityValidation { get; }
        IEntityKey Key { get; }
        string TitlePropertyName { get; set; }
        string PreviewPropertyName { get; set; }
        EntityManageKind ManageKind { get; set; }
        string SetFriendlyName { get; set; }
        string SetName { get; set; }
        bool SetNameSet { get; }
        string DefaultSortExpression { get; set; }
        bool DefaultSortDescending { get; set; }
        List<string> PropertyOrder { get; set; }
        IList<IProperty> Properties { get; set; }
        List<IRelationship> Relationships { get; set; }
    }
}
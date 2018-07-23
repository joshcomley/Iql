using Iql.Entities.DisplayFormatting;
using Iql.Entities.Geography;
using Iql.Entities.Relationships;
using Iql.Entities.Rules;
using System.Collections.Generic;
using Iql.Entities.NestedSets;

namespace Iql.Entities
{
    public interface IEntityMetadata : IMetadata
    {
        IList<IGeographic> Geographics { get; set; }
        IList<INestedSet> NestedSets { get; set; }
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
        IList<IPropertyGroup> PropertyOrder { get; set; }
        IList<IProperty> Properties { get; set; }
        IList<IRelationship> Relationships { get; set; }
    }
}
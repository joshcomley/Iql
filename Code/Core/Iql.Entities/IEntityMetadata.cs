using System.Collections.Generic;

namespace Iql.Entities
{
    public interface IEntityMetadata : IMetadata
    {
        string TitlePropertyName { get; set; }
        string PreviewPropertyName { get; set; }
        EntityManageKind ManageKind { get; set; }
        string SetFriendlyName { get; set; }
        string SetName { get; set; }
        string ResolveSetFriendlyName();
        string ResolveSetName();
        string DefaultSortExpression { get; set; }
        bool DefaultSortDescending { get; set; }
        List<string> PropertyOrder { get; set; }
    }
}
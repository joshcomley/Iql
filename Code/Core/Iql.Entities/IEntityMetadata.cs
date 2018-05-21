using System.Collections.Generic;

namespace Iql.Entities
{
    public interface IEntityMetadata : IMetadata
    {
        EntityManageKind ManageKind { get; set; }
        string SetFriendlyName { get; set; }
        string SetName { get; set; }
        string ResolveSetFriendlyName();
        string ResolveSetName();
        List<string> PropertyOrder { get; set; }
    }
}
﻿namespace Iql.Data.Configuration
{
    public interface IEntityMetadata : IMetadata
    {
        EntityManageKind ManageKind { get; set; }
        string SetFriendlyName { get; set; }
        string SetName { get; set; }
        string ResolveSetFriendlyName();
        string ResolveSetName();
    }
}
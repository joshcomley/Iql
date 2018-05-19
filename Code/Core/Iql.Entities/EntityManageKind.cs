using System;

namespace Iql.Data.Configuration
{
    [Flags]
    public enum EntityManageKind
    {
        None = 1,
        Read = 2 << 0,
        List = Read | 2 << 1,
        New = 2 << 2,
        Update = 2 << 3,
        Delete = 2 << 4,
        Full = List | New | Update | Delete,
        NoDelete = List | New | Update
    }
}
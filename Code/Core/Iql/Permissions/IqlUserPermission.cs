using System;

namespace Iql.Entities.Permissions
{
    [Flags]
    public enum IqlUserPermission
    {
        Unset = 0,
        None = 1,
        Read = 2,
        Create = 4,
        Update = 8,
        Delete = 16,
        Full = Read | Create | Update | Delete,
        ReadAndUpdate = Read | Update,
        CreateAndReadAndUpdate = Create | Read | Update,
    }
}
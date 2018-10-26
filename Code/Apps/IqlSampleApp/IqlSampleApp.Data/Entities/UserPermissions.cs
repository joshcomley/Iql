using System;

namespace IqlSampleApp.Data.Entities
{
    [Flags]
    public enum UserPermissions
    {
        Read = 1,
        Create = 2 << 0,
        Delete = 2 << 1,
        Edit = 2 << 2
    }
}

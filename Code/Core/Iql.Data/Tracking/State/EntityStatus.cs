using System;

namespace Iql.Data.Tracking.State
{
    public enum EntityStatus
    {
        Unattached = 0,
        New = 1,
        NewAndDeleted = 2,
        Existing = 10,
        ExistingAndPendingDelete = 11,
        ExistingAndDeleted = 12
    }
}
using System.Collections.Generic;

namespace Iql.Queryable.Data.Tracking
{
    public interface ITrackedEntity
    {
        object Entity { get; }
        List<ITrackedRelationship> TrackedRelationships { get; }
    }
}
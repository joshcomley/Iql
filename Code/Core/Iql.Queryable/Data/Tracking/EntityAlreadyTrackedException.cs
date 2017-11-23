using System;

namespace Iql.Queryable.Data.Tracking
{
    public class EntityAlreadyTrackedException : Exception
    {
        public EntityAlreadyTrackedException(string message) : base(message)
        {
            
        }
    }
}
using System;

namespace Iql.Queryable.Exceptions
{
    public class EntityAlreadyTrackedException : Exception
    {
        public EntityAlreadyTrackedException(string message) : base(message)
        {
            
        }
    }
}
using System;

namespace Iql.Data.Exceptions
{
    public class EntityAlreadyTrackedException : Exception
    {
        public EntityAlreadyTrackedException(string message) : base(message)
        {
            
        }
    }
}
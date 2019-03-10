using System;
using Iql.Events;

namespace Iql.Entities.Exceptions
{
    public class AttemptingToAssignRemotelyGeneratedKeyException : Exception
    {
        public AttemptingToAssignRemotelyGeneratedKeyException()
        {
            EventEmitterExceptions.EnsureIsThrown<AttemptingToAssignRemotelyGeneratedKeyException>();
        }
    }
}
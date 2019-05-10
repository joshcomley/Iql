using System;
using Iql.Events;

namespace Iql.Entities.Exceptions
{
    public class DuplicateKeyException : Exception
    {
        public DuplicateKeyException(string message = null) : base(message)
        {
            EventEmitterExceptions.EnsureIsThrown<DuplicateKeyException>();
        }
    }
}
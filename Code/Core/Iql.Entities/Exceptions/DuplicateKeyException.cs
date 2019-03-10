using System;
using Iql.Events;

namespace Iql.Entities.Exceptions
{
    public class DuplicateKeyException : Exception
    {
        public DuplicateKeyException()
        {
            EventEmitterExceptions.EnsureIsThrown<DuplicateKeyException>();
        }
    }
}
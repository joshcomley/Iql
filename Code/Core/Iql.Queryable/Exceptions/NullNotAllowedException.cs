using System;

namespace Iql.Queryable.Exceptions
{
    public class NullNotAllowedException : Exception
    {
        public NullNotAllowedException(Type type, string propertyName)
            : base($"Property \"{propertyName}\" on type \"{type.Name}\" does not allow nulls.")
        {
            
        }
    }
}
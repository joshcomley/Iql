using System;

namespace Iql.Queryable.Exceptions
{
    public class RelatedListHasNoValueException : Exception
    {
        public RelatedListHasNoValueException(string message = null) : base(message)
        {

        }
    }
}
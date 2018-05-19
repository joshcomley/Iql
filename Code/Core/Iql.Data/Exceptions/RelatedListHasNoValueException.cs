using System;

namespace Iql.Data.Exceptions
{
    public class RelatedListHasNoValueException : Exception
    {
        public RelatedListHasNoValueException(string message = null) : base(message)
        {

        }
    }
}
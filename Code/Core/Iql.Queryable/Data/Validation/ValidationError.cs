﻿namespace Iql.Queryable.Data.Validation
{
    public class ValidationError
    {
        public string Message { get; set; }

        public ValidationError(string message)
        {
            Message = message;
        }
    }
}
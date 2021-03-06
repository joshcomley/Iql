﻿using System.Diagnostics;

namespace Iql.Entities.Validation.Validation
{
    [DebuggerDisplay("{Message} - {Key}")]
    public class ValidationError
    {
        public string Key { get; }
        public string Message { get; set; }

        public ValidationError(string key, string message)
        {
            Key = key;
            Message = message;
        }
    }
}
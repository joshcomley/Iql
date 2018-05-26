using System;

namespace Iql.Entities.Sanitization
{
    public interface IEntitySanitizer
    {
        string Key { get; set; }
        Action<object> Run { get; set; }
    }
}
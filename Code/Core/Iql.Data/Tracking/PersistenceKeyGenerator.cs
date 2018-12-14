using System;

namespace Iql.Data.Tracking
{
    public class PersistenceKeyGenerator
    {
        public static Func<Guid> New { get; set; } = () => Guid.NewGuid();
    }
}
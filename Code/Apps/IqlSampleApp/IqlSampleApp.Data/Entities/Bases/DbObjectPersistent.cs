using System;

namespace Brandless.Data.Entities
{
    public class DbObjectPersistent : IHasPersistenceKey
    {
        public Guid PersistenceKey { get; set; }
    }
}
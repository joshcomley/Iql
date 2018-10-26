using System;
using Brandless.Data.Entities;

namespace IqlSampleApp.Data.Entities.Bases
{
    public class DbObjectPersistent : IHasPersistenceKey
    {
        public Guid PersistenceKey { get; set; }
    }
}
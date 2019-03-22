using System;

namespace Iql.Data.Contracts
{
    public interface IPersistenceKey
    {
        Guid PersistenceKey { get; set; }
    }
}
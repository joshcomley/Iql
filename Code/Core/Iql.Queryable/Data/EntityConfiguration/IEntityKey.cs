using System;
using System.Collections.Generic;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public interface IEntityKey
    {
        Type KeyType { get; set; }
        Type Type { get; set; }
        List<IProperty> Properties { get; set; }
        bool IsGeneratedRemotely { get; set; }
    }
}
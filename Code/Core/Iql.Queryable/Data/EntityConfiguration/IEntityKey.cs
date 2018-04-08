using System;
using System.Collections.Generic;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public interface IEntityKey
    {
        bool HasRelationshipKeys { get; }
        Type KeyType { get; set; }
        Type Type { get; set; }
        IList<IProperty> Properties { get; set; }
        bool IsGeneratedRemotely { get; set; }
        bool IsPivot();
    }
}
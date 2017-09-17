using System;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public interface IKeyProperty
    {
        string Name { get; }
        Type Type { get; }
    }
}
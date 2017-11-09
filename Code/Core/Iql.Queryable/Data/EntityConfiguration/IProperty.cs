using System;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public interface IProperty
    {
        string Name { get; }
        Type Type { get; }
        bool IsCollection { get; }
        Type DeclaringType { get; }
        string ConvertedFromType { get; }
    }
}
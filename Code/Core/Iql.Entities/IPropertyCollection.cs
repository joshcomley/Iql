using System;
using System.Collections.Generic;

namespace Iql.Entities
{
    public interface IPropertyCollection : IPropertyGroup, IConfigurable<IPropertyCollection>
    {
        bool Enclose { get; set; }
        ContentAlignment ContentAlignment { get; set; }
        List<IPropertyGroup> Properties { get; }
    }

    public interface IConfigurable<out T>
        where T : IConfigurable<T>
    {
        T Configure(Action<T> configure);
    }
}
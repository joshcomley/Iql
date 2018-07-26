using System;
using System.Collections.Generic;

namespace Iql.Entities
{
    public interface IPropertyCollection : IPropertyGroup, IConfigurableProperty<IPropertyCollection>
    {
        ContentAlignment ContentAlignment { get; set; }
        List<IPropertyGroup> Properties { get; }
    }

    public interface IConfigurableProperty<out T>
        where T : IConfigurableProperty<T>
    {
        T Configure(Action<T> configure);
    }
}
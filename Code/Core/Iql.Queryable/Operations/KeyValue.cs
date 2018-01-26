using System;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Operations
{
    public class KeyValue
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Type ValueType { get; set; }
        public KeyValue(string name, object value, Type valueType)
        {
            Name = name;
            Value = value;
            ValueType = valueType;
        }

        public bool IsDefaultValue()
        {
            var type = ValueType ?? Value?.GetType();
#if !TypeScript
            if (type == null)
            {
                return Equals(Value, null);
            }
            return Equals(Value, type.DefaultValue());
#else
            return Equals(Value, null) || Equals(Value, Activator.CreateInstance(type));
#endif
        }
    }
}
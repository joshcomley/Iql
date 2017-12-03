using System;

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
            return Equals(Value, null) || Equals(Value, Activator.CreateInstance(ValueType));
        }
    }
}
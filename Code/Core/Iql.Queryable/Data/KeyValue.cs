using System;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data
{
    public class KeyValue
    {
        private bool _hasDefaultValueSet;
        private bool _hasDefaultValue;
        private object _value;

        public bool HasDefaultValue
        {
            get
            {
                if (!_hasDefaultValueSet)
                {
                    _hasDefaultValueSet = true;
                    _hasDefaultValue = Value.IsDefaultValue();
                }

                return _hasDefaultValue;
            }
        }

        public string Name { get; set; }

        public object Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _hasDefaultValueSet = false;
                }
                _value = value;
            }
        }

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
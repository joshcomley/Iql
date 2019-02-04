#if TypeScript
using System;
#endif
using Iql.Conversion;
using Iql.Entities.Extensions;
using Newtonsoft.Json;

namespace Iql.Entities
{
    public class KeyValue : IJsonSerializable
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
                    _hasDefaultValue = Value.IsDefaultValue(ValueType);
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

        public ITypeDefinition ValueType { get; set; }
        public KeyValue(string name, object value, ITypeDefinition valueType)
        {
            Name = name;
            Value = value;
            ValueType = valueType;
        }

        public bool IsDefaultValue()
        {
            var type = ValueType?.ElementType ?? Value?.GetType();
#if !TypeScript
            if (type == null)
            {
                return Equals(Value, null);
            }
            return Equals(Value, ValueType.DefaultValue());
#else
            return Equals(Value, null) || Equals(Value, Activator.CreateInstance(type));
#endif
        }

        public string SerializeToJson()
        {
            return JsonConvert.SerializeObject(PrepareForJson());
        }

        public object PrepareForJson()
        {
            return new
            {
                Name,
                Value
            };
        }
    }
}
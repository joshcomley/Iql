using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using Iql.Entities;
using Iql.Entities.InferredValues;

namespace Iql.Server.Serialization.Deserialization.EntityConfiguration
{
    [DebuggerDisplay("{Name}")]
    public class Property : PropertyBase, IProperty
    {
        public bool IsCount { get; }
        public override Func<object, object> GetValue { get; set; }
        public override Func<object, object, object> SetValue { get; set; }
        public Dictionary<string, object> CustomInformation { get; }
        public IProperty IsInferredWithExpression(LambdaExpression expression, bool onlyIfNew = false, InferredValueKind kind = InferredValueKind.Always, bool canOverride = false)
        {
            SetInferredWithExpression(expression);
            return this;
        }

        public override IqlPropertyGroupKind GroupKind { get; } = IqlPropertyGroupKind.Primitive;
        public override PropertyGroupMetadata[] GetPropertyGroupMetadata()
        {
            throw new NotImplementedException();
        }

        public override PropertySearchKind SearchKind { get; set; }
    }
}
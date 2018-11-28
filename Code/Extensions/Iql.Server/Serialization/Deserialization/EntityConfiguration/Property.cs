using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using Iql.Entities;

namespace Iql.Server.Serialization.Deserialization.EntityConfiguration
{
    [DebuggerDisplay("{Name}")]
    public class Property : PropertyBase, IProperty
    {
        public IqlExpression InferredWithIql { get; set; }
        public override Func<object, object> GetValue { get; set; }
        public override Func<object, object, object> SetValue { get; set; }
        public Dictionary<string, object> CustomInformation { get; }
        public IProperty IsInferredWithExpression(LambdaExpression expression)
        {
            SetInferredWithExpression(expression);
            return this;
        }
    }
}
using Iql.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Iql.Server.Serialization
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
            InferredWith = expression;
            return this;
        }
    }
}
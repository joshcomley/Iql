﻿using System;
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
        public override Func<object, object> GetValue { get; set; }
        public override Func<object, object, object> SetValue { get; set; }
        public Dictionary<string, object> CustomInformation { get; }
        public IProperty IsInferredWithExpression(LambdaExpression expression, bool onlyIfNew = false, InferredValueMode mode = InferredValueMode.Always, bool canOverride = false)
        {
            SetInferredWithExpression(expression);
            return this;
        }
    }
}
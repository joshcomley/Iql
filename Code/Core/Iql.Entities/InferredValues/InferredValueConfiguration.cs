﻿using System.Linq.Expressions;
using Iql.Conversion;

namespace Iql.Entities.InferredValues
{
    public class InferredValueConfiguration : IInferredValueConfiguration
    {
        public InferredValueConfiguration(IPropertyMetadata property = null)
        {
            Property = property;
        }
        public IPropertyMetadata Property { get; set; }
        public string Key { get; set; }
        public InferredValueKind Kind { get; set; } = InferredValueKind.Always;
        public bool CanOverride { get; set; }
        public bool ForNewOnly { get; set; }

        private LambdaExpression _inferredWithExpression;
        private LambdaExpression _inferredWithConditionExpression;
        private IqlExpression _inferredWithConditionIql;
        private IqlExpression _inferredWithIql;

        public IqlExpression InferredWithConditionIql
        {
            get
            {
                // Lazy convert the expression
                if (_inferredWithConditionExpression != null)
                {
                    _inferredWithConditionIql = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(
                        _inferredWithConditionExpression,
                        Property.EntityConfiguration.Builder, 
                        typeof(InferredValueContext<>).MakeGenericType(Property.EntityConfiguration.Type)).Expression;
                    _inferredWithConditionExpression = null;
                }
                return _inferredWithConditionIql;
            }
            set
            {
                _inferredWithConditionExpression = null;
                _inferredWithConditionIql = value;
            }
        }

        public IqlExpression InferredWithIql
        {
            get
            {
                // Lazy convert the expression
                if (_inferredWithExpression != null)
                {
                    _inferredWithIql = IqlConverter.Instance.ConvertLambdaExpressionToIqlByType(
                        _inferredWithExpression,
                        Property.EntityConfiguration.Builder,
                        typeof(InferredValueContext<>).MakeGenericType(Property.EntityConfiguration.Type)).Expression;
                    _inferredWithExpression = null;
                }
                return _inferredWithIql;
            }
            set
            {
                _inferredWithExpression = null;
                _inferredWithIql = value;
            }
        }

        public IInferredValueConfiguration SetInferredWithExpression(
            LambdaExpression value, bool onlyIfNew = false, InferredValueKind kind = InferredValueKind.Always, bool canOverride = false, params string[] onlyWhenPropertyChanges)
        {
            // This expression is lazy converted to IQL on demand
            _inferredWithExpression = value;
            ForNewOnly = onlyIfNew;
            Kind = kind;
            CanOverride = canOverride;
            OnPropertyChanges = onlyWhenPropertyChanges;
            return this;
        }

        public string[] OnPropertyChanges { get; set; }

        public IInferredValueConfiguration SetConditionallyInferredWithExpression(
            LambdaExpression expression, LambdaExpression condition)
        {
            _inferredWithExpression = expression;
            _inferredWithConditionExpression = condition;
            return this;
        }

        // Help avoid triggering lazy evaluation of inferred IQL expression
        public bool HasCondition => _inferredWithConditionExpression != null || _inferredWithConditionIql != null;
    }
}
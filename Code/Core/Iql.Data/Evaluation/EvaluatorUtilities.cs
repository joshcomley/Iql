using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Conversion;
using Iql.Data.Context;
using Iql.Data.DataStores.InMemory;
using Iql.Data.Evaluation;
using Iql.Data.Extensions;
using Iql.Data.IqlToIql;
using Iql.Data.Types;
using Iql.Entities;
using Iql.Entities.InferredValues;
using Iql.Entities.Permissions;
using Iql.Entities.Rules.Relationship;
using Iql.Entities.Services;
using Iql.Extensions;
using Iql.Parsing.Evaluation;
using Iql.Parsing.Types;

namespace Iql.Data
{
    class ProcessExpressionResult
    {
        public bool Success { get; }
        public IqlFlattenedExpression[] propertyExpressions { get; set; }
        public Dictionary<IqlPropertyPath, object> lookup { get; set; }
        public IqlExpression expression { get; set; }

        public ProcessExpressionResult(bool success, IqlFlattenedExpression[] propertyExpressions, Dictionary<IqlPropertyPath, object> lookup, IqlExpression expression)
        {
            Success = success;
            this.propertyExpressions = propertyExpressions;
            this.lookup = lookup;
            this.expression = expression;
        }
    }
    public class IqlExpressonEvaluatedResult
    {
        public IqlExpression Expression { get; }
        public IqlExpressonEvaluationResult RootResult { get; }
        public IqlPropertyPathEvaluationResult Result { get; }

        public IqlExpressonEvaluatedResult(
            IqlPropertyPathEvaluationResult result,
            IqlExpression expression,
            IqlExpressonEvaluationResult rootResult
            )
        {
            Result = result;
            Expression = expression;
            RootResult = rootResult;
        }
    }

    public class IqlExpressonEvaluationResult : IqlObjectEvaluationResult
    {
        public IqlPropertyPathEvaluationResult[] Paths { get; set; }

        public IqlExpressonEvaluationResult(
            bool success,
            object result,
            IEnumerable<IqlPropertyPathEvaluationResult> paths) : base(success, result)
        {
            Paths = paths.ToArray();
        }
    }

    public class IqlPropertyPathEvaluationResult
    {
        public bool Success { get; set; }
        public object Parent { get; }
        public IqlPropertyPath Source { get; }
        public IqlPropertyPathEvaluated[] Results { get; set; }
        public Func<IqlPropertyPathEvaluationResult, object> ResolveNull { get; set; }
        public object Value => Success ? Results.Last().Value : ResolveNull(this);

        public IqlPropertyPathEvaluationResult(
            bool success,
            object parent,
            IqlPropertyPath source,
            IqlPropertyPathEvaluated[] results,
            Func<IqlPropertyPathEvaluationResult, object> resolveNull = null
            )
        {
            Source = source;
            Results = results;
            ResolveNull = resolveNull ?? (_ => null);
            Success = success;
            Parent = parent;
        }
    }

    public class IqlPropertyPathEvaluated
    {
        public IqlPropertyPathEvaluationResult Result { get; }
        public IqlPropertyPath Path { get; set; }
        public object Parent { get; set; }
        public object Value { get; set; }
        public int PathLength { get; }
        public int Position { get; }
        public bool IsFinal => Position == PathLength - 1;
        public IqlPropertyPathEvaluated(
            IqlPropertyPathEvaluationResult result,
            IqlPropertyPath path,
            object parent,
            object value,
            int pathLength,
            int position)
        {
            Result = result;
            Path = path;
            Parent = parent;
            Value = value;
            PathLength = pathLength;
            Position = position;
        }
    }
    public static class ExpressionEvaluator
    {

    }
}
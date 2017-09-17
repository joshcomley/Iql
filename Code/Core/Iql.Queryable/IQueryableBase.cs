using System;
using System.Collections.Generic;
using Iql.Parsing;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public interface IQueryableBase
    {
        EvaluateContext EvaluateContext { get; }
        Type ItemType { get; }
        List<IQueryOperation> Operations { get; }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Parsing;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public interface IQueryableBase
    {
        bool HasDefaults { get; set; }
        IQueryableBase Copy();
        IQueryableBase New();
        EvaluateContext EvaluateContext { get; }
        Type ItemType { get; }
        List<IQueryOperation> Operations { get; }
        Task<IList> ToList();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Queryable;

namespace Iql.DotNet.Queryable
{
    public interface IDotNetQueryResult : IQueryResultBase
    {
        List<Func<IList, IList>> Actions { get; }
    }
}
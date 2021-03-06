﻿using System;
using System.Collections;

namespace Iql.Data.DataStores.InMemory
{
    public interface IInMemoryContext
    {
        IEnumerable SourceList { get; set; }
        IEnumerable ResolveSource(Type entityType);
        void AddMatches(Type type, IList matches);
        double DistanceBetween(IqlPointExpression left, IqlPointExpression right);
        bool Intersects(IqlPointExpression left, IqlPolygonExpression right);
    }
}
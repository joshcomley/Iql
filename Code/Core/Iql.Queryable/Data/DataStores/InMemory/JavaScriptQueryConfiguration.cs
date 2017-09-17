using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.DataStores.InMemory
{
    public class JavaScriptQueryConfiguration
    {
        private readonly Dictionary<string, LambdaExpression> _sources = new Dictionary<string, LambdaExpression>();

        public void RegisterSource<T>(Expression<Func<IList<T>>> getter)
        {
            _sources[typeof(T).Name] = getter;
        }

        public IList<T> GetSource<T>()
        {
            return GetSourceByName(typeof(T).Name) as IList<T>;
        }

        public IList GetSourceByName(string name)
        {
            return _sources[name].Compile().DynamicInvoke() as IList;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace Iql.Queryable.Data.Crud.Operations.Results
{
    public class FlattenedGetDataResult<T> : DataResult<T, Dictionary<Type, IList>>, IFlattenedGetDataResult
        where T : class
    {
        public FlattenedGetDataResult(
            Dictionary<Type, IList> data,
            GetDataOperation<T> operation,
            bool success) : base(data, operation, success)
        {
            Data = new Dictionary<Type, IList>();
        }

        private readonly Dictionary<object, bool> _matched = new Dictionary<object, bool>();

        IList IFlattenedGetDataResult.Root
        {
            get => Root;
            set => Root = (List<T>) value;
        }

        public void AddData<TEntity>(List<TEntity> matches)
        {
            var data = Data;
            var type = typeof(TEntity);
            if (!data.ContainsKey(type))
            {
                data.Add(type, matches);
                foreach (var match in matches)
                {
                    _matched.Add(match, true);
                }
            }
            else
            {
                var list = (List<TEntity>)data[type];
                for (var i = 0; i < matches.Count; i++)
                {
                    var match = matches[i];
                    if (!_matched.ContainsKey(match))
                    {
                        _matched.Add(match, true);
                        list.Add(match);
                    }
                }
            }
        }

        public void AddDataArray<TEntity>(TEntity[] matches)
        {
            var data = Data;
            var type = typeof(TEntity);
            if (!data.ContainsKey(type))
            {
                data.Add(type, matches);
                foreach (var match in matches)
                {
                    _matched.Add(match, true);
                }
            }
            else
            {
                var list = (List<TEntity>)data[type];
                for (var i = 0; i < matches.Length; i++)
                {
                    var match = matches[i];
                    if (!_matched.ContainsKey(match))
                    {
                        _matched.Add(match, true);
                        list.Add(match);
                    }
                }
            }
        }
    }
}
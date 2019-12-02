using System;
using System.Collections;
using System.Collections.Generic;

namespace Iql.Data.Crud.Operations.Results
{
    public class FlattenedGetDataResult<T> : DataResult<T, Dictionary<Type, IList>>, IFlattenedGetDataResult
        where T : class
    {
        public bool IsOffline { get; set; }
        public FlattenedGetDataResult(
            Dictionary<Type, IList> data,
            GetDataOperation<T> operation,
            bool success,
            RequestStatus requestStatus = RequestStatus.Online) : base(data, operation, success, requestStatus)
        {
            Data = new Dictionary<Type, IList>();
        }
        private bool _matchedDelayedInitialized;
        private Dictionary<object, bool> _matchedDelayed;

        private Dictionary<object, bool> _matched { get { if(!_matchedDelayedInitialized) { _matchedDelayedInitialized = true; _matchedDelayed = new Dictionary<object, bool>(); } return _matchedDelayed; } set { _matchedDelayedInitialized = true; _matchedDelayed = value; } }

        IList IFlattenedGetDataResult.Root
        {
            get => (IList)Root;
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
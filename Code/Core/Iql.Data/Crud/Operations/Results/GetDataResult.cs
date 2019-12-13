using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Data.Lists;
using Iql.Data.Tracking;

namespace Iql.Data.Crud.Operations.Results
{
    public class AggregatedGetDataResult<T> : IAggregatedGetDataResult
        where T : class
    {
        public DataTracker DataTracker { get; }
        public long? TotalCount { get; set; }
        public IList Root { get; set; }
        public GetDataResult<T>[] Results { get; }
        IGetDataResult[] IAggregatedGetDataResult.Results => Results.Select(_ => (IGetDataResult)_).ToArray();
        public DbList<T> Data { get; }

        public AggregatedGetDataResult(GetDataResult<T>[] results)
        {
            Results = results;
            TotalCount = Results.Sum(_ => _.Data?.Count ?? 0);
            var list = new List<T>();
            for (var i = 0; i < results.Length; i++)
            {
                var result = results[i];
                list.AddRange(result.Data);
            }
            DataTracker = results.Length > 0 ? results[0].DataTracker : null;
            Data = new DbList<T>(DataTracker, list);
            Data.Success = results.All(_ => _.Success);
        }
    }

    public class GetDataResult<T> : DataResult<T, DbList<T>>, IGetDataResult
        where T : class
    {
        object IGetDataResult.Data => Data;
        public bool IsOffline { get; set; }
        public DataTracker DataTracker { get; }

        public GetDataResult(bool isOffline, DataTracker dataTracker, DbList<T> data, GetDataOperation<T> operation, bool success) : base(data,
            operation, success)
        {
            IsOffline = isOffline;
            DataTracker = dataTracker;
        }
    }
}
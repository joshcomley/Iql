using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public class DbList<T> : List<T> where T : class
    {
        public DbQueryable<T> SourceQueryable { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public DbList(IEnumerable<T> source = null)
        {
            if (source != null)
            {
#if TypeScript
                if (source.GetType() == typeof(int))
                {
                    var count = (int)(object)source;
                    for (var i = 0; i < count; i++)
                    {
                        Add(null);
                    }
                }
                else
                {
#endif
                AddRange(source);
#if TypeScript
                }
#endif
            }
        }


        public DbQueryable<T> NewNextPageQuery()
        {
            if (PagingInfo == null)
            {
                throw new Exception("No page size defined for this query result");
            }
            var queryable = SourceQueryable.Copy();
            if (PagingInfo.Page == PagingInfo.PageCount)
            {
                return queryable;
            }
            return queryable.Skip(PagingInfo.PageSize).Take(PagingInfo.PageSize);
        }

        public async Task LoadNextPage()
        {
            var result = await NextPage();
            Merge(result);
        }

        public async Task<DbList<T>> NextPage()
        {
            var result = await NewNextPageQuery().ToList();
            return result;
        }

        public async Task LoadPreviousPage()
        {
            var result = await PreviousPage();
            Merge(result);
        }

        public async Task<DbList<T>> PreviousPage()
        {
            var result = await NewPreviousPageQuery().ToList();
            return result;
        }

        public async Task<DbList<T>> Page(int page, int pageSize)
        {
            var queryable = SourceQueryable.Copy();
            var operations = queryable.Operations.Where(o => !(o is SkipOperation)).ToList();
            queryable.Operations.Clear();
            queryable.Operations.AddRange(operations);
            queryable.Operations.Add(new SkipOperation((page - 1) * pageSize));
            var result = await queryable.ToList();
            return result;
        }

        private void Merge(DbList<T> result)
        {
            PagingInfo = PagingInfo ?? new PagingInfo(
                             result.PagingInfo.SkippedSoFar,
                             result.PagingInfo.TotalCount,
                             result.PagingInfo.PageSize,
                             result.PagingInfo.Page,
                             result.PagingInfo.PageCount);
            SourceQueryable = result.SourceQueryable;
            Clear();
            AddRange(result);
        }


        public DbQueryable<T> NewPreviousPageQuery()
        {
            if (PagingInfo == null)
            {
                throw new Exception("No page size defined for this query result");
            }
            var queryable = SourceQueryable.Copy();
            if (PagingInfo.Page == 0)
            {
                return queryable;
            }
            var lastSkipOperation = queryable.Operations.Last(o => o is SkipOperation) as SkipOperation;
            var lastTakeOperation = queryable.Operations.Last(o => o is TakeOperation) as TakeOperation;
            if (lastSkipOperation != null)
            {
                queryable.Operations.Remove(lastSkipOperation);
            }
            else
            {
                queryable = queryable.Skip(-PagingInfo.PageSize);
            }
            if (lastTakeOperation != null)
            {
                queryable.Operations.Remove(lastTakeOperation);
            }
            else
            {
                queryable = queryable.Take(-PagingInfo.PageSize);
            }
            return queryable;
        }
    }
}
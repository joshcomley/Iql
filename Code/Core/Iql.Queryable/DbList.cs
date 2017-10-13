using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Queryable.Operations;

namespace Iql.Queryable
{
    public class PagingInfo
    {
        public PagingInfo(int skippedSoFar, int totalCount, int pageSize, int page, int pageCount)
        {
            SkippedSoFar = skippedSoFar;
            TotalCount = totalCount;
            PageSize = pageSize;
            Page = page;
            PageCount = pageCount;
        }

        public int SkippedSoFar { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public int PageCount { get; }
    }
    public class DbList<T> : List<T> where T : class
    {
        public DbQueryable<T> SourceQueryable { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public DbList(IEnumerable<T> source)
        {
            this.AddRange(source);
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

        private async Task<DbList<T>> NextPage()
        {
            var result = await NewNextPageQuery().ToList();
            return result;
        }

        public async Task LoadPreviousPage()
        {
            var result = await PreviousPage();
            Merge(result);
        }

        private async Task<DbList<T>> PreviousPage()
        {
            var result = await NewPreviousPageQuery().ToList();
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
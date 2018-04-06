using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Queryable.Data.Paging;
using Iql.Queryable.Data.Queryable;
using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.Lists
{
    public class DbList<T> : EntityList<T>, IDbList where T : class
    {
        public DbList(IEnumerable<T> source = null)
        {
            this.Initialize(source);
        }

        public DbQueryable<T> SourceQueryable { get; set; }
        public PagingInfo PagingInfo { get; set; }

        PagingInfo IDbList.PagingInfo
        {
            get => PagingInfo;
            set => PagingInfo = value;
        }

        IDbQueryable IDbList.SourceQueryable
        {
            get => SourceQueryable;
            set => SourceQueryable = (DbQueryable<T>) value;
        }

        async Task IDbList.LoadNextPage()
        {
            await LoadNextPage();
        }

        async Task IDbList.LoadPreviousPage()
        {
            await LoadPreviousPage();
        }

        IDbQueryable IDbList.NewNextPageQuery()
        {
            return NewNextPageQuery();
        }

        IDbQueryable IDbList.NewPreviousPageQuery()
        {
            return NewPreviousPageQuery();
        }

        async Task<IDbList> IDbList.NextPage()
        {
            return await NextPage();
        }

        async Task<IDbList> IDbList.Page(int page, int pageSize)
        {
            return await Page(page, pageSize);
        }

        async Task<IDbList> IDbList.PreviousPage()
        {
            return await PreviousPage();
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
            var result = await NewNextPageQuery().ToListAsync();
            return result;
        }

        public async Task LoadPreviousPage()
        {
            var result = await PreviousPage();
            Merge(result);
        }

        public async Task<DbList<T>> PreviousPage()
        {
            var result = await NewPreviousPageQuery().ToListAsync();
            return result;
        }

        public async Task<DbList<T>> Page(int page, int pageSize)
        {
            var queryable = SourceQueryable.Copy();
            var operations = queryable.Operations.Where(o => !(o is SkipOperation)).ToList();
            queryable.Operations.Clear();
            queryable.Operations.AddRange(operations);
            queryable.Operations.Add(new SkipOperation((page - 1) * pageSize));
            var result = await queryable.ToListAsync();
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
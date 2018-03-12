using System.Collections;
using System.Threading.Tasks;
using Iql.Queryable.Data.Paging;

namespace Iql.Queryable.Data.Lists
{
    public interface IDbList : IList
    {
        PagingInfo PagingInfo { get; set; }
        IDbSet SourceQueryable { get; set; }
        Task LoadNextPage();
        Task LoadPreviousPage();
        IDbSet NewNextPageQuery();
        IDbSet NewPreviousPageQuery();
        Task<IDbList> NextPage();
        Task<IDbList> Page(int page, int pageSize);
        Task<IDbList> PreviousPage();
    }
}
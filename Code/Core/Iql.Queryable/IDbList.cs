using System.Threading.Tasks;
using Iql.Queryable.Data;

namespace Iql.Queryable
{
    public interface IDbList
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
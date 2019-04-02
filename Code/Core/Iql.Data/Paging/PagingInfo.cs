namespace Iql.Data.Paging
{
    public class PagingInfo
    {
        public PagingInfo(int skippedSoFar, long totalCount, int pageSize, int page, int pageCount)
        {
            SkippedSoFar = skippedSoFar;
            TotalCount = totalCount;
            PageSize = pageSize;
            Page = page;
            PageCount = pageCount;
        }

        public int SkippedSoFar { get; set; }
        public long TotalCount { get; set; }
        public int PageSize { get; private set; }
        public int Page { get; set; }
        public int PageCount { get; private set; }

        public void UpdatePageSize(int pageSize)
        {
            PageSize = pageSize;
            Page = SkippedSoFar / PageSize;
            var pageCount = 0;
            var i = TotalCount;
            while (i > 0)
            {
                pageCount++;
                i -= pageSize;
            }
            PageCount = pageCount;
        }
    }
}
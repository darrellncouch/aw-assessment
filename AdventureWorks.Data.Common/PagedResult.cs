namespace AdventureWorks.Data.Common
{
    public class PagedResult<T>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public bool HasNextPage { get; set; }
        public decimal PageCount { get; set; }
        public int TotalCount { get; set; }
        public List<T> Value { get; set; } = default!;
    }
}
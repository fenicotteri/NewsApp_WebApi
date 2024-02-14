namespace NewsApp.Helper
{
    public class QueryObject
    {
        /// <summary>Any text to search in post titles or content.</summary>
        public string? Search { get; set; } = null;
        /// <summary>Id of the author=.</summary>
        public string? AuthorId { get; set; } = null;
        /// <summary>Email, name, or surname of the author.</summary>
        public string? Author {  get; set; } = null;
        /// <summary>The order of sorting by creation date.</summary>
        public bool IsDecsending { get; set; } = false;
        /// <summary>Pagination.</summary>
        public int PageNumber { get; set; } = 1;
        /// <summary>Pagination.</summary>
        public int PageSize { get; set; } = 20;
    }
}

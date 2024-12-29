namespace ArtBase.Models
{
    public class SearchResult
    {
        public int Page { get; set; }
        public List<ResultItem> Results { get; set; } = new();
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
    }
}

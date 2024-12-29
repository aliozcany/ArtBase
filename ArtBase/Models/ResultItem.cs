namespace ArtBase.Models
{
    public class ResultItem
    {
        public int? Id { get; set; }
        public string? Poster_Path { get; set; } // Poster görseli için
        public string? Name { get; set; } // Diziler için isim
        public string? Title { get; set; } // Filmler için isim
        public string? Overview { get; set; }
        public List<int>? genre_ids { get; set; }
        public double? Vote_Average { get; set; }
        public int? Vote_Count { get; set; }
        public string media_type { get; set; }
        public List<string>? Origin_Country { get; set; }
        public List<string>? genreName { get; set; }
    }
    public class GenreResponse{ public List<GenreList> Genres { get; set; } = new(); }
    public class GenreList
    {
        public int? Id { get; set; }

        public string? Name { get; set; }
    }
}
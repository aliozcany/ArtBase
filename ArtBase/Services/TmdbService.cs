using System.Net.Http;
using System.Text.Json;
using ArtBase.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ArtBase.Services
{
    public class TmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiKey;
        private readonly string? _baseUrl;
        public List<GenreList> tvList;
        public List<GenreList> movieList;


        public TmdbService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["TMDB:ApiKey"];
            _baseUrl = configuration["TMDB:BaseUrl"];
        }

        public async Task<List<ResultItem>> SearchMoviesAsync(string query)
        {
            tvList = await GetGenreList("tv");
            movieList = await GetGenreList("movie");

            var response = await _httpClient.GetAsync($"{_baseUrl}/search/multi?query={query}&api_key={_apiKey}&language=tr");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // JSON büyük/küçük harf duyarlılığını kaldır
            };

            // Sadece gerekli parametreleri al
            var result = JsonSerializer.Deserialize<SearchResult>(jsonResponse, options);

            foreach (var item in result.Results.Where(x => x.media_type == "tv" || x.media_type == "movie"))
            {
                item.genreName = new List<string>();

                // Tür isimlerini al
                for (var i = 0; i < item.genre_ids.Count; i++)
                {
                    if (item.media_type == "tv")
                    {
                        item.genreName.Add(tvList.FirstOrDefault(g => g.Id == item.genre_ids[i])?.Name);
                    }
                    else
                    {
                        item.genreName.Add(movieList.FirstOrDefault(g => g.Id == item.genre_ids[i])?.Name);
                    }
                }

                // Detayları alarak runtime ekle
                if (item.media_type == "movie")
                {
                    if (item.Id.HasValue) // item.Id null olabilir, kontrol et
                    {
                        var movieDetails = await GetMovieDetails(item.Id.Value); // Nullable int'i Value ile kullan
                        item.RunTime = movieDetails?.Runtime; // Filmin süresi
                    }
                }
                else if (item.media_type == "tv")
                {
                    if (item.Id.HasValue) // item.Id null olabilir, kontrol et
                    {
                        var tvDetails = await GetTvDetails(item.Id.Value); // Nullable int'i Value ile kullan
                        item.RunTime = tvDetails?.episode_run_time?.FirstOrDefault()*tvDetails?.number_of_episodes; // Dizinin ortalama bölüm süresi
                    }
                }

            }

            return result?.Results ?? new List<ResultItem>();
        }

        // Film detaylarını almak için metot
        public async Task<MovieDetailsModel> GetMovieDetails(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/movie/{id}?api_key={_apiKey}&language=tr");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<MovieDetailsModel>(jsonResponse, options);
        }

        // Dizi detaylarını almak için metot
        public async Task<TvDetailsModel> GetTvDetails(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/tv/{id}?api_key={_apiKey}&language=tr");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<TvDetailsModel>(jsonResponse, options);
        }



        public async Task<List<GenreList>> GetGenreList(string mediatype)
        {

            var response = await _httpClient.GetAsync($"{_baseUrl}/genre/{mediatype}/list?language=tr&api_key={_apiKey}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var result = JsonSerializer.Deserialize<GenreResponse>(jsonResponse, options);

            return result.Genres;

        }

    }
}

namespace ArtBase.Services
{
    public class TmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public TmdbService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["TMDB:ApiKey"];
            _baseUrl = configuration["TMDB:BaseUrl"];
        }

        public async Task<dynamic> SearchMultiAsync(string query)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/search/multi?query={query}&api_key={_apiKey}");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<dynamic>(jsonResponse);
        }
    }
}

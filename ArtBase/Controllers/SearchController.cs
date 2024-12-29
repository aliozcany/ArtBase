using ArtBase.Services;
using Microsoft.AspNetCore.Mvc;

public class SearchController : Controller
{
    private readonly TmdbService _tmdbService;

    public SearchController(TmdbService tmdbService)
    {
        _tmdbService = tmdbService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Results(string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            ViewBag.Error = "Arama terimi boş olamaz.";
            return View("Index");
        }

        var searchResults = await _tmdbService.SearchMoviesAsync(query);

        if (searchResults == null || !searchResults.Any())
        {
            ViewBag.Error = "Sonuç bulunamadı.";
            return View("Index");
        }

        return View(searchResults);
    }
}

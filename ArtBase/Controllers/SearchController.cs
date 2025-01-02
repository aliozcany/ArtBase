using ArtBase.Services;
using ArtBase.Models; // Watchlist modeli için
using Microsoft.AspNetCore.Identity; // Kullanıcı kimlik bilgileri için
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;

public class SearchController : Controller
{
    private readonly TmdbService _tmdbService;
    private readonly ArtBaseDBContext _context; // Veritabanı bağlamı
    private readonly UserManager<IdentityUser> _userManager; // Kullanıcı kimliği

    public SearchController(TmdbService tmdbService, ArtBaseDBContext context, UserManager<IdentityUser> userManager)
    {
        _tmdbService = tmdbService;
        _context = context;
        _userManager = userManager;
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

    [HttpPost]
    public IActionResult AddToWatchlist(string title, string posterPath, string contentType, string overview, int? runtime)
    {
        if (string.IsNullOrEmpty(title))
        {
            return Json(new { success = false, message = "Başlık (Title) boş olamaz." });
        }

        var userId = _userManager.GetUserId(User); // Giriş yapan kullanıcının ID'si

        if (string.IsNullOrEmpty(userId))
        {
            return RedirectToAction("Index", "Account"); // Kullanıcı giriş yapmadıysa giriş sayfasına yönlendirin
        }


        // Aynı içeriğin zaten Watchlist'te olup olmadığını kontrol edin
        var existingItem = _context.WatchLists.FirstOrDefault(w =>
            w.Title == title && w.UserId == userId);

        if (existingItem != null)
        {
            TempData["Error"] = "Bu içerik zaten Watchlist'inizde mevcut.";
            return RedirectToAction("Results");
        }

        var watchlistItem = new WatchListsModel
        {
            Title = title,
            PosterPath = posterPath,
            ContentType = contentType,
            Overview = overview,
            Runtime = runtime,
            UserId = userId
        };

        _context.WatchLists.Add(watchlistItem);
        _context.SaveChanges();

        TempData["Success"] = "İçerik başarıyla Watchlist'e eklendi.";
        return RedirectToAction("Watchlist");
    }

    [Authorize]
    public IActionResult Watchlist()
    {
        var userId = _userManager.GetUserId(User); // Kullanıcı ID'sini al
        var watchlist = _context.WatchLists
            .Where(w => w.UserId == userId)
            .ToList(); // Sadece giriş yapan kullanıcının Watchlist'ini al

        return View(watchlist);
    }

    [Authorize]
    [HttpPost]
    public IActionResult RemoveFromWatchlist(int id)
    {
        var userId = _userManager.GetUserId(User);

        var item = _context.WatchLists.FirstOrDefault(w => w.WatchlistID == id && w.UserId == userId);
        if (item != null)
        {
            _context.WatchLists.Remove(item);
            _context.SaveChanges();
            TempData["Success"] = "İçerik başarıyla Watchlist'ten kaldırıldı.";
        }
        else
        {
            TempData["Error"] = "İçerik bulunamadı veya size ait değil.";
        }

        return RedirectToAction("Watchlist");
    }


}

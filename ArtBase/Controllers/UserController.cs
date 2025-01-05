using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArtBase.Models; // Veritabanı modelleri için gerekli
using System.Linq;

[Authorize]
public class UserController : Controller
{
    private readonly UserManager<IdentityUser> _userManager; // Kullanıcı kimliği yönetimi
    private readonly ArtBaseDBContext _context; // Veritabanı bağlamı

    public UserController(UserManager<IdentityUser> userManager, ArtBaseDBContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public IActionResult Dashboard()
    {
        var userName = User.Identity.Name; // Giriş yapan kullanıcının adı
        ViewBag.UserName = userName;
        return View();
    }

    public IActionResult Profile()
    {
        var userId = _userManager.GetUserId(User); // Kullanıcı ID'sini al
        var user = _context.Users.FirstOrDefault(u => u.Id == userId); // Kullanıcı bilgileri
        ViewBag.UserName = user?.UserName ?? "Bilinmiyor"; // Kullanıcı adı


        // Kullanıcıya ait Watchlist'i çek
        var watchlist = _context.WatchLists
            .Where(w => w.UserId == userId)
            .ToList();

            string totalRuntime = null;

        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "SELECT CalculateTotalRuntime(@userId)";
            var parameter = command.CreateParameter();
            parameter.ParameterName = "@userId";
            parameter.Value = userId;
            command.Parameters.Add(parameter);

            _context.Database.OpenConnection();

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    totalRuntime = reader.GetString(0); // İlk sütunu al
                }
            }
        }

        

        ViewBag.TotalRuntime = totalRuntime ?? "0 gün, 0 saat, 0 dakika";

        return View(watchlist); // Watchlist'i View'e gönder
    }



}

using System;
using System.Diagnostics;
using ArtBase.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace ArtBase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ArtBaseDBContext _context; // Veritabaný baðlamý
        private readonly UserManager<IdentityUser> _userManager; // ASP.NET Identity

        public HomeController(ILogger<HomeController> logger, ArtBaseDBContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Giriþ yapmýþ kullanýcýlar için gönderi listesi
                var posts = _context.Posts
                        .OrderByDescending(p => p.CreatedAt)
                        .Select(p => new PostViewModel
                        {
                            Content = p.Content,
                            CreatedAt = p.CreatedAt,
                            Username = p.User.UserName // Kullanýcý adý
                        })
                        .ToList();

                return View("AuthenticatedIndex", posts); // Giriþ yapmýþ kullanýcýlar için özel View
            }
            else
            {
                // Giriþ yapmamýþ kullanýcýlar için farklý bir içerik
                return View("GuestIndex"); // Üye olmayanlar için View
            }
        }

        [HttpPost]
        public IActionResult AddPost(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                var userId = _userManager.GetUserId(User);

                var post = new PostModel
                {
                    Content = content,
                    CreatedAt = DateTime.Now,
                    UserId = userId
                };

                _context.Posts.Add(post);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

         
    }
}

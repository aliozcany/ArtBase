using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class UserController : Controller
{
    public IActionResult Dashboard()
    {
        var userName = User.Identity.Name; // Giriş yapan kullanıcının adı
        ViewBag.UserName = userName;
        return View();
    }

    public IActionResult Profile()
    {
        return View();
    }
}
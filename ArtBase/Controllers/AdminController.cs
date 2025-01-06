using System.Data;
using ArtBase.Models;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtBase.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager; // Kullanıcı kimliği yönetimi

        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> ShowAllUsers()
        {
            var userid = _userManager.GetUserId;
            ViewBag.AdminID = userid;
            var users = _userManager.Users.ToList();
            
            return View(users);
        }

        // Kullanıcıyı silmek için bir aksiyon
        [HttpPost]
        public async Task<IActionResult> Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }
            //kullanıcıyı sil
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest("Kullanıcı silinemedi.");
            }

            return RedirectToAction("ShowAllUsers");
        }
    }
}

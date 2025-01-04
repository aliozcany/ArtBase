using Microsoft.AspNetCore.Mvc;

namespace ArtBase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RedirectController : ControllerBase
    {
        [HttpGet("to-vue")]
        public IActionResult RedirectToVue()
        {
            return Redirect("http://localhost:5000"); // Vue.js uygulamanın çalıştığı URL
        }
    }
}

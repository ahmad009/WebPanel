using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace B2B.Controllers
{
    [Authorize]
    public class HomeController : MasterController
    {
        public HomeController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpContextAccessor, configuration)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

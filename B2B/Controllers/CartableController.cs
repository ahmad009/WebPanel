using BusinessLogic;
using DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace B2B.Controllers
{
    [Route("[controller]")]
    [LogActionFilter]
    [AllowAnonymous]
    public class CartableController : MasterController
    {
        public CartableController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpContextAccessor, configuration)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("SCM")]
        public IActionResult SCM()
        {
            return View();
        }
    }
}

using BusinessLogic;
using DataModel.Model;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace B2B.Controllers
{
    public class MasterController : Controller
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IConfiguration _configuration;

        public MasterController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._configuration = configuration;
        }

        [NonAction]
        public async Task<Web_UserModel?> GetCurrentUserAsync()
        {
            string? userName = Cookie.Get(Request, "username");
            if (userName == null)
                return null;
            Web_UserModel? user = await UserBO.Instance.GetWebUserAsync(userName);
            return user;
        }
    }
}

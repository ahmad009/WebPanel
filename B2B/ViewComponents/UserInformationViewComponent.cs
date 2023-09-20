using BusinessLogic;
using DataModel.Model;
using Microsoft.AspNetCore.Mvc;

namespace B2B.ViewComponents
{
    public class UserInformationViewComponent : ViewComponent
    {
        public async Task<Web_UserModel?> GetCurrentUserAsync()
        {
            string? userName = Cookie.Get(Request, "username");
            if (userName == null)
                return null;
            Web_UserModel? user = await UserBO.Instance.GetWebUserAsync(userName);
            return user;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Web_UserModel? model = new Web_UserModel();
            model = await GetCurrentUserAsync();

            return View(model);
        }
    }
}

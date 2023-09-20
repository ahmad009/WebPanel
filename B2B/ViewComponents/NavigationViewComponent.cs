using Azure;
using B2B.Controllers;
using BusinessLogic;
using DataModel;
using DataModel.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace B2B.ViewComponents
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public NavigationViewComponent(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Web_UserModel?> GetCurrentUserAsync()
        {
            string? userName = Cookie.Get(Request, "username");
            if (userName == null)
                return null;
            Web_UserModel? user = await UserBO.Instance.GetWebUserAsync(userName);
            return user;
        }

        private ViewMenuDS InitializeNavigationMenu(int? menuItemId, int? selectedMenuItemId, Guid Id)
        {
            ViewMenuDS menuItems = WebMenuBO.Instance.GenerateMenuInfo(menuItemId, selectedMenuItemId ?? 0, Id);
            if ((selectedMenuItemId ?? 0) != 0)
            {
                menuItems.SelectedMenuItemId = selectedMenuItemId.Value;
            }
            else
            {
                menuItems.SelectedMenuItemId = 100;
            }

            return menuItems;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? menuItemId, int? selectedMenuItemId)
        {
            var User = GetCurrentUserAsync().Result;
            ViewData["firstname"] = Cookie.Get(Request, "firstname");
            ViewData["lastname"] = Cookie.Get(Request, "lastname");
            ViewData["email"] = Cookie.Get(Request, "email");
            
            if (User != null)
            {
                var MenuItems = InitializeNavigationMenu(menuItemId, selectedMenuItemId, User.Id);

                return View(MenuItems);
            }
            else
            {
                _httpContextAccessor.HttpContext.Response.Redirect("/Login");
                return View(new ViewMenuDS());
            }
        }
    }
}

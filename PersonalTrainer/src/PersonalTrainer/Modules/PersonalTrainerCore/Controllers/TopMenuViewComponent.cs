using Framework.Models;
using Framework.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace PersonalTrainerCore.Controllers
{
    /// <summary>
    /// Komponent górnej belki.
    /// </summary>
    public class TopMenuViewComponent : ViewComponent
    {
        private readonly IUserManagement userManagement;

        public TopMenuViewComponent(IUserManagement userManagement)
        {
            this.userManagement = userManagement;
        }

        public async Task<IViewComponentResult> InvokeAsync(Boolean loggedIn, String username)
        {
            return await Task.Run(() =>
            {
                var dto = new TopMenuDto();
                if (userManagement.UserLogedIn())
                {
                    dto.IsLogedIn = true;
                    dto.UserName = userManagement.GetCurrentUser().Login;
                  
                }
                else
                {
                    dto.IsLogedIn = false;
                    dto.UserName = String.Empty;
                }

                return View(dto);
            });
        }
    }
}

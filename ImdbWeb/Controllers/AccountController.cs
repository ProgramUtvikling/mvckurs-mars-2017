using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Authentication;
using System.Security.Claims;
using System.Security.Principal;
using ImdbWeb.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ImdbWeb.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInModel model)
        {
            if(model.Username == "arjan" && model.Password == "pass")
            {
                var identity = new GenericIdentity(model.Username);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.Authentication.SignInAsync("MyAuthMiddleware", principal, new AuthenticationProperties { IsPersistent = false });

                return View("LoggedInOk");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.Authentication.SignOutAsync("MyAuthMiddleware");
            return RedirectToAction("Index", "Home");
        }
    }
}

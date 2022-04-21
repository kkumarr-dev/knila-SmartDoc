using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartDoc.Helper.Auth;
using SmartDoc.Models;
using SmartDoc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDoc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult LoginPartial()
        {
            return PartialView();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            var userData = await _accountService.GetUserData(user);
            if (userData == null)
            {
                return Unauthorized();
            }
            else
            {
                await AuthenticationConfig.AddClaims(HttpContext, userData);
                if (userData.RoleId == 1) return Ok(new { url = "/Admin/" });
                else if (userData.RoleId == 2) return Ok(new { url = "/Appoinment/" });
                else if (userData.RoleId == 3) return Ok(new { url = "/Appoinment/" });
                else
                {
                    return Ok();
                }
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("login", "Account");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Services.LoginService;
using EcommerceApp.Domain.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace EcommerceApp.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var loginUser= await _loginService.Login(loginDTO);
            
            if (loginUser!=null)
            {
                var jsonUser = JsonConvert.SerializeObject(loginUser);
                HttpContext.Session.SetString("loginUser", jsonUser);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role,loginUser.Roles.ToString()));
                var userIdentity=new ClaimsIdentity(claims,"Login");
                ClaimsPrincipal principal=new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                if (loginUser.Roles==Roles.Admin)
                {
                    return RedirectToAction("Index", "Admin", new { area = "Admin" });
                }

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.Response.Cookies.Delete(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            return View(loginDTO);
        }
    }
}

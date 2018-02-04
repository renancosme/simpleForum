using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SimpleForum.Domain;
using SimpleForum.Presentation.Models;

namespace SimpleForum.Presentation.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            var user = _unitOfWork.Users.Find(u => 
                            u.Name.Equals(login) &&
                            u.Password.Equals(password))
                            .FirstOrDefault();

            if (user == null)
            {
                ModelState.AddModelError("Login", "User or password is not correct.");
                return View();
            }
            else
            {
                var claims = new List<Claim> {
                            new Claim(ClaimTypes.Name, login, ClaimValueTypes.String),
                            new Claim(ClaimTypes.Sid, user.Id.ToString())
                         };

                var userIdentity = new ClaimsIdentity(claims, "login");

                var userPrincipal = new ClaimsPrincipal(userIdentity);

                HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(userPrincipal)
                );
            }            

            return RedirectToAction("Create", "Topic");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Create", "Topic");
        }
    }    
}
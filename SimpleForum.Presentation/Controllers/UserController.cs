using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleForum.Domain;
using SimpleForum.Presentation.Models;

namespace SimpleForum.Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserViewModel userViewModel)
        {            
            if (ModelState.IsValid)
            {
                try
                {
                    var userExists = _unitOfWork.Users.Find(u => u.Name.Equals(userViewModel.Login)).Count() > 0;
                    if (userExists)
                    {
                        ModelState.AddModelError("Login", "Sorry, there is an user with this login.");
                    }
                    else
                    {
                        RedirectToAction(
                            "LogIn", 
                            "Login", 
                            new { login = userViewModel.Login, password = userViewModel.Password });
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("UserCreate", e.Message);
                }
            }

            return View();
        }
    }
}
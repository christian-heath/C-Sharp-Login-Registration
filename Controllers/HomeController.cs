using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using loginreg.Models;

namespace loginreg.Controllers
{
    public class HomeController : Controller
    {
        private LoginRegContext dbContext;

        public HomeController(LoginRegContext context)
        {
            dbContext = context;
        }
        [HttpGet("")]
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet("Result")]
        public IActionResult Result(User user)
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Register");
            }
            @ViewBag.User = user;
            return View();
        }

        [HttpPost("Register")]
        public IActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Register");
                }
                else
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    user.Password = Hasher.HashPassword(user, user.Password);
                    dbContext.Add(user);
                    dbContext.SaveChanges();
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    return RedirectToAction("Result", user);
                }
            }
            else
            {
                return View("Register");
            }
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginUser user)
        {
            if(ModelState.IsValid)
            {
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == user.Email);
                if(userInDb == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Login");
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(user, userInDb.Password, user.Password);
                if(result == 0)
                {
                    ModelState.AddModelError("Password", "Password does not exist!");
                    return View("Login");
                }
                else
                {
                    HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                    return RedirectToAction("Result", userInDb);
                }
            }
            else
            {
                return View("Login");
            }
        }
        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
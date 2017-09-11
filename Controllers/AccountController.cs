using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trippy.Models;
using trippy.ViewModels;

namespace trippy.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<User> _signInManager;

        public AccountController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Trips", "App");
            }

            return View();
        }

        [HttpPost("Account/Login")]
        public async Task<ActionResult> Login(LoginViewModel loginView, string returnURL)
        {
           
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(loginView.Username, loginView.Password, true, false);

                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnURL))
                    {
                        RedirectToAction("Trips", "App");
                    }
                    else
                    {
                        Redirect(returnURL);
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password Incorrect");
                }
            }
            return View();
        }

        [HttpGet("Accout/Logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "App");
        }
    }
}

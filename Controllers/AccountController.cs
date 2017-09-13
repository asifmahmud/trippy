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
        private UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
                        return RedirectToAction("Trips", "App");
                    }
                    else
                    {
                        return Redirect(returnURL);
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password Incorrect");
                }
            }
            return View();
        }

        
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost("Account/SignUp")]
        public async Task<ActionResult> SignUp(SignUpViewModel signUpView)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.FindByEmailAsync(signUpView.Email) == null)
                {
                    var User = new User()
                    {
                        UserName = signUpView.Username,
                        Email = signUpView.Email
                    };
                    await _userManager.CreateAsync(User, signUpView.Password);
                    return RedirectToAction("Trips", "App");

                }
                else
                {
                    ModelState.AddModelError("", "User already exists.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid Username or Password");
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

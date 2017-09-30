using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            ViewBag.loginError = "";
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
                    ViewBag.loginError = "Username or Password is incorrect";
                    return View();

                }
            }
            return View();
        }

        
        public IActionResult SignUp()
        {
            ViewBag.signUpError = "";
            return View();
        }

        private bool _passCheck(string password)
        {
            string passwordPattern = "((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*~]).{8, 100})";
            Regex reg = new Regex(passwordPattern);
            return reg.IsMatch(password);
        }

        [HttpPost("Account/SignUp")]
        [HttpPost("/")]
        public async Task<ActionResult> SignUp(SignUpViewModel signUpView)
        {
            if (ModelState.IsValid)
            {
                if (!_passCheck(signUpView.Password))
                {
                    ViewBag.signUpError =
                        "Your password must be at least 8 characters and must " +
                        "include at aleast one upper case character, one lower " +
                        "case character, one special character ($,@,% ...) and a number.";

                    return View();
                }

                if (await _userManager.FindByEmailAsync(signUpView.Email) == null)
                {
                    var User = new User()
                    {
                        UserName = signUpView.Username,
                        Email = signUpView.Email
                    };
                    await _userManager.CreateAsync(User, signUpView.Password);
                    var signInResult = await _signInManager.PasswordSignInAsync(
                                                                signUpView.Username, 
                                                                signUpView.Password, 
                                                                true, 
                                                                false);
                    if (signInResult.Succeeded)
                    {
                        return RedirectToAction("Trips", "App");
                    }
                    else
                    {
                        return RedirectToAction("SignUp", "Account");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User already exists.");
                    ViewBag.signUpError = "The user with the email address already exsists";
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                ViewBag.signUpError = "Invalid Username or Password";
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

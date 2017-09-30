﻿using Microsoft.AspNetCore.Identity;
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
            ViewBag.loginError = "";
            return View();
        }

        [HttpPost("Account/Login")]
        public async Task<ActionResult> Login(LoginViewModel loginView, string returnURL)
        {
           
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(loginView.LoginUsername, loginView.LoginPassword, true, false);

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

        [HttpPost("Account/SignUp")]
        [HttpPost("/")]
        public async Task<ActionResult> SignUp(SignUpViewModel signUpView)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.FindByEmailAsync(signUpView.SignupEmail) == null)
                {
                    var User = new User()
                    {
                        UserName = signUpView.SignupUsername,
                        Email = signUpView.SignupEmail
                    };
                    await _userManager.CreateAsync(User, signUpView.SignupPassword);
                    var signInResult = await _signInManager.PasswordSignInAsync(
                                                                signUpView.SignupUsername, 
                                                                signUpView.SignupPassword, 
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

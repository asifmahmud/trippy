using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using trippy.ViewModels;
using trippy.Services;
using Microsoft.Extensions.Configuration;
using trippy.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace trippy.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private IWorldRepository _repository;
        private ILogger<AppController> _logger;

        public AppController(
            IMailService mailService, 
            IConfigurationRoot config, 
            IWorldRepository repository,
            ILogger<AppController> logger)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("ReturningUser", "App");
            }
            return View();
        }

        public IActionResult ReturningUser()
        {
            return View();
        }

        [Authorize]
        public IActionResult Trips()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get trips in Index page: {0}", ex.Message);
                return Redirect("/error");
            }
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSettings:toAddress"], model.Email, "Mail", model.Message);
                ModelState.Clear();
                ViewBag.UserMessage = "Message Sent";
            }
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}

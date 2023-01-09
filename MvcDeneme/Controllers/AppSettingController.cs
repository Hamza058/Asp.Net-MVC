using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDeneme.Controllers
{
    public class AppSettingController : Controller
    {
        private readonly IConfiguration _configuration;

        public AppSettingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.baseUrl = _configuration["baseUrl"];
            ViewBag.smsKey = _configuration["Keys:Sms"];
            ViewBag.emailKey = _configuration.GetSection("Keys")["email"];
            return View();
        }
    }
}

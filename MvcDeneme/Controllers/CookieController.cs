using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDeneme.Controllers
{
    public class CookieController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Response.Cookies.Append("background-color", "red", new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(2)
            });
            return View();
        }
        public IActionResult CookieRead()
        {
            var bgcolor = HttpContext.Request.Cookies["background-color"];
            ViewBag.bgcolor = bgcolor;
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MvcDeneme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDeneme.Controllers
{
    public class VisitorAjaxController : Controller
    {
        private readonly Context _context;

        public VisitorAjaxController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveVisitorComment(Visitor visitor)
        {
            visitor.Created = DateTime.UtcNow;
            _context.Visitors.Add(visitor);
            _context.SaveChanges();

            return Json(new { IsSuccess = "true" });
        }

        [HttpGet]
        public IActionResult VisitorCommentList()
        {
            var visitors = _context.Visitors.OrderByDescending(x=>x.Created).ToList();
            return Json(visitors);
        }
    }
}

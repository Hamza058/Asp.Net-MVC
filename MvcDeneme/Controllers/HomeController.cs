using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcDeneme.Models;
using MvcDeneme.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDeneme.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context _context;

        public HomeController(ILogger<HomeController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.OrderByDescending(x => x.Id).Select(x => new ProductParitalViewModel()
            {
                Id = x.Id,
                UrunAdı = x.UrunAdı,
                Fiyat = x.Fiyat
            }).ToList();
            ViewBag.productListPartialViewModel = new ProductListPartialViewModel()
            {
                Products = products
            };

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel errorViewModel)
        {
            errorViewModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View(errorViewModel);
        }

        public IActionResult Visitor()
        {
            return View();
        }

        public IActionResult SaveComment(Visitor visitor)
        {
            visitor.Created = DateTime.UtcNow;
            _context.Visitors.Add(visitor);
            _context.SaveChanges();

            return RedirectToAction(nameof(HomeController.Visitor));//Farklı bir yöntem
        }
    }
}

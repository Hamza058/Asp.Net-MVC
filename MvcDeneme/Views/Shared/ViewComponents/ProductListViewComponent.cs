using Microsoft.AspNetCore.Mvc;
using MvcDeneme.Models;
using MvcDeneme.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDeneme.Views.Shared.ViewComponents
{
    //[ViewComponent(Name ="p-list")]//Yaparak ViewComponent e isim verebiliriz
    public class ProductListViewComponent:ViewComponent
    {
        private readonly Context _context;

        public ProductListViewComponent(Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int type = 1)
        {
            var viewmodels = _context.Products.Select(x => new ProductListComponentViewModel()
            {
                UrunAdı = x.UrunAdı,
                Renk = x.Renk
            }).ToList();

            if(type==1)
                return View("Default",viewmodels);
            else
                return View("Type2", viewmodels);
        }
    }
}

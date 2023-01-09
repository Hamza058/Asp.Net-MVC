using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MvcDeneme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDeneme.Views.Shared.ViewComponents
{
    public class VisitorViewComponent:ViewComponent
    {
        private readonly Context _context;

        public VisitorViewComponent(Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int type = 1)
        {
            var values = _context.Visitors.ToList();

            if (type == 1)
                return View("Default", values);
            else
                return View("View");
        }
    }
}

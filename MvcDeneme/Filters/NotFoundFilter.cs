using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MvcDeneme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDeneme.Filters
{
    public class NotFoundFilter:ActionFilterAttribute
    {
        private readonly Context _context;

        public NotFoundFilter(Context context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var idValue = context.ActionArguments.Values.First();
            var id = (int)idValue;
            var hasProduct = _context.Products.Any(x => x.Id == id);
            if (hasProduct == false)
            {
                context.Result = new RedirectToActionResult("Error", "Home", new ErrorViewModel()
                {
                    Errors=new List<string>() {$"Id({id})' ye sahip ürün veritabanında bulanamamıştır." }
                });
            }
        }
    }
}

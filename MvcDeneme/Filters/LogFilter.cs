using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDeneme.Filters
{
    public class LogFilter:ActionFilterAttribute
    {
        //Method çalışmadan önce-1
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Debug.WriteLine("önce");
        }
        //Method çalıştıktan sonra-2
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine("sonra");
        }
        //Sonuç almadan önce-3
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Debug.WriteLine("re önce");
        }
        //Sonuç aldıktan sonra-4
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Debug.WriteLine("re sonra");
        }

    }
}

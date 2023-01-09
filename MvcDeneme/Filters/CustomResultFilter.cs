using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDeneme.Filters
{
    public class CustomResultFilter:ResultFilterAttribute
    {
        private readonly string _name;
        private readonly string _value;

        public CustomResultFilter(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add(_name, _value);
        }
    }
}

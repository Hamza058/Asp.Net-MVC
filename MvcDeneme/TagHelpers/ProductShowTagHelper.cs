using Microsoft.AspNetCore.Razor.TagHelpers;
using MvcDeneme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDeneme.TagHelpers
{
    public class ProductShowTagHelper:TagHelper
    {
        public Product Product { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Content.SetHtmlContent(@$"<ul class='list-gorup'>" +
                $"<li class='list-group-item'>{Product.Id}</li>" +
                $"<li class='list-group-item'>{Product.UrunAdı}</li>" +
                $"<li class='list-group-item'>{Product.Renk}</li>" +
                $"</ul");
        }
    }
}

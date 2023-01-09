using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDeneme.TagHelpers
{
    //[HtmlTargetElement("thumbnail")]//yaparak taghelper a isim verebiliriz
    public class ImageThumbnailTagHelper : TagHelper
    {
        public string ImageSrc { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            string fileName = ImageSrc.Split('.')[0];
            string fileExtension = Path.GetExtension(ImageSrc);

            output.Attributes.SetAttribute("src", $"{fileName}-100x100{fileExtension}");
            output.Attributes.SetAttribute("width", "200");
        }
    }
}

using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Northwind.Web.TagHelpers
{
    public class ImageLinkTagHelper: TagHelper
    {

        public string ImageId {get;set;}
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            var imageUrl = $"/images/{ImageId}";
            output.Attributes.SetAttribute("href", imageUrl);
        }
    }
}
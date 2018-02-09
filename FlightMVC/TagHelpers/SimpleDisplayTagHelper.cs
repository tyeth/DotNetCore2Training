using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FlightMVC.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("p",Attributes = "asp-for")]
    public class SimpleDisplayTagHelper : TagHelper
    {

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }
        public async override void Process(TagHelperContext context, TagHelperOutput output)//,TagHelperContent origContent)
        {
            if(context==null || output==null)
            { throw new ArgumentNullException(); }
            var o = new DefaultTagHelperContent();
            TagHelperContent childContent = (await output.GetChildContentAsync());
            if (childContent == null) throw new ApplicationException("No innerHTML for p tag");
            string oldContent = childContent.GetContent();


            string content = For.ModelExplorer.GetSimpleDisplayText();
            if (string.IsNullOrEmpty(oldContent) == false)
            {
                content = oldContent + "\r\n" + content;
            }
            output.Content.SetHtmlContent(content);
        }
    }
}

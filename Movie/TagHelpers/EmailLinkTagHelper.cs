using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Movie.TagHelpers
{


    [HtmlTargetElement("a")]
    public class EmailLinkTagHelper : TagHelper
    {

        public string Email { get; set; }
        public string MyTitle { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (MyTitle!=null)
            {
                output.TagName = "a";
                output.Content.Append(MyTitle ?? Email);
                output.Attributes.Add("href", $"mailto:{Email}");
            }


            ////string link = $"<a href=\"mailto:{mail}\">{title ?? mail}</a>";
            //output.TagName = "a";
            ////output.Content.Append("Youtube");
            ////output.Attributes.Add("href", "https://www.youtube.com");


            //output.Content.Append(MyTitle ?? Email);
            //output.Attributes.Add("href", $"mailto:{Email}");
        }
    }
}

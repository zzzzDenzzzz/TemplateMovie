using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Movie.Extensions
{
    public static class MovieHtmlHelpers
    {
        public static IHtmlContent EmailLink(this IHtmlHelper htmlHelper, string mail, string title = null)
        {
            var link = new TagBuilder("a");
            link.InnerHtml.Append(title ?? mail);
            link.Attributes.Add("href", $"mailto:{mail}");
            link.AddCssClass("text-danger");

            return link;
        }
    }
}

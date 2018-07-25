using Microsoft.AspNetCore.Html;
using System.Text.RegularExpressions;
using TinyBlog.Web.ViewModels;

namespace Microsoft.AspNetCore.Mvc.Rendering
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent ProcessHtml(this IHtmlHelper htmlHelper, BlogViewModel blog, string content)
        {
            string result = content;

            // Youtube content embedded using this syntax: [youtube:xyzAbc123]
            var video = "<div class=\"video\"><iframe src=\"//www.youtube.com/embed/{0}?modestbranding=1&amp;theme=light\" allowfullscreen></iframe></div>";
            result = Regex.Replace(result, @"\[youtube:(.*?)\]", (Match m) => string.Format(video, m.Groups[1].Value));

            // Images replaced by CDN paths if they are located in the /images/ folder
            var cdn = blog.CdnUrl;
            var root = "/images/";

            if (!root.StartsWith("/"))
                root = "/" + root;

            result = Regex.Replace(result, "<img.*?src=\"([^\"]+)\"", (Match m) =>
            {
                string src = m.Groups[1].Value;
                int index = src.IndexOf(root);

                if (index > -1)
                {
                    string clean = src.Substring(index);
                    return m.Value.Replace(src, cdn + clean);
                }

                return m.Value;
            });

            return new HtmlString(result);
        }
    }
}

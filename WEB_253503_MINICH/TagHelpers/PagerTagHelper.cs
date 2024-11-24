using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WEB_253503_MINICH.UI.TagHelpers
{
    [HtmlTargetElement("Pager")]
    public class PagerTagHelper : TagHelper
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Category { get; set; }
        public bool Admin { get; set; }
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PagerTagHelper(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");

            if (CurrentPage > 1)
            {
                var prevLink = CreatePageItem(CurrentPage - 1, "«");
                tag.InnerHtml.AppendHtml(prevLink);
            }

            for (int i = 1; i <= TotalPages; i++)
            {
                var pageLink = CreatePageItem(i, i.ToString());
                if (i == CurrentPage)
                {
                    pageLink.AddCssClass("active");
                }
                tag.InnerHtml.AppendHtml(pageLink);
            }

            if (CurrentPage < TotalPages)
            {
                var nextLink = CreatePageItem(CurrentPage + 1, "»");
                tag.InnerHtml.AppendHtml(nextLink);
            }

            output.TagName = "ul";
            output.Content.SetHtmlContent(tag);
        }

        private TagBuilder CreatePageItem(int page, string text)
        {
            var li = new TagBuilder("li");
            li.AddCssClass("page-item");
            if (page == CurrentPage)
            {
                li.AddCssClass("active");
            }

            var a = new TagBuilder("a");
            a.AddCssClass("page-link");
            a.Attributes["href"] = GeneratePageLink(page);
            a.InnerHtml.Append(text);

            li.InnerHtml.AppendHtml(a);
            return li;
        }

        private string GeneratePageLink(int pageNo)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new InvalidOperationException("HttpContext is null.");
            }

            string? url = null;


            var values = new RouteValueDictionary
                {
                    { "pageNo", pageNo }
                };

            if (!string.IsNullOrEmpty(Category))
            {
                values.Add("category", Category);
            }

            url = _linkGenerator.GetPathByAction(
                action: "Index",
                controller: "Cup",
                values: values,
                httpContext: httpContext);


            return url ?? "#";
        }
    }
}

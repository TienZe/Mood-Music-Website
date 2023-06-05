using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace PBL3.Infrastructures
{
    [HtmlTargetElement("div", Attributes = "active-page,total-pages")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public int ActivePage { get; set; }
        public int TotalPages { get; set; }
        public string PageAction { get; set; } = string.Empty;
        public string PageController { get; set; } = string.Empty;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-data-")]
        public Dictionary<string, object> PageValues { get; set; }  // key-value : query string
            = new Dictionary<string, object>();

        public PaginationTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Set ko hiển thị thẻ div được áp dụng tag helper, tức là chỉ
            // hiển thị output.Content, chứ không hiển thị cả output
            output.SuppressOutput(); // clean content, TagName = null

            var urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            int beginIndex = Math.Max(ActivePage - 1, 1);
            int endIndex = Math.Min(ActivePage + 1, TotalPages);
            // Check corner case
            if (TotalPages >= 3)
            {
                if (ActivePage == 1)
                {
                    beginIndex = 1;
                    endIndex = 3;
                }
                if (ActivePage == TotalPages)
                {
                    beginIndex = TotalPages - 3 + 1;
                    endIndex = TotalPages;
                }
            }
           
            // Thêm Previous
            {
                TagBuilder divTag = new TagBuilder("div");
                divTag.AddCssClass("Model-prev-pagenumber-next-last");
                PageValues["pageIndex"] = Math.Max(ActivePage - 1, 1);
                var url = urlHelper.Action(PageAction, PageController, PageValues);
                // Tạo thẻ a với href "url"
                var aTag = new TagBuilder("a");
                aTag.Attributes.Add("href", url);
                aTag.InnerHtml.Append("Previous");
                // Thêm thẻ a vào div
                divTag.InnerHtml.AppendHtml(aTag);
                // Append output
                output.Content.AppendHtml(divTag);
            }
            // Thêm các link trang lân cận
            for (int i = beginIndex; i <= endIndex; i++)
            {
                TagBuilder divTag = new TagBuilder("div");
                divTag.AddCssClass("Model-prev-pagenumber-next-last");
                if (i == ActivePage)
                {
                    divTag.Attributes.Add("style", "background :  #D9D9D9;");
                }

                PageValues["pageIndex"] = i;
                var url = urlHelper.Action(PageAction, PageController, PageValues);

                // Tạo thẻ a với href "url"
                var aTag = new TagBuilder("a");
                aTag.Attributes.Add("href", url);
                aTag.InnerHtml.Append(i.ToString());
                // Thêm thẻ a vào div
                divTag.InnerHtml.AppendHtml(aTag);
                // Ghi đoạn HTML đã tạo vào output
                output.Content.AppendHtml(divTag);
            }
            // Nếu vẫn còn trang phía sau, hiển thị thêm dấu ...
            if (endIndex < TotalPages) 
            {
                output.Content.AppendHtml(@"<div class=""Model-3dots"">...</div>");
            }
            // Thêm Next
            {
                TagBuilder divTag = new TagBuilder("div");
                divTag.AddCssClass("Model-prev-pagenumber-next-last");
                PageValues["pageIndex"] = Math.Min(ActivePage + 1, TotalPages);
                var url = urlHelper.Action(PageAction, PageController, PageValues);
                // Tạo thẻ a với href "url"
                var aTag = new TagBuilder("a");
                aTag.Attributes.Add("href", url);
                aTag.InnerHtml.Append("Next");
                // Thêm thẻ a vào div
                divTag.InnerHtml.AppendHtml(aTag);
                // Append output
                output.Content.AppendHtml(divTag);
            }
            // Thêm Last
            {
                TagBuilder divTag = new TagBuilder("div");
                divTag.AddCssClass("Model-prev-pagenumber-next-last");
                PageValues["pageIndex"] = TotalPages;
                var url = urlHelper.Action(PageAction, PageController, PageValues);
                // Tạo thẻ a với href "url"
                var aTag = new TagBuilder("a");
                aTag.Attributes.Add("href", url);
                aTag.InnerHtml.Append("Last");
                // Thêm thẻ a vào div
                divTag.InnerHtml.AppendHtml(aTag);
                // Append output
                output.Content.AppendHtml(divTag);
            }
        }
    }
}

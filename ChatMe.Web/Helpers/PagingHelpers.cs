using ChatMe.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ChatMe.Web.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
        PageInfo pageInfo, Func<int, string> pageUrl)
        {
            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");
            for (int i = 1; i <= pageInfo.TotalPages; i++) {
                TagBuilder li = new TagBuilder("li");

                if (i == pageInfo.PageNumber) {
                    li.AddCssClass("active");
                }

                TagBuilder a = new TagBuilder("a");
                a.MergeAttribute("href", pageUrl(i));
                a.InnerHtml = i.ToString();
                li.InnerHtml = a.ToString();

                ul.InnerHtml += li.ToString();
            }
            return MvcHtmlString.Create(ul.ToString());
        }
    }
}
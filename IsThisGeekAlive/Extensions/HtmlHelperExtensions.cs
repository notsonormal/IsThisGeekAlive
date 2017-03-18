using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IsThisGeekAlive.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent BoldText(
            this IHtmlHelper htmlHelper, 
            string text, 
            string textClass = "text-info")
        {
            var contentBuilder = new HtmlContentBuilder()
                .AppendHtml($"<b class={textClass}>{text}</b>");

            return contentBuilder;
        }
    
        public static IHtmlContent BoldTextFor<TModel, TResult>(
            this IHtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TResult>> expression,
            string textClass = "text-info")
            where TModel : class
        {
            var displayFor = htmlHelper.DisplayFor(expression);

            var contentBuilder = new HtmlContentBuilder()
                .AppendHtml($"<b class={textClass}>")
                .AppendHtml(displayFor)
                .AppendHtml("</b>");

            return contentBuilder;
        }
    }
}

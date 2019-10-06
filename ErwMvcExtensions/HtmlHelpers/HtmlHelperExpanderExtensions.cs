using ErwMvcExtensions.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace ErwMvcExtensions.HtmlHelpers
{
    public static class HtmlHelperExpanderExtensions
    {
        public static ErwExpander BeginExpander(this HtmlHelper htmlHelper)
        {
            return BeginExpander(htmlHelper, 
                                 null, 
                                 ExpanderDefaultStyles.Enabled, 
                                 ExpanderDisplayMode.Collapsed, 
                                 new RouteValueDictionary());
        }

        public static ErwExpander BeginExpander(this HtmlHelper htmlHelper, string titleInnerHtml)
        {
            return BeginExpander(htmlHelper, 
                                 titleInnerHtml, 
                                 ExpanderDefaultStyles.Enabled, 
                                 ExpanderDisplayMode.Collapsed, 
                                 new RouteValueDictionary());
        }

        public static ErwExpander BeginExpander(this HtmlHelper htmlHelper, ExpanderDisplayMode expanderDisplayMode)
        {
            return BeginExpander(htmlHelper, 
                                 null, 
                                 ExpanderDefaultStyles.Enabled, 
                                 expanderDisplayMode, 
                                 new RouteValueDictionary());
        }

        public static ErwExpander BeginExpander(this HtmlHelper htmlHelper, object htmlAttributes)
        {
            return BeginExpander(htmlHelper, 
                                 null, 
                                 ExpanderDefaultStyles.Enabled, 
                                 ExpanderDisplayMode.Collapsed, 
                                 HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static ErwExpander BeginExpander(this HtmlHelper htmlHelper, IDictionary<string, object> htmlAttributes)
        {
            return BeginExpander(htmlHelper,
                                 null, 
                                 ExpanderDefaultStyles.Enabled, 
                                 ExpanderDisplayMode.Collapsed, 
                                 htmlAttributes);
        }

        public static ErwExpander BeginExpander(this HtmlHelper htmlHelper, string titleInnerHtml, ExpanderDefaultStyles expanderDefaultStyles)
        {
            return BeginExpander(htmlHelper, 
                                 titleInnerHtml, 
                                 expanderDefaultStyles, 
                                 ExpanderDisplayMode.Collapsed, 
                                 new RouteValueDictionary());
        }

        public static ErwExpander BeginExpander(this HtmlHelper htmlHelper, string titleInnerHtml, ExpanderDisplayMode expanderDisplayMode)
        {
            return BeginExpander(htmlHelper, 
                                 titleInnerHtml, 
                                 ExpanderDefaultStyles.Enabled, 
                                 expanderDisplayMode, 
                                 new RouteValueDictionary());
        }

        public static ErwExpander BeginExpander(this HtmlHelper htmlHelper, string titleInnerHtml, object htmlAttributes)
        {
            return BeginExpander(htmlHelper, 
                                 titleInnerHtml, 
                                 ExpanderDefaultStyles.Enabled, 
                                 ExpanderDisplayMode.Collapsed, 
                                 HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static ErwExpander BeginExpander(this HtmlHelper htmlHelper, string titleInnerHtml, IDictionary<string, object> htmlAttributes)
        {
            return BeginExpander(htmlHelper, 
                                 titleInnerHtml, 
                                 ExpanderDefaultStyles.Enabled, 
                                 ExpanderDisplayMode.Collapsed,
                                 htmlAttributes);
        }

        public static ErwExpander BeginExpander(this HtmlHelper htmlHelper, ExpanderDisplayMode expanderDisplayMode, object htmlAttributes)
        {
            return BeginExpander(htmlHelper, 
                                 null, 
                                 ExpanderDefaultStyles.Enabled, 
                                 expanderDisplayMode, 
                                 HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static ErwExpander BeginExpander(this HtmlHelper htmlHelper, ExpanderDisplayMode expanderDisplayMode, IDictionary<string, object> htmlAttributes)
        {
            return BeginExpander(htmlHelper, 
                                 null, 
                                 ExpanderDefaultStyles.Enabled, 
                                 expanderDisplayMode, 
                                 htmlAttributes);
        }

        public static ErwExpander BeginExpander(this HtmlHelper htmlHelper, string titleInnerHtml, ExpanderDefaultStyles expanderDefaultStyles, ExpanderDisplayMode expanderDisplayMode)
        {
            return BeginExpander(htmlHelper, 
                                 titleInnerHtml, 
                                 expanderDefaultStyles, 
                                 expanderDisplayMode, 
                                 new RouteValueDictionary());
        }

        public static ErwExpander BeginExpander(this HtmlHelper htmlHelper, string titleInnerHtml, ExpanderDefaultStyles expanderDefaultStyles, object htmlAttributes)
        {
            return BeginExpander(htmlHelper, 
                                 titleInnerHtml, 
                                 expanderDefaultStyles, 
                                 ExpanderDisplayMode.Collapsed, 
                                 HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static ErwExpander BeginExpander(this HtmlHelper htmlHelper, string titleInnerHtml, ExpanderDefaultStyles expanderDefaultStyles, IDictionary<string, object> htmlAttributes)
        {
            return BeginExpander(htmlHelper, 
                                 titleInnerHtml, 
                                 expanderDefaultStyles, 
                                 ExpanderDisplayMode.Collapsed, 
                                 htmlAttributes);
        }

        public static ErwExpander BeginExpander(this HtmlHelper htmlHelper, string titleInnerHtml, ExpanderDefaultStyles expanderDefaultStyles, ExpanderDisplayMode expanderDisplayMode, object htmlAttributes)
        {
            return ErwExpanderHelper(htmlHelper, 
                                     titleInnerHtml, 
                                     expanderDefaultStyles,
                                     expanderDisplayMode, 
                                     HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static ErwExpander BeginExpander(this HtmlHelper htmlHelper, string titleInnerHtml, ExpanderDefaultStyles expanderDefaultStyles, ExpanderDisplayMode expanderDisplayMode, IDictionary<string, object> htmlAttributes)
        {
            return ErwExpanderHelper(htmlHelper, 
                                     titleInnerHtml, 
                                     expanderDefaultStyles, 
                                     expanderDisplayMode, 
                                     htmlAttributes);
        }

        public static void EndExpander(this HtmlHelper htmlHelper)
        {
            EndExpander(htmlHelper.ViewContext);
        }

        internal static void EndExpander(ViewContext viewContext)
        {
            viewContext.Writer.Write("</div>");
            viewContext.Writer.Write("</div>");
        }

        private static ErwExpander ErwExpanderHelper(this HtmlHelper htmlHelper, string titleInnerHtml, ExpanderDefaultStyles expanderDefaultStyles,  ExpanderDisplayMode expanderDisplayMode, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder mainExpanderTag = new TagBuilder("div");
            htmlAttributes.Add("data-ui-type", "expander");
            mainExpanderTag.MergeAttributes(htmlAttributes);
            mainExpanderTag.AddCssClass("erw-expander");

            TagBuilder titleExpanderTag = new TagBuilder("div");
            titleExpanderTag.MergeAttribute("data-ui-type", "expander-title");

            TagBuilder contentExpanderTag = new TagBuilder("div");
            contentExpanderTag.MergeAttribute("data-ui-type", "expander-content");

            switch (expanderDefaultStyles)
            {
                case ExpanderDefaultStyles.Enabled:
                    titleExpanderTag.AddCssClass("erw-expander-title");

                    TagBuilder titlePositionExpanderTag = new TagBuilder("div");
                    titlePositionExpanderTag.AddCssClass("erw-expander-title-position");

                    TagBuilder arrowTitleExpaderTag = new TagBuilder("span");
                    arrowTitleExpaderTag.AddCssClass("erw-arrow");
                    arrowTitleExpaderTag.InnerHtml = new TagBuilder("span").ToString(TagRenderMode.Normal) + new TagBuilder("span").ToString(TagRenderMode.Normal);

                    titlePositionExpanderTag.InnerHtml = arrowTitleExpaderTag.ToString(TagRenderMode.Normal) + titleInnerHtml ?? "Expander";
                    titleExpanderTag.InnerHtml = titlePositionExpanderTag.ToString(TagRenderMode.Normal);
                    break;
                case ExpanderDefaultStyles.Disabled:
                    titleExpanderTag.InnerHtml = titleInnerHtml;
                    break;
                default:
                    break;
            }

            switch (expanderDisplayMode)
            {
                case ExpanderDisplayMode.Collapsed:
                    contentExpanderTag.MergeAttribute("style", "display: none;");
                    break;
                case ExpanderDisplayMode.Expanded:
                    break;
                default:
                    break;
            }

            htmlHelper.ViewContext.Writer.Write(mainExpanderTag.ToString(TagRenderMode.StartTag));
            htmlHelper.ViewContext.Writer.Write(titleExpanderTag.ToString(TagRenderMode.Normal));
            htmlHelper.ViewContext.Writer.Write(contentExpanderTag.ToString(TagRenderMode.StartTag));
            var theExpander = new ErwExpander(htmlHelper.ViewContext);

            return theExpander;
        }
    }

    public enum ExpanderDisplayMode : int
    {
        Collapsed = 0,
        Expanded = 1
    }

    public enum ExpanderDefaultStyles: int
    {
        Enabled = 0,
        Disabled = 1
    }
}

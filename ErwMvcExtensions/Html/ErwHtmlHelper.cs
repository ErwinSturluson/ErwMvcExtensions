using System;
using System.Globalization;
using System.Web.Mvc;

namespace ErwMvcExtensions.Html
{
    public static class ErwHtmlHelper
    {
        public static string GetErwInputTypeString(ErwInputType inputType)
        {
            string inputTypeString = string.Empty;

            switch (inputType)
            {
                case ErwInputType.Upload:
                    inputTypeString = "file";
                    break;
                case ErwInputType.AutoComplete:
                    inputTypeString = "text";
                    break;
                default:
                    break;
            }

            return inputTypeString;
        }

        public static string GetHttpMethodString(HttpMethodType httpMethodType)
        {
            string httpMethodTypeString = string.Empty;

            switch (httpMethodType)
            {
                case HttpMethodType.GET:
                    httpMethodTypeString = "GET";
                    break;
                case HttpMethodType.POST:
                    httpMethodTypeString = "POST";
                    break;
                case HttpMethodType.PUT:
                    httpMethodTypeString = "PUT";
                    break;
                case HttpMethodType.DELETE:
                    httpMethodTypeString = "DELETE";
                    break;
                case HttpMethodType.HEAD:
                    httpMethodTypeString = "HEAD";
                    break;
                case HttpMethodType.CONNECT:
                    httpMethodTypeString = "CONNECT";
                    break;
                case HttpMethodType.OPTIONS:
                    httpMethodTypeString = "OPTIONS";
                    break;
                case HttpMethodType.TRACE:
                    httpMethodTypeString = "TRACE";
                    break;
                default:
                    break;
            }

            return httpMethodTypeString;
        }

        public static object GetModelStateValue(this HtmlHelper htmlHelper, string key, Type destinationType)
        {
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(key, out modelState))
            {
                if (modelState.Value != null)
                {
                    return modelState.Value.ConvertTo(destinationType, null);
                }
            }
            return null;
        }

        public static string EvalString(this HtmlHelper htmlHelper, string key)
        {
            return Convert.ToString(htmlHelper.ViewData.Eval(key), CultureInfo.CurrentCulture);
        }

        public static string EvalString(this HtmlHelper htmlHelper, string key, string format)
        {
            return Convert.ToString(htmlHelper.ViewData.Eval(key, format), CultureInfo.CurrentCulture);
        }

        public static MvcHtmlString ToMvcHtmlString(this TagBuilder tagBuilder, TagRenderMode renderMode)
        {
            return MvcHtmlString.Create(tagBuilder.ToString(renderMode));
        }

    }

    public enum ErwInputType : int
    {
        Upload = 0,
        AutoComplete = 1
    }

    public enum HttpMethodType : int
    {
        GET = 0,
        POST = 1,
        PUT = 2,
        DELETE = 3,
        HEAD = 4,
        CONNECT = 5,
        OPTIONS = 6,
        TRACE = 7,
        None = 8
    }
}

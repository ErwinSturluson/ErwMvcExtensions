using ErwMvcExtensions.Html;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace ErwMvcExtensions.HtmlHelpers
{
    public static class HtmlHelperUploadForExtensions
    {
        public static MvcHtmlString UploadFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return UploadForHelper(htmlHelper,
                                   metadata,
                                   metadata.Model,
                                   ExpressionHelper.GetExpressionText(expression),
                                   null,
                                   null);
        }

        public static MvcHtmlString UploadFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return UploadForHelper(htmlHelper,
                                   metadata,
                                   metadata.Model,
                                   ExpressionHelper.GetExpressionText(expression),
                                   format,
                                   null);
        }

        public static MvcHtmlString UploadFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return UploadForHelper(htmlHelper,
                                   metadata,
                                   metadata.Model,
                                   ExpressionHelper.GetExpressionText(expression),
                                   null,
                                   null);
        }

        public static MvcHtmlString UploadFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format, object htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return UploadForHelper(htmlHelper,
                                   metadata,
                                   metadata.Model,
                                   ExpressionHelper.GetExpressionText(expression),
                                   format,
                                   null);
        }

        public static MvcHtmlString UploadFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return UploadForHelper(htmlHelper,
                                   metadata,
                                   metadata.Model,
                                   ExpressionHelper.GetExpressionText(expression),
                                   null,
                                   htmlAttributes);
        }

        public static MvcHtmlString UploadFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return UploadForHelper(htmlHelper,
                                   metadata,
                                   metadata.Model,
                                   ExpressionHelper.GetExpressionText(expression),
                                   format,
                                   htmlAttributes);
        }

        private static MvcHtmlString UploadForHelper(this HtmlHelper htmlHelper, ModelMetadata metadata, object model, string expression, string format, IDictionary<string, object> htmlAttributes)
        {
            return HtmlHelperInputExtensions.InputHelper(htmlHelper,
                                             ErwInputType.Upload,
                                             metadata,
                                             expression,
                                             model,
                                             useViewData: false,
                                             isChecked: false,
                                             setId: true,
                                             isExplicitValue: true,
                                             format: format,
                                             htmlAttributes: htmlAttributes,
                                             routeValues: null,
                                             HttpMethodType.None,
                                             markupGenerator: null);
        }
    }
}

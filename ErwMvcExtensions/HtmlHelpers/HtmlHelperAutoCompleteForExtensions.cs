using ErwMvcExtensions.Html;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace ErwMvcExtensions.HtmlHelpers
{
    public static class HtmlHelperAutoСompleteForExtensions
    {
        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         null,
                                         routeValues,
                                         HttpMethodType.POST,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         null,
                                         new RouteValueDictionary(routeValues),
                                         HttpMethodType.POST,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, HttpMethodType httpMethodType)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         null,
                                         routeValues,
                                         httpMethodType,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, HttpMethodType httpMethodType)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         null,
                                         new RouteValueDictionary(routeValues),
                                         httpMethodType,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         null,
                                         routeValues,
                                         HttpMethodType.POST,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         null,
                                         new RouteValueDictionary(routeValues),
                                         HttpMethodType.POST,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, HttpMethodType httpMethodType, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         null,
                                         routeValues,
                                         httpMethodType,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, HttpMethodType httpMethodType, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         null,
                                         new RouteValueDictionary(routeValues),
                                         httpMethodType,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, string format)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         null,
                                         routeValues,
                                         HttpMethodType.POST,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, string format)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         null,
                                         new RouteValueDictionary(routeValues),
                                         HttpMethodType.POST,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, HttpMethodType httpMethodType, string format)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         null,
                                         routeValues,
                                         httpMethodType,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, HttpMethodType httpMethodType, string format)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         null,
                                         new RouteValueDictionary(routeValues),
                                         httpMethodType,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, string format, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         null,
                                         routeValues,
                                         HttpMethodType.POST,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, string format, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         null,
                                         new RouteValueDictionary(routeValues),
                                         HttpMethodType.POST,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, HttpMethodType httpMethodType, string format, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         null,
                                         routeValues,
                                         httpMethodType,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, HttpMethodType httpMethodType, string format, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         null,
                                         new RouteValueDictionary(routeValues),
                                         httpMethodType,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, object htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                                         routeValues,
                                         HttpMethodType.POST,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, object htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                                         new RouteValueDictionary(routeValues),
                                         HttpMethodType.POST,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, HttpMethodType httpMethodType, object htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                                         routeValues,
                                         httpMethodType,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, HttpMethodType httpMethodType, object htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                                         new RouteValueDictionary(routeValues),
                                         httpMethodType,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, object htmlAttributes, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                                         routeValues,
                                         HttpMethodType.POST,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, object htmlAttributes, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                                         new RouteValueDictionary(routeValues),
                                         HttpMethodType.POST,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, HttpMethodType httpMethodType, object htmlAttributes, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                                         routeValues,
                                         httpMethodType,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, HttpMethodType httpMethodType, object htmlAttributes, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                                         new RouteValueDictionary(routeValues),
                                         httpMethodType,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, string format, object htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                                         routeValues,
                                         HttpMethodType.POST,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, string format, object htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                                         new RouteValueDictionary(routeValues),
                                         HttpMethodType.POST,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, HttpMethodType httpMethodType, string format, object htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                                         routeValues,
                                         httpMethodType,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, HttpMethodType httpMethodType, string format, object htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                                         new RouteValueDictionary(routeValues),
                                         httpMethodType,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, string format, object htmlAttributes, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                                         routeValues,
                                         HttpMethodType.POST,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, string format, object htmlAttributes, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                                         new RouteValueDictionary(routeValues),
                                         HttpMethodType.POST,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, HttpMethodType httpMethodType, string format, object htmlAttributes, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                                         routeValues,
                                         httpMethodType,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, HttpMethodType httpMethodType, string format, object htmlAttributes, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                                         new RouteValueDictionary(routeValues),
                                         httpMethodType,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         htmlAttributes,
                                         routeValues,
                                         HttpMethodType.POST,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         htmlAttributes,
                                         new RouteValueDictionary(routeValues),
                                         HttpMethodType.POST,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, HttpMethodType httpMethodType, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         htmlAttributes,
                                         routeValues,
                                         httpMethodType,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, HttpMethodType httpMethodType, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         htmlAttributes,
                                         new RouteValueDictionary(routeValues),
                                         httpMethodType,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         htmlAttributes,
                                         routeValues,
                                         HttpMethodType.POST,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, IDictionary<string, object> htmlAttributes, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         htmlAttributes,
                                         new RouteValueDictionary(routeValues),
                                         HttpMethodType.POST,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, HttpMethodType httpMethodType, IDictionary<string, object> htmlAttributes, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         htmlAttributes,
                                         routeValues,
                                         httpMethodType,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, HttpMethodType httpMethodType, IDictionary<string, object> htmlAttributes, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         null,
                                         htmlAttributes,
                                         new RouteValueDictionary(routeValues),
                                         httpMethodType,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, string format, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         htmlAttributes,
                                         routeValues,
                                         HttpMethodType.POST,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, string format, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         htmlAttributes,
                                         new RouteValueDictionary(routeValues),
                                         HttpMethodType.POST,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, HttpMethodType httpMethodType, string format, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         htmlAttributes,
                                         routeValues,
                                         httpMethodType,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, HttpMethodType httpMethodType, string format, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         htmlAttributes,
                                         new RouteValueDictionary(routeValues),
                                         httpMethodType,
                                         null);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, string format, IDictionary<string, object> htmlAttributes, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         htmlAttributes,
                                         routeValues,
                                         HttpMethodType.POST,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, string format, IDictionary<string, object> htmlAttributes, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         htmlAttributes,
                                         new RouteValueDictionary(routeValues),
                                         HttpMethodType.POST,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, RouteValueDictionary routeValues, HttpMethodType httpMethodType, string format, IDictionary<string, object> htmlAttributes, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         htmlAttributes,
                                         routeValues,
                                         httpMethodType,
                                         markupGenerator);
        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object routeValues, HttpMethodType httpMethodType, string format, IDictionary<string, object> htmlAttributes, Func<object, object> markupGenerator)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return AutoCompleteForHelper(htmlHelper,
                                         metadata,
                                         metadata.Model,
                                         ExpressionHelper.GetExpressionText(expression),
                                         format,
                                         htmlAttributes,
                                         new RouteValueDictionary(routeValues),
                                         httpMethodType,
                                         markupGenerator);
        }

        private static MvcHtmlString AutoCompleteForHelper(this HtmlHelper htmlHelper, ModelMetadata metadata, object model, string expression, string format, IDictionary<string, object> htmlAttributes, RouteValueDictionary routeValues, HttpMethodType httpMethodType, Func<object, object> markupGenerator)
        {
            return HtmlHelperInputExtensions.InputHelper(htmlHelper,
                                             ErwInputType.AutoComplete,
                                             metadata,
                                             expression,
                                             model,
                                             useViewData: false,
                                             isChecked: false,
                                             setId: true,
                                             isExplicitValue: true,
                                             format: format,
                                             htmlAttributes: htmlAttributes,
                                             routeValues: routeValues,
                                             httpMethodType: httpMethodType,
                                             markupGenerator: markupGenerator);
        }
    }
}

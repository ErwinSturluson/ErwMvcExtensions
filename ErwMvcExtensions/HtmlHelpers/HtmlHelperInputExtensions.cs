using ErwMvcExtensions.Html;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ErwMvcExtensions.HtmlHelpers
{
    internal static class HtmlHelperInputExtensions
    {
        internal static MvcHtmlString InputHelper(HtmlHelper htmlHelper,
                                                  ErwInputType inputType,
                                                  ModelMetadata metadata,
                                                  string name,
                                                  object value,
                                                  bool useViewData,
                                                  bool isChecked,
                                                  bool setId,
                                                  bool isExplicitValue,
                                                  string format,
                                                  IDictionary<string, object> htmlAttributes,
                                                  RouteValueDictionary routeValues,
                                                  HttpMethodType httpMethodType,
                                                  Func<object, object> markupGenerator)
        {
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (string.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("Name can not be null or empty.");
            }

            TagBuilder tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("type", ErwHtmlHelper.GetErwInputTypeString(inputType));
            tagBuilder.MergeAttribute("name", fullName, true);

            TagRenderMode tagRenderMode = TagRenderMode.SelfClosing;

            string valueParameter = htmlHelper.FormatValue(value, format);

            if (setId)
            {
                tagBuilder.GenerateId(fullName);
            }

            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullName, out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                }
            }

            tagBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));

            switch (inputType)
            {
                case ErwInputType.Upload:
                    tagBuilder.MergeAttribute("data-ui-type", "upload");
                    PropertyInfo generatedProperty = metadata.ContainerType.GetProperty(name);
                    if (generatedProperty.PropertyType == typeof(HttpPostedFileBase[]))
                    {
                        tagBuilder.MergeAttribute("multiple", "multiple");
                    }
                    break;
                case ErwInputType.AutoComplete:
                    string dataObtainAction = UrlHelper.GenerateUrl(null, null, null, routeValues, htmlHelper.RouteCollection, htmlHelper.ViewContext.RequestContext, false);
                    string httpMethod = ErwHtmlHelper.GetHttpMethodString(httpMethodType);

                    if (markupGenerator == null)
                    {
                        TagBuilder mainAutoCompleteTag = new TagBuilder("div");
                        mainAutoCompleteTag.MergeAttribute("data-ui-type", "autocomplete");
                        mainAutoCompleteTag.MergeAttribute("data-ui-route", dataObtainAction);
                        mainAutoCompleteTag.MergeAttribute("data-ui-method", httpMethod);
                        mainAutoCompleteTag.AddCssClass("erw-autocomplete");

                        TagBuilder inputContainerAutoCompleteTag = new TagBuilder("div");
                        inputContainerAutoCompleteTag.MergeAttribute("data-ui-type", "autocomplete-input");
                        inputContainerAutoCompleteTag.AddCssClass("erw-autocomplete-input");
                        inputContainerAutoCompleteTag.InnerHtml = tagBuilder.ToString(TagRenderMode.SelfClosing);

                        TagBuilder listAutoCompleteTag = new TagBuilder("div");
                        listAutoCompleteTag.MergeAttribute("data-ui-type", "autocomplete-list");
                        listAutoCompleteTag.AddCssClass("erw-autocomplete-list");

                        mainAutoCompleteTag.InnerHtml = inputContainerAutoCompleteTag.ToString(TagRenderMode.Normal) + listAutoCompleteTag.ToString(TagRenderMode.Normal);

                        tagBuilder = mainAutoCompleteTag;
                        tagRenderMode = TagRenderMode.Normal;
                    }
                    else
                    {
                        TagBuilder mainAutoCompleteTag = new TagBuilder("div");
                        mainAutoCompleteTag.MergeAttribute("data-ui-type", "autocomplete");
                        mainAutoCompleteTag.MergeAttribute("data-ui-route", dataObtainAction);
                        mainAutoCompleteTag.MergeAttribute("data-ui-method", httpMethod);

                        TagBuilder inputContainerAutoCompleteTag = new TagBuilder("div");
                        inputContainerAutoCompleteTag.MergeAttribute("data-ui-type", "autocomplete-input");
                        inputContainerAutoCompleteTag.InnerHtml = tagBuilder.ToString(TagRenderMode.SelfClosing);

                        TagBuilder listAutoCompleteTag = new TagBuilder("div");
                        listAutoCompleteTag.MergeAttribute("data-ui-type", "autocomplete-list");

                        tagBuilder = markupGenerator.Invoke(new TagBuilder[] {
                                                            mainAutoCompleteTag,
                                                            inputContainerAutoCompleteTag,
                                                            listAutoCompleteTag }) as TagBuilder;

                        tagRenderMode = TagRenderMode.Normal;
                    }

                    break;
                default:
                    string attemptedValue = (string)htmlHelper.GetModelStateValue(fullName, typeof(string));
                    tagBuilder.MergeAttribute("value", attemptedValue ?? ((useViewData) ? htmlHelper.EvalString(fullName, format) : valueParameter), isExplicitValue);
                    break;
            }

            return tagBuilder.ToMvcHtmlString(tagRenderMode);
        }
    }
}

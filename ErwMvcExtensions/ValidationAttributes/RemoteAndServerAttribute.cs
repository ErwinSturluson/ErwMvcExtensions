using ErwMvcExtensions.Html;
using ErwMvcExtensions.System;
using ErwMvcExtensions.ValidationAttributes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ErwMvcExtensions.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class RemoteAndServerAttribute : ValidationAttribute, IClientValidatable
    {
        private HttpMethodType httpMethodType;
        private RouteValueDictionary routeValues;

        protected RemoteAndServerAttribute(object routeValues, HttpMethodType httpMethodType)
        {
            this.routeValues = new RouteValueDictionary(routeValues);
            this.httpMethodType = httpMethodType;
        }

        public RemoteAndServerAttribute(string controller, string action)
            : this(null, controller, action)
        {
        }

        public RemoteAndServerAttribute(string controller, string action, HttpMethodType httpMethodType)
            : this(null, controller, action, httpMethodType)
        {
        }

        public RemoteAndServerAttribute(string area, string controller, string action)
            : this(area, controller, action, HttpMethodType.POST)
        {
        }

        public RemoteAndServerAttribute(string area, string controller, string action, HttpMethodType httpMethodType)
            : this(new { area = area, controller = controller, action = action}, httpMethodType)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            ResourceManager resourceManager = null;
            string formattingPattern = string.Empty;

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentOutOfRangeException("Parameter string \"name\" can't be null or empty.");
            }

            try
            {
                if (this.ErrorMessageResourceType == null)
                {
                    resourceManager = new ResourceManager(typeof(ValidationAttributeErrorMessageResources));
                    formattingPattern = resourceManager.GetString("RemoteAndServerAttribute", CultureInfoExtensions.GetCultureFromHttp(HttpContext.Current.Request));
                }
                else if (string.IsNullOrEmpty(this.ErrorMessageResourceName))
                {
                    resourceManager = new ResourceManager(this.ErrorMessageResourceType);
                    formattingPattern = resourceManager.GetString("RemoteAndServerAttribute", CultureInfoExtensions.GetCultureFromHttp(HttpContext.Current.Request));
                }
                else
                {
                    resourceManager = new ResourceManager(this.ErrorMessageResourceType);
                    formattingPattern = resourceManager.GetString(this.ErrorMessageResourceName, CultureInfoExtensions.GetCultureFromHttp(HttpContext.Current.Request));
                }

                if (string.IsNullOrEmpty(formattingPattern))
                {
                    throw new FormatException("Variable string \"formattingPattern\" can't be null or empty.");
                }
            }
            catch (Exception)
            {
                formattingPattern = "This value is not valid for the {0} field.";
            }

            string formattedErrorMessage = string.Format(formattingPattern, name);

            return formattedErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string currentPropertyDisplayName = !string.IsNullOrEmpty(validationContext.DisplayName) ? validationContext.DisplayName : validationContext.MemberName;

                Assembly asm = validationContext.ObjectInstance.GetType().Assembly;
                string controllerName = asm.GetFullyQualifiedControllerName(this.routeValues);
                Type controllerType = asm.GetType(controllerName);
                object controller = Activator.CreateInstance(controllerType);
                MethodInfo action = controllerType.GetMethod(this.routeValues["action"].ToString());
                JsonResult actionResult = action.Invoke(controller, new object[] { value }) as JsonResult;

                if (!(bool)actionResult.Data)
                {
                    return new ValidationResult(this.FormatErrorMessage(currentPropertyDisplayName));
                }                
            }

            return ValidationResult.Success;
        }


        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            string currentPropertyDisplayName = metadata.GetDisplayName();
            string url = ErwUrlHelper.GenerateSimpleUrl(this.routeValues);
            string method = ErwHtmlHelper.GetHttpMethodString(this.httpMethodType);

            yield return new ModelClientValidationRemoteAndServerRule(this.FormatErrorMessage(currentPropertyDisplayName), url, method);
        }
    }

    public class ModelClientValidationRemoteAndServerRule : ModelClientValidationRule
    {
        public ModelClientValidationRemoteAndServerRule(string errorMessage, string url, string method)
        {
            this.ValidationType = "remoteandserver";
            this.ErrorMessage = errorMessage;
            this.ValidationParameters.Add("url", url);
            this.ValidationParameters.Add("method", method);
        }
    }
}

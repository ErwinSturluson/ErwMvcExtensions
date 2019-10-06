using ErwMvcExtensions.System;
using ErwMvcExtensions.ValidationAttributes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ErwMvcExtensions.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class HttpPostedFileBaseExtensionsAttribute : ValidationAttribute, IClientValidatable
    {
        private List<string> validExtensions;

        public HttpPostedFileBaseExtensionsAttribute(params string[] validExtensions)
        {
            this.validExtensions = validExtensions.Select(e => e.StartsWith(".") ? e : "." + e).ToList();
        }

        public HttpPostedFileBaseExtensionsAttribute(string validExtensions)
            : this(validExtensions.Split(','))
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
                    formattingPattern = resourceManager.GetString("HttpPostedFileBaseExtensions", CultureInfoExtensions.GetCultureFromHttp(HttpContext.Current.Request));
                }
                else if (string.IsNullOrEmpty(this.ErrorMessageResourceName))
                {
                    resourceManager = new ResourceManager(this.ErrorMessageResourceType);
                    formattingPattern = resourceManager.GetString("HttpPostedFileBaseExtensions", CultureInfoExtensions.GetCultureFromHttp(HttpContext.Current.Request));
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
                formattingPattern = "Valid extensions for the {0} field: {1}.";
            }


            string validExtensionsString = this.validExtensions.Aggregate((first, second) => first + ", " + second);

            string formattedErrorMessage = string.Format(formattingPattern, name, validExtensionsString);

            return formattedErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string currentPropertyDisplayName = !string.IsNullOrEmpty(validationContext.DisplayName) ? validationContext.DisplayName : validationContext.MemberName;

            if (value as HttpPostedFileBase != null)
            {
                if (!this.validExtensions.Any(e => (value as HttpPostedFileBase).FileName.ToLower().EndsWith(e.ToLower())))
                {
                    return new ValidationResult(this.FormatErrorMessage(currentPropertyDisplayName));
                }
            }
            else if (value as HttpPostedFileBase[] != null)
            {
                foreach (var file in value as HttpPostedFileBase[])
                {
                    if (file == null)
                    {
                        return ValidationResult.Success;
                    }
                    else if (!this.validExtensions.Any(e => file.FileName.ToLower().EndsWith(e.ToLower())))
                    {
                        return new ValidationResult(this.FormatErrorMessage(currentPropertyDisplayName));
                    }
                }
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            string currentPropertyDisplayName = metadata.GetDisplayName();
            string validExtensionsString = this.validExtensions.Aggregate((first, second) => first + "," + second);

            yield return new ModelClientValidationHttpPostedFileBaseExtensionsRule(this.FormatErrorMessage(currentPropertyDisplayName), validExtensionsString);
        }
    }

    public class ModelClientValidationHttpPostedFileBaseExtensionsRule : ModelClientValidationRule
    {
        public ModelClientValidationHttpPostedFileBaseExtensionsRule(string errorMessage, string validExtensions)
        {
            this.ValidationType = "httppostedfilebaseextensions";
            this.ErrorMessage = errorMessage;
            this.ValidationParameters.Add("validextensions", validExtensions);
        }
    }
}

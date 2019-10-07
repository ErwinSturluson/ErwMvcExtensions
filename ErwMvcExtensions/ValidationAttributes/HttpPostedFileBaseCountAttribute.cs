using ErwMvcExtensions.System;
using ErwMvcExtensions.ValidationAttributes.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Resources;
using System.Web;
using System.Web.Mvc;

namespace ErwMvcExtensions.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class HttpPostedFileBaseCountAttribute : ValidationAttribute, IClientValidatable
    {
        private int minCount;
        private int maxCount;

        public HttpPostedFileBaseCountAttribute(int maxCount)
            : this(1, maxCount)
        {
        }

        public HttpPostedFileBaseCountAttribute(int minCount, int maxCount)
        {
            this.minCount = minCount;
            this.maxCount = maxCount;
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
                    formattingPattern = resourceManager.GetString("HttpPostedFileBaseCount", CultureInfoExtensions.GetCultureFromHttp(HttpContext.Current.Request));
                }
                else if (string.IsNullOrEmpty(this.ErrorMessageResourceName))
                {
                    resourceManager = new ResourceManager(this.ErrorMessageResourceType);
                    formattingPattern = resourceManager.GetString("HttpPostedFileBaseCount", CultureInfoExtensions.GetCultureFromHttp(HttpContext.Current.Request));
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
                formattingPattern = "The number of downloaded files for the {0} should be in the range from {1} to {2}.";
            }

            string formattedErrorMessage = string.Format(formattingPattern, name, this.minCount, this.maxCount);

            return formattedErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string currentPropertyDisplayName = !string.IsNullOrEmpty(validationContext.DisplayName) ? validationContext.DisplayName : validationContext.MemberName;

            if (value as HttpPostedFileBase != null)
            {
                return ValidationResult.Success;
            }
            else if (value as HttpPostedFileBase[] != null)
            {
                if ((value as HttpPostedFileBase[]).Length < this.minCount || (value as HttpPostedFileBase[]).Length > this.maxCount)
                {
                    return new ValidationResult(this.FormatErrorMessage(currentPropertyDisplayName));
                }
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            string currentPropertyDisplayName = metadata.GetDisplayName();

            yield return new ModelClientValidationHttpPostedFileBaseCountRule(this.FormatErrorMessage(currentPropertyDisplayName), this.minCount, this.maxCount);
        }
    }

    public class ModelClientValidationHttpPostedFileBaseCountRule : ModelClientValidationRule
    {
        public ModelClientValidationHttpPostedFileBaseCountRule(string errorMessage, int minCount, int maxCount)
        {
            this.ValidationType = "httppostedfilebasecount";
            this.ErrorMessage = errorMessage;
            this.ValidationParameters.Add("mincount", minCount);
            this.ValidationParameters.Add("maxcount", maxCount);
        }
    }
}

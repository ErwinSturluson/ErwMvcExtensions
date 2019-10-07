using ErwMvcExtensions.System;
using ErwMvcExtensions.ValidationAttributes.Resources;
using ErwMvcExtensions.ViewModels.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.Mvc;

namespace ErwMvcExtensions.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AvailableIfAttribute : ValidationAttribute, IClientValidatable
    {
        private string propertyToCheck;
        private object propertyToCheckValue;
        private CompareMethod compareMethod;

        public AvailableIfAttribute(string propertyToCheck, object propertyToCheckValue)
            : this(propertyToCheck, propertyToCheckValue, CompareMethod.EqualsTo)
        {
        }

        public AvailableIfAttribute(string propertyToCheck, object propertyToCheckValue, CompareMethod compareMethod)
        {
            this.propertyToCheck = propertyToCheck;
            this.propertyToCheckValue = propertyToCheckValue;
            this.compareMethod = compareMethod;
        }

        public override string FormatErrorMessage(string name)
        {
            ResourceManager resourceManager = null;
            List<string> propertiesDisplayNames = name.Split(',').ToList();
            string formattingPattern = string.Empty;

            if (propertiesDisplayNames.Count < 3)
            {
                throw new ArgumentOutOfRangeException("At least 3 comma separated values must be passed to the FormatErrorMessage method. For example: \"CurrentPropertyName,PropertyToCheckName,PropertyToCheckValueName\".");
            }

            try
            {
                if (this.ErrorMessageResourceType == null)
                {
                    resourceManager = new ResourceManager(typeof(ValidationAttributeErrorMessageResources));
                    formattingPattern = resourceManager.GetString("AvailableIf", CultureInfoExtensions.GetCultureFromHttp(HttpContext.Current.Request));
                }
                else if (string.IsNullOrEmpty(this.ErrorMessageResourceName))
                {
                    resourceManager = new ResourceManager(this.ErrorMessageResourceType);
                    formattingPattern = resourceManager.GetString("AvailableIf", CultureInfoExtensions.GetCultureFromHttp(HttpContext.Current.Request));
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
                formattingPattern = "Field {0} available only if {1} {2} {3}.";
            }

            string currentPropertyDisplayName = propertiesDisplayNames.First();
            propertiesDisplayNames.Remove(currentPropertyDisplayName);
            string propertyToCheckDisplayName = propertiesDisplayNames.First();
            string propertyToCheckValueDisplayName = propertiesDisplayNames.Last();
            string compareMethodString = string.Empty;

            switch (this.compareMethod)
            {
                case CompareMethod.EqualsTo:
                    compareMethodString = "=";
                    break;
                case CompareMethod.NotEqualsTo:
                    compareMethodString = "!=";
                    break;
                default:
                    break;
            }
            if (string.IsNullOrEmpty(compareMethodString))
            {
                throw new FormatException("Variable string \"compareMethodString\" can't be null or empty.");
            }

            string formattedErrorMessage = string.Format(formattingPattern, currentPropertyDisplayName, propertyToCheckDisplayName, compareMethodString, propertyToCheckValueDisplayName);

            return formattedErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo property = validationContext.ObjectInstance.GetType().GetProperty(this.propertyToCheck);
            object propertyValue = property.GetValue(validationContext.ObjectInstance, null);

            if (value != null)
            {
                if (!this.IsAvailable(propertyValue))
                {
                    ResourceManager resourceManager = null;
                    string propertyDisplayName = string.Empty;
                    var propertiesDisplayNames = new List<string>();

                    propertiesDisplayNames.Add(!string.IsNullOrEmpty(validationContext.DisplayName) ? validationContext.DisplayName : validationContext.MemberName);

                    var propertyDisplayAttribute = property.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;

                    if (propertyDisplayAttribute == null || propertyDisplayAttribute.ResourceType == null)
                    {
                        resourceManager = new ResourceManager(typeof(PropertyDisplayNameResources));
                    }
                    else
                    {
                        resourceManager = new ResourceManager(propertyDisplayAttribute.ResourceType);
                    }

                    try
                    {
                        propertyDisplayName = resourceManager.GetString(propertyDisplayAttribute.Name, CultureInfoExtensions.GetCultureFromHttp(HttpContext.Current.Request));
                    }
                    catch (Exception)
                    {
                        propertiesDisplayNames.Add(property.Name);
                    }

                    propertiesDisplayNames.Add(this.propertyToCheckValue.ToString());

                    if (propertiesDisplayNames.Count > 1)
                    {
                        string propertiesDisplayNamesString = propertiesDisplayNames.Aggregate((first, second) => first + "," + second);

                        return new ValidationResult(this.FormatErrorMessage(propertiesDisplayNamesString));
                    }
                }
            }

            return ValidationResult.Success;
        }

        private bool IsAvailable(object actualPropertyToCheckValue)
        {
            bool comparsionRersult = false;

            switch (this.compareMethod)
            {
                case CompareMethod.EqualsTo:
                    comparsionRersult = actualPropertyToCheckValue != null && actualPropertyToCheckValue.Equals(this.propertyToCheckValue);
                    break;
                case CompareMethod.NotEqualsTo:
                    comparsionRersult = actualPropertyToCheckValue == null || !actualPropertyToCheckValue.Equals(this.propertyToCheckValue);
                    break;
                default:
                    break;
            }

            return comparsionRersult;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ResourceManager resourceManager = null;
            var propertiesDisplayNames = new List<string>();

            propertiesDisplayNames.Add(metadata.GetDisplayName());

            PropertyInfo otherProperty = metadata.ContainerType.GetProperty(this.propertyToCheck);

            var propertyDisplayAttribute = otherProperty.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;

            if (propertyDisplayAttribute == null || propertyDisplayAttribute.ResourceType == null)
            {
                resourceManager = new ResourceManager(typeof(PropertyDisplayNameResources));
            }
            else
            {
                resourceManager = new ResourceManager(propertyDisplayAttribute.ResourceType);
            }

            try
            {
                string propertyDisplayName = resourceManager.GetString(propertyDisplayAttribute.Name, CultureInfoExtensions.GetCultureFromHttp(HttpContext.Current.Request));
                if (string.IsNullOrEmpty(propertyDisplayName))
                {
                    throw new FormatException("Variable string \"propertyDisplayName\" can't be null or empty.");
                }
                propertiesDisplayNames.Add(propertyDisplayName);
            }
            catch (Exception)
            {
                propertiesDisplayNames.Add(otherProperty.Name);
            }

            propertiesDisplayNames.Add(this.propertyToCheckValue.ToString());

            string propertiesDisplayNamesString = propertiesDisplayNames.Aggregate((first, second) => first + "," + second);

            yield return new ModelClientValidationAvailableIfRule(this.FormatErrorMessage(propertiesDisplayNamesString), this.propertyToCheck, this.propertyToCheckValue, this.compareMethod);
        }
    }

    public class ModelClientValidationAvailableIfRule : ModelClientValidationRule
    {
        public ModelClientValidationAvailableIfRule(string errorMessage, string propertyToCheck, object propertyToCheckValue, CompareMethod compareMethod)
        {
            this.ValidationType = "availableif";
            this.ErrorMessage = errorMessage;
            this.ValidationParameters.Add("propertytocheck", propertyToCheck);
            this.ValidationParameters.Add("propertytocheckvalue", propertyToCheckValue);
            this.ValidationParameters.Add("comparemethod", compareMethod.ToString());
        }
    }
}

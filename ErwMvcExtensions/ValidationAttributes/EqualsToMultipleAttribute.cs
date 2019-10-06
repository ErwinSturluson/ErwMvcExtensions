using ErwMvcExtensions.System;
using ErwMvcExtensions.ValidationAttributes.Resources;
using ErwMvcExtensions.ViewModels.Resources;
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

namespace ErwMvcExtensions.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class EqualsToMultipleAttribute : ValidationAttribute, IClientValidatable
    {
        private List<string> propertiesToCompareNames;

        public EqualsToMultipleAttribute(params string[] propertiesToCompareNames)
        {
            this.propertiesToCompareNames = propertiesToCompareNames.ToList();
        }

        public EqualsToMultipleAttribute(string propertiesToCompareNames)
            : this(propertiesToCompareNames.Split(','))
        {
        }

        public override string FormatErrorMessage(string name)
        {
            ResourceManager resourceManager = null;
            List<string> propertiesDisplayNames = name.Split(',').ToList();
            string formattingPattern = string.Empty;

            if (propertiesDisplayNames.Count < 2)
            {
                throw new ArgumentOutOfRangeException("At least 2 comma separated values must be passed to the FormatErrorMessage method. For example: \"CurrentPropertyName,PropertyToCompareName\".");
            }

            try
            {
                if (this.ErrorMessageResourceType == null)
                {
                    resourceManager = new ResourceManager(typeof(ValidationAttributeErrorMessageResources));
                    formattingPattern = resourceManager.GetString("EqualsToMultiple", CultureInfoExtensions.GetCultureFromHttp(HttpContext.Current.Request));
                }
                else if (string.IsNullOrEmpty(this.ErrorMessageResourceName))
                {
                    resourceManager = new ResourceManager(this.ErrorMessageResourceType);
                    formattingPattern = resourceManager.GetString("EqualsToMultiple", CultureInfoExtensions.GetCultureFromHttp(HttpContext.Current.Request));
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
                formattingPattern = "{0} must be same as {1}.";
            }

            string currentPropertyDisplayName = propertiesDisplayNames.First();
            propertiesDisplayNames.Remove(currentPropertyDisplayName);
            string propertiesToCompareDisplayNames = propertiesDisplayNames.Aggregate((first, second) => first + ", " + second);

            string formattedErrorMessage = string.Format(formattingPattern, currentPropertyDisplayName, propertiesToCompareDisplayNames);

            return formattedErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                ResourceManager resourceManager = null;
                var propertiesDisplayNames = new List<string>();
                var propertiesToCompare = new List<PropertyInfo>();

                propertiesDisplayNames.Add(!string.IsNullOrEmpty(validationContext.DisplayName) ? validationContext.DisplayName : validationContext.MemberName);

                foreach (string propertyName in this.propertiesToCompareNames)
                {
                    PropertyInfo propertyToCompare = validationContext.ObjectInstance.GetType().GetProperty(propertyName);

                    if (propertyToCompare == null)
                    {
                        continue;
                    }

                    propertiesToCompare.Add(propertyToCompare);
                }

                foreach (PropertyInfo propertyToCompare in propertiesToCompare)
                {
                    object propertyToCompareValue = propertyToCompare?.GetValue(validationContext.ObjectInstance, null);

                    if (propertyToCompareValue == null)
                    {
                        continue;
                    }

                    if (!value.Equals(propertyToCompareValue))
                    {
                        var propertyDisplayAttribute = propertyToCompare.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;

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
                            propertiesDisplayNames.Add(propertyToCompare.Name);
                        }
                    }
                }

                if (propertiesDisplayNames.Count > 1)
                {
                    string propertiesDisplayNamesString = propertiesDisplayNames.Aggregate((first, second) => first + "," + second);

                    return new ValidationResult(this.FormatErrorMessage(propertiesDisplayNamesString));
                }
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ResourceManager resourceManager = null;
            var propertiesDisplayNames = new List<string>();
            var propertiesToCompare = new List<PropertyInfo>();

            propertiesDisplayNames.Add(metadata.GetDisplayName());

            foreach (string propertyName in this.propertiesToCompareNames)
            {
                PropertyInfo propertyToCompare = metadata.ContainerType.GetProperty(propertyName);

                if (propertyToCompare == null)
                {
                    continue;
                }

                propertiesToCompare.Add(propertyToCompare);
            }

            foreach (PropertyInfo propertyToCompare in propertiesToCompare)
            {
                var propertyDisplayAttribute = propertyToCompare.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;

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
                    propertiesDisplayNames.Add(propertyToCompare.Name);
                }
            }

            string propertiesDisplayNamesString = propertiesDisplayNames.Aggregate((first, second) => first + "," + second);
            propertiesDisplayNames.RemoveAt(0);

            string propertiesToCompareString = propertiesToCompare.Select(p => p.Name).Aggregate((first, second) => first + "," + second);
            string propertiesToCompareDisplayNames = propertiesDisplayNames.Aggregate((first, second) => first + "," + second);

            yield return new ModelClientValidationEqualsToMultipleRule(this.FormatErrorMessage(propertiesDisplayNamesString), propertiesToCompareString, propertiesToCompareDisplayNames);
        }
    }

    public class ModelClientValidationEqualsToMultipleRule : ModelClientValidationRule
    {
        public ModelClientValidationEqualsToMultipleRule(string errorMessage, string propertiesToCompare, string propertiesToCompareDisplayNames)
        {
            this.ValidationType = "equalstomultiple";
            this.ErrorMessage = errorMessage;
            this.ValidationParameters.Add("propertiestocompare", propertiesToCompare);
            this.ValidationParameters.Add("propertiestocomparedisplaynames", propertiesToCompareDisplayNames);
        }
    }
}

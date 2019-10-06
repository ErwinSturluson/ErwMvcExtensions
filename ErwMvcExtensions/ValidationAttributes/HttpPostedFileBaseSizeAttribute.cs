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
    //TODO: Fix error message size units from byte to dynamic in resources.
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class HttpPostedFileBaseSizeAttribute : ValidationAttribute, IClientValidatable
    {
        private int minSize;
        private int maxSize;
        private SizeUnit sizeUnit;

        public HttpPostedFileBaseSizeAttribute(int maxSize)
            : this(0, maxSize, SizeUnit.Byte)
        {
        }

        public HttpPostedFileBaseSizeAttribute(int maxSize, SizeUnit sizeUnit)
            : this(0, maxSize, sizeUnit)
        {
        }

        public HttpPostedFileBaseSizeAttribute(int minSize, int maxSize)
            : this(minSize, maxSize, SizeUnit.Byte)
        {
        }

        public HttpPostedFileBaseSizeAttribute(int minSize, int maxSize, SizeUnit sizeUnit)
        {
            this.minSize = minSize;
            this.maxSize = maxSize;
            this.sizeUnit = sizeUnit;
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
                    formattingPattern = resourceManager.GetString("HttpPostedFileBaseSize", CultureInfoExtensions.GetCultureFromHttp(HttpContext.Current.Request));
                }
                else if (string.IsNullOrEmpty(this.ErrorMessageResourceName))
                {
                    resourceManager = new ResourceManager(this.ErrorMessageResourceType);
                    formattingPattern = resourceManager.GetString("HttpPostedFileBaseSize", CultureInfoExtensions.GetCultureFromHttp(HttpContext.Current.Request));
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
                formattingPattern = "The size of the uploaded files for the {0} field should be in the range from {1} to {2} {3}.";
            }

            string formattedErrorMessage = string.Format(formattingPattern, name, this.minSize, this.maxSize, this.GetSizeUnitString(this.sizeUnit));

            return formattedErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string currentPropertyDisplayName = !string.IsNullOrEmpty(validationContext.DisplayName) ? validationContext.DisplayName : validationContext.MemberName;

            int minSizeInBytes = 1;
            int maxSizeInBytes = 1;

            try
            {
                switch (this.sizeUnit)
                {
                    case SizeUnit.Byte:
                        minSizeInBytes = this.minSize;
                        maxSizeInBytes = this.maxSize;
                        break;
                    case SizeUnit.Kilobyte:
                        minSizeInBytes = this.minSize * (int)SizeUnit.Kilobyte;
                        maxSizeInBytes = this.maxSize * (int)SizeUnit.Kilobyte;
                        break;
                    case SizeUnit.Megabyte:
                        minSizeInBytes = this.minSize * (int)SizeUnit.Megabyte;
                        maxSizeInBytes = this.maxSize * (int)SizeUnit.Megabyte;
                        break;
                    case SizeUnit.Gigabyte:
                        minSizeInBytes = this.minSize * (int)SizeUnit.Gigabyte;
                        maxSizeInBytes = this.maxSize * (int)SizeUnit.Gigabyte;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                minSizeInBytes = 1;
                maxSizeInBytes = int.MaxValue;
            }

            if (value as HttpPostedFileBase != null)
            {
                if ((value as HttpPostedFileBase).ContentLength < minSizeInBytes || (value as HttpPostedFileBase).ContentLength > maxSizeInBytes)
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
                    else if (file.ContentLength < minSizeInBytes || file.ContentLength > maxSizeInBytes)
                    {
                        return new ValidationResult(this.FormatErrorMessage(currentPropertyDisplayName));
                    }
                }
            }

            return ValidationResult.Success;
        }

        private string GetSizeUnitString(SizeUnit sizeUnit)
        {
            string sizeUnitString = string.Empty;

            switch (sizeUnit)
            {
                case SizeUnit.Byte:
                    sizeUnitString = "B";
                    break;
                case SizeUnit.Kilobyte:
                    sizeUnitString = "Kb";
                    break;
                case SizeUnit.Megabyte:
                    sizeUnitString = "Mb";
                    break;
                case SizeUnit.Gigabyte:
                    sizeUnitString = "Gb";
                    break;
                default:
                    break;
            }

            return sizeUnitString;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            string currentPropertyDisplayName = metadata.GetDisplayName();

            yield return new ModelClientValidationHttpPostedFileBaseSizeRule(this.FormatErrorMessage(currentPropertyDisplayName), this.minSize, this.maxSize, this.GetSizeUnitString(this.sizeUnit));
        }
    }

    public class ModelClientValidationHttpPostedFileBaseSizeRule : ModelClientValidationRule
    {
        public ModelClientValidationHttpPostedFileBaseSizeRule(string errorMessage, int minSize, int maxSize, string sizeUnit)
        {
            this.ValidationType = "httppostedfilebasesize";
            this.ErrorMessage = errorMessage;
            this.ValidationParameters.Add("minsize", minSize);
            this.ValidationParameters.Add("maxsize", maxSize);
            this.ValidationParameters.Add("sizeunit", sizeUnit);
        }
    }

    public enum SizeUnit : int
    {
        Byte = 1,
        Kilobyte = 1024,
        Megabyte = 1048576,
        Gigabyte = 1073741824
    }
}

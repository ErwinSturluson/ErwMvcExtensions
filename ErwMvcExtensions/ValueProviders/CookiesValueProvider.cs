using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErwMvcExtensions.ValueProviders
{
    public class CookiesValueProvider : IValueProvider
    {
        public bool ContainsPrefix(string prefix)
        {
            bool hasPrefix = HttpContext.Current.Request.Cookies.AllKeys.Contains(prefix);

            return hasPrefix;
        }

        public ValueProviderResult GetValue(string key)
        {
            ValueProviderResult valueProviderResult = null;
            HttpCookieCollection cookies = HttpContext.Current.Request.Cookies;

            if (ContainsPrefix(key))
            {
                valueProviderResult = new ValueProviderResult(cookies[key].Value, cookies[key].Value, CultureInfo.CurrentCulture);
            }

            return valueProviderResult;
        }
    }

    public class CookiesValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            return new CookiesValueProvider();
        }
    }
}

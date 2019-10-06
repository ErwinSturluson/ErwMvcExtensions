using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace ErwMvcExtensions.System
{
    public class CultureInfoExtensions : CultureInfo
    {
        public CultureInfoExtensions(string name)
            : base(name)
        {
        }

        public CultureInfoExtensions(int culture)
            : base(culture)
        {
        }

        public CultureInfoExtensions(string name, bool useUserOverride)
            : base(name, useUserOverride)
        {
        }

        public CultureInfoExtensions(int culture, bool useUserOverride)
            : base(culture, useUserOverride)
        {
        }

        public static CultureInfo GetCultureFromHttp(HttpRequestBase httpRequest)
        {
            return GetCultureFromHttpCollections(httpRequest.RequestContext.RouteData.Values, httpRequest.QueryString, httpRequest.UserLanguages, httpRequest.Cookies, httpRequest.Form);
        }

        public static CultureInfo GetCultureFromHttp(HttpRequest httpRequest)
        {
            return GetCultureFromHttpCollections(httpRequest.RequestContext.RouteData.Values, httpRequest.QueryString, httpRequest.UserLanguages, httpRequest.Cookies, httpRequest.Form);
        }

        private static CultureInfo GetCultureFromHttpCollections(RouteValueDictionary routeValues, NameValueCollection queryString, IEnumerable<string> languages, HttpCookieCollection cookies, NameValueCollection formValues)
        {
            string userCulture = routeValues["lang"] != null ?
                                 !string.IsNullOrEmpty(routeValues["lang"].ToString()) ?
                                 routeValues["lang"].ToString() : null : null;

            if (userCulture != null)
            {
                return new CultureInfo(userCulture);
            }

            userCulture = queryString["lang"] != null ?
                          !string.IsNullOrEmpty(queryString["lang"]) ?
                          queryString["lang"] : null : null;

            if (userCulture != null)
            {
                return new CultureInfo(userCulture);
            }


            userCulture = cookies["lang"] != null ?
                          !string.IsNullOrEmpty(cookies["lang"].Value) ?
                          cookies["lang"].Value : null : null;

            if (userCulture != null)
            {
                return new CultureInfo(userCulture);
            }

            userCulture = formValues["lang"] != null ?
                          !string.IsNullOrEmpty(formValues["lang"].ToString()) ?
                          formValues["lang"].ToString() : null : null;

            if (userCulture != null)
            {
                return new CultureInfo(userCulture);
            }

            userCulture = languages.Any() ? languages.First() : null;

            if (userCulture != null)
            {
                return new CultureInfo(userCulture);
            }

            return CultureInfo.InvariantCulture;
        }
    }
}

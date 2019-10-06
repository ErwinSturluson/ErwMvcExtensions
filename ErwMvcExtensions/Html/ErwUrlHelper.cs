using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace ErwMvcExtensions.Html
{
    public static class ErwUrlHelper
    {
        public static string GenerateSimpleUrl(RouteValueDictionary routeValues)
        {
            string generatedUrl = "/" + (routeValues["area"] != null ?
                                  routeValues["area"].ToString() + "/" : string.Empty) +
                                  routeValues["controller"].ToString() + "/" +
                                  routeValues["action"].ToString();

            return generatedUrl;
        }
    }
}

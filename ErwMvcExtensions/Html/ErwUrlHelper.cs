using System.Web.Routing;

namespace ErwMvcExtensions.Html
{
    public static class ErwUrlHelper
    {
        public static string GenerateSimpleUrl(object routeValues)
        {
            return GenerateSimpleUrl(new RouteValueDictionary(routeValues));
        }
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

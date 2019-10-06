using System.Reflection;
using System.Web.Routing;

namespace ErwMvcExtensions.System
{
    public static class AssemblyExtensions
    {
        public static string GetFullyQualifiedControllerName(this Assembly asm, object routeValues)
        {
            return asm.GetFullyQualifiedControllerName(new RouteValueDictionary(routeValues));
        }

        public static string GetFullyQualifiedControllerName(this Assembly asm, RouteValueDictionary routeValues)
        {
            string fullyQualifiedControllerName = asm.GetName().Name + "." +
                                                (routeValues["area"] != null ? routeValues["area"].ToString() + "." : string.Empty) +
                                                "Controllers." + (routeValues["controller"].ToString().EndsWith("Controller") ?
                                                routeValues["controller"].ToString() : routeValues["controller"].ToString() + "Controller");

            return fullyQualifiedControllerName;
        }
    }
}

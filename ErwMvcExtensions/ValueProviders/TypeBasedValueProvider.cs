using ErwMvcExtensions.System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ErwMvcExtensions.ValueProviders
{
    public class TypeBasedValueProvider : IValueProvider
    {
        private ControllerContext controllerContext;
        private ValueProviderResult valueProviderResult;

        public TypeBasedValueProvider(ControllerContext controllerContext)
        {
            this.controllerContext = controllerContext;
            this.valueProviderResult = null;
        }

        public bool ContainsPrefix(string prefix)
        {
            bool hasPrefix = false;

            Type prefixType = this.GetValueTypeByPrefix();

            valueProviderResult = this.GetValueByType(prefixType);

            if (valueProviderResult != null)
            {
                hasPrefix = true;
            }

            return hasPrefix;
        }

        public ValueProviderResult GetValue(string key)
        {
            if (valueProviderResult != null)
            {
                return this.valueProviderResult;
            }

            return null;
        }

        private Type GetValueTypeByPrefix()
        {
            Type valueType = null;

            MethodInfo[] actionMethods = this.controllerContext.Controller.GetType().GetMethods()
                .Where(m => !m.Name.StartsWith("get_") && !m.Name.StartsWith("set_")).ToArray();

            string actionName = this.controllerContext.RequestContext.RouteData.Values["action"].ToString();

            foreach (MethodInfo actionMethod in actionMethods)
            {
                string actionNameToCompare = actionName.ToLower();
                string actionMethodNameToCompare = actionMethod.Name.ToLower();

                if (actionNameToCompare == actionMethodNameToCompare && actionMethod.GetParameters().Any())
                {
                    valueType = actionMethod.GetParameters().First().ParameterType;
                    break;
                }
            }

            return valueType;
        }

        private ValueProviderResult GetValueByType(Type valueType)
        {
            ValueProviderResult valueProviderResult = null;

            if (valueType == null || !valueType.IsPrimitive && valueType.FullName != typeof(string).FullName)
            {
                return valueProviderResult;
            }

            var request = HttpContext.Current.Request;
            var dataSources = new List<IEnumerable>();

            dataSources.Add(request.Form.AllKeys.Any() ?
                request.Form.AllKeys.Select(k => request.Form[k.ToString()]).ToList()
                : null);

            dataSources.Add(request.RequestContext.RouteData.Values.Keys.Any() ?
                request.RequestContext.RouteData.Values.Keys
                .Where(k => k.ToLower() != "area" && k.ToLower() != "controller" && k.ToLower() != "action")
                .Select(k => request.RequestContext.RouteData.Values[k.ToString()]).ToList()
                : null);

            dataSources.Add(request.QueryString.AllKeys.Any() ?
                request.QueryString.AllKeys
                .Select(k => request.QueryString.GetValues(k))
                .Aggregate((first, second) =>
                {
                    second.CopyTo(first, first.Length); return second;
                }).ToList()
                : null);

            dataSources.Add(request.Cookies.AllKeys.Any() ?
                request.Cookies.AllKeys
                .Select(c => request.Cookies[c.ToString()].Value).ToList()
                : null);

            foreach (IEnumerable source in dataSources)
            {
                if (this.TrySetValueByType(out object value, valueType, source))
                {
                    return new ValueProviderResult(value, value.ToString(), CultureInfo.CurrentCulture);
                }
            }

            return valueProviderResult;
        }

        private bool TrySetValueByType(out object value, Type valueType, IEnumerable enumerable)
        {
            value = null;

            if (valueType == null || enumerable == null)
            {
                return false;
            }

            foreach (object item in enumerable)
            {
                value = valueType.FullName == typeof(string).FullName ? item.ToString() : valueType.ParseIfPrimitive(item);

                if (value != null)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public class TypeBasedValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            return new TypeBasedValueProvider(controllerContext);
        }
    }
}

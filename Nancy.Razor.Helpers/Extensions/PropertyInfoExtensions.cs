using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nancy.Razor.Helpers.Tag;

namespace Nancy.Razor.Helpers.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static HtmlInputType GetInputType(this PropertyInfo property)
        {

            var typeCode = Type.GetTypeCode(property.PropertyType);
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return HtmlInputType.Checkbox;
                case TypeCode.Double:
                case TypeCode.Decimal:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return HtmlInputType.Number;
                case TypeCode.DateTime:
                    return HtmlInputType.Datetime;
            }
            return HtmlInputType.Text;
        }

        public static string GetPresentationName(this PropertyInfo property)
        {
            var displayAttr = property.GetCustomAttribute<DisplayNameAttribute>();
            return displayAttr != null ? displayAttr.DisplayName : property.Name;
        }

        public static string GetValueAsString(this PropertyInfo property, object thisObject)
        {
            var valueString = string.Empty;
            if (!property.CanRead)
            {
                return valueString;
            }
            var valueObject = property.GetValue(thisObject);
            valueString = valueObject != null ? Convert.ToString(valueObject) : string.Empty;
            return valueString;
        }
    }
}

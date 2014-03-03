using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Razor.Helpers.Tag;

namespace Nancy.Razor.Helpers.Extensions
{
    public static class HtmlTagExtensions
    {
        public static void ApplyModelProperty(this HtmlTag tag, object model, string property)
        {
            if (model != null)
            {
                var modelProperty = model.GetType().GetProperty(property);
                if (modelProperty != null && modelProperty.CanRead)
                {
                    tag.WithNonEmptyAttribute("value", modelProperty.GetValue(model));
                }
            }

        }
    }
}

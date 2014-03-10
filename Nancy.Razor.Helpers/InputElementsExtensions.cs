using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using Nancy.Diagnostics;
using Nancy.Razor.Helpers.Extensions;
using Nancy.Razor.Helpers.Tag;
using Nancy.ViewEngines.Razor;

namespace Nancy.Razor.Helpers
{
    public static class InputElementsExtensions
    {

        public static IHtmlString TextBoxFor<TModel, TR>(this HtmlHelpers<TModel> html,
            Expression<Func<TModel, TR>> prop) where TModel : class
        {
            return TextBoxFor(html, prop, null);
        }

        public static IHtmlString TextBoxFor<TModel, TR>(this HtmlHelpers<TModel> html,
            Expression<Func<TModel, TR>> prop, object htmlAttributes) where TModel : class
        {
            var tag = HtmlTagBuilder.CreateInputElementFor(html, prop, HtmlInputType.Text, htmlAttributes);

            AppendValidationResults(html, tag);

            if (html.Model != null)
            {
                AppendOldValue(html, tag);
            }

            return tag != null ? new NonEncodedHtmlString(tag.ToString()) : NonEncodedHtmlString.Empty;
        }

        private static void AppendOldValue<TModel>(HtmlHelpers<TModel> html, HtmlTag tag)
        {
            var type = html.Model.GetType();
            var name = tag.Attribute("name").Value;
            var property = type.GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Instance);
            if (property != null && property.CanRead)
            {
                var value = property.GetValue(html.Model);
                var valueString = value != null ? value.ToString() : string.Empty;
                tag.WithAttribute("value", valueString);
            }
        }

        private static void AppendValidationResults<TModel>(HtmlHelpers<TModel> htmlHelper, HtmlTag tag)
        {
            var validationResult = htmlHelper.RenderContext.Context.ModelValidationResult;
            var name = tag.Attribute("name").Value;
            if (validationResult == null) return;

            if (validationResult.Errors.Any(error => error.Key.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
            {
                var classAttr = tag.Attribute("class");
                var @class = classAttr != null
                    ? string.Concat(classAttr.Value, " input-error")
                    : "input-error";
                tag.RemoveAttribute("class").WithAttribute("class", @class);
            }
        }

        public static IHtmlString PasswordFor<TModel, TR>(this HtmlHelpers<TModel> html,
            Expression<Func<TModel, TR>> prop) where TModel : class
        {
            return PasswordFor(html, prop, null);
        }

        public static IHtmlString PasswordFor<TModel, TR>(this HtmlHelpers<TModel> html,
            Expression<Func<TModel, TR>> prop, object htmlAttributes) where TModel : class
        {
            var tag = HtmlTagBuilder.CreateInputElementFor(html, prop, HtmlInputType.Password, htmlAttributes);
            return tag != null ? new NonEncodedHtmlString(tag.ToString()) : NonEncodedHtmlString.Empty;
        }

        public static IHtmlString HiddenFor<TModel, TR>(this HtmlHelpers<TModel> html,
            Expression<Func<TModel, TR>> prop) where TModel : class
        {
            return HiddenFor(html, prop, null);
        }

        public static IHtmlString HiddenFor<TModel, TR>(this HtmlHelpers<TModel> html,
            Expression<Func<TModel, TR>> prop, object htmlAttributes) where TModel : class
        {
            var tag = HtmlTagBuilder.CreateInputElementFor(html, prop, HtmlInputType.Hidden, htmlAttributes);
            return tag != null ? new NonEncodedHtmlString(tag.ToString()) : NonEncodedHtmlString.Empty;
        }

        public static IHtmlString CheckBoxFor<TModel, TR>(this HtmlHelpers<TModel> html,
            Expression<Func<TModel, TR>> prop) where TModel : class
        {
            return CheckBoxFor(html, prop, null);
        }

        public static IHtmlString CheckBoxFor<TModel, TR>(this HtmlHelpers<TModel> html,
            Expression<Func<TModel, TR>> prop, object htmlAttributes) where TModel : class
        {
            var tag = HtmlTagBuilder.CreateCheckBoxFor(html, prop, htmlAttributes);
            return tag != null ? new NonEncodedHtmlString(tag.ToString()) : NonEncodedHtmlString.Empty;
        }



        public static IHtmlString InputElementFor<TModel, TR>(this HtmlHelpers<TModel> html,
            Expression<Func<TModel, TR>> prop) where TModel : class
        {
            return InputElementFor(html, prop, null);
        }
        public static IHtmlString InputElementFor<TModel, TR>(this HtmlHelpers<TModel> html,
            Expression<Func<TModel, TR>> prop, object htmlAttributes) where TModel : class
        {
            var property = prop.AsPropertyInfo();
            if (property == null) return NonEncodedHtmlString.Empty;
            var inputType = GetInputTypeByProperty(property);
            var tag = inputType == HtmlInputType.Checkbox ?
                HtmlTagBuilder.CreateCheckBoxFor(html, prop, htmlAttributes) :
                HtmlTagBuilder.CreateInputElementFor(html, prop, inputType, htmlAttributes);

            return tag != null ? new NonEncodedHtmlString(tag.ToString()) : NonEncodedHtmlString.Empty;
        }

        private static HtmlInputType GetInputTypeByProperty(PropertyInfo property)
        {
            var dataTypeMetadata = property.GetCustomAttribute<DataTypeAttribute>();
            if (dataTypeMetadata != null)
            {
                switch (dataTypeMetadata.DataType)
                {
                    case DataType.Password:
                        return HtmlInputType.Password;
                    case DataType.Date:
                        return HtmlInputType.Date;
                    case DataType.DateTime:
                        return HtmlInputType.Datetime;
                    case DataType.PhoneNumber:
                        return HtmlInputType.Tel;
                    case DataType.Text:
                        return HtmlInputType.Text;
                    case DataType.Time:
                        return HtmlInputType.Time;
                    case DataType.EmailAddress:
                        return HtmlInputType.Email;
                    case DataType.Url:
                        return HtmlInputType.Url;
                }
            }
            return property.GetInputType();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nancy.Razor.Helpers.Tag;
using Nancy.ViewEngines.Razor;

namespace Nancy.Razor.Helpers
{
    public static class ValidationMessageExtensions
    {
        public static IHtmlString ValidationMessageFor<TModel, TR>(this HtmlHelpers<TModel> html,
            Expression<Func<TModel, TR>> prop) where TModel : class
        {
            var validationResult = html.RenderContext.Context.ModelValidationResult;
            if (validationResult == null || validationResult.IsValid)
            {
                return NonEncodedHtmlString.Empty;
            }

            var property = (prop.Body as MemberExpression).Member as PropertyInfo;
            var keyName = property.Name;
            var hasError = validationResult.Errors.Any(err => keyName.Equals(err.Key));
            if (!hasError)
            {
                return NonEncodedHtmlString.Empty;
            }
            var error = validationResult.Errors.First(err => keyName.Equals(err.Key));

            var tag = new HtmlTag("span").WithAttribute("class", "error-message");
            tag.RawContent = error.Value.First().ErrorMessage;
            return new NonEncodedHtmlString(tag.ToString());
        }



        public static IHtmlString ValidationSummaryFor<TModel, TR>(this HtmlHelpers<TModel> html,
            Expression<Func<TModel, TR>> prop) where TModel : class
        {
            return ValidationSummaryFor(html, prop, false);
        }

        public static IHtmlString ValidationSummaryFor<TModel, TR>(this HtmlHelpers<TModel> html,
            Expression<Func<TModel, TR>> prop, bool useAllKeys) where TModel : class
        {
            var validationResult = html.RenderContext.Context.ModelValidationResult;
            if (validationResult == null || validationResult.IsValid)
            {
                return NonEncodedHtmlString.Empty;
            }

            var property = (prop.Body as MemberExpression).Member as PropertyInfo;
            var keyName = property.Name;
            var tag = new HtmlTag("ul").WithAttribute("class", "error-list");

            var errorsToLook = useAllKeys
                ? validationResult.Errors
                : validationResult.Errors.Where(x => x.Key == string.Empty);

            foreach (var error in errorsToLook)
            {
                var evalues = error.Value.Where(ev => ev.MemberNames.Contains(keyName)).ToList();
                foreach (var evalue in evalues)
                {
                    var child = tag.WithChild("li").WithAttribute("class", "error-list-item");
                    child.RawContent = evalue.ErrorMessage;
                }
            }

            return new NonEncodedHtmlString(tag.ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nancy.Razor.Helpers.Extensions;
using Nancy.ViewEngines.Razor;

namespace Nancy.Razor.Helpers
{
    public static class EditorExtensions
    {
        public static IHtmlString EditorFor<TModel, TR>(this HtmlHelpers<TModel> html,
            Expression<Func<TModel, TR>> prop) where TModel: class
        {

            var property = prop.AsPropertyInfo();
            if (property == null) return NonEncodedHtmlString.Empty;
            var uihintAttribute = property.GetCustomAttribute<UIHintAttribute>();

            var viewName = uihintAttribute != null
                ? uihintAttribute.UIHint
                : property.PropertyType.Name;

            var modelForEditor = html.Model != null && property.CanRead
                ? property.GetValue(html.Model)
                : null;

            var viewLocation = GetViewLocation(viewName);

            var view = html.RenderContext.LocateView(viewLocation, modelForEditor);
            var response = html.Engine.RenderView(view, modelForEditor, html.RenderContext);
            var action = response.Contents;
            var mem = new MemoryStream();
            action.Invoke(mem);
            mem.Position = 0;
            var reader = new StreamReader(mem);
            return new NonEncodedHtmlString(reader.ReadToEnd());
        }

        private static string GetViewLocation(string viewName)
        {
            return viewName.Contains("/")
                ? viewName
                : "Shared/EditorTemplates/" + viewName;
        }
    }
}

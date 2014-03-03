using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nancy.Razor.Helpers.Extensions
{
    public static class ExpressionExtensions
    {
        public static PropertyInfo AsPropertyInfo<T, TR>(this Expression<Func<T, TR>> expr)
        {
            var memExpr = expr.Body as MemberExpression;
            return memExpr != null ? memExpr.Member as PropertyInfo : null;
        }
    }
}

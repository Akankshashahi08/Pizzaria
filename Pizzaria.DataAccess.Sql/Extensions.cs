using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Pizzaria.DataAccess.Sql
{
    public static class Extensions
    {
        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(this Expression<Func<TSource, TProperty>> exp)
        {
            var member = exp.Body as MemberExpression;
            return member?.Member as PropertyInfo;
        }
    }
}

using System;
using System.Linq.Expressions;

namespace Showcase.Wpf.Base.Extensions
{
    public static class ExpressionExtensions
    {
        public static string ToPropertyName<TProperty>(this Expression<Func<TProperty>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var memberExpression = propertyExpression.Body as MemberExpression;

            if (memberExpression == null)
            {
                var unaryExpression = propertyExpression.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }

            if (memberExpression == null)
            {
                throw new ArgumentException("Invalid expression.", "propertyExpression");
            }

            return memberExpression.Member.Name;
        }
    }
}

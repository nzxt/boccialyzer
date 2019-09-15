using Blyzer.Domain.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Blyzer.Domain.Filtering
{
    public static class FilterHelper
    {
        private static readonly MethodInfo StringContainsMethod = typeof(string).GetMethod("Contains");

        private static readonly MethodInfo StartsWithMethod =
            typeof(string).GetMethod("StartsWith", new[] { typeof(string) });

        private static readonly MethodInfo
            EndsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });

        private static readonly MethodInfo TrimMethod = typeof(string).GetMethod("Trim", new Type[0]);
        private static readonly MethodInfo ToLowerMethod = typeof(string).GetMethod("ToLower", new Type[0]);

        private static Dictionary<string, FilterOperator> FilterOperatorsDictionary =>
            new Dictionary<string, FilterOperator>
            {
                {"==", FilterOperator.EqualTo},
                {"!=", FilterOperator.NotEqualTo},
                {">>", FilterOperator.GreaterThan},
                {">=", FilterOperator.GreaterThanOrEqualTo},
                {"<<", FilterOperator.LessThan},
                {"<=", FilterOperator.LessThanOrEqualTo},
                {"@=", FilterOperator.Contains},
                {"_=", FilterOperator.StartsWith},
                {"=_", FilterOperator.EndsWith},
                //{ "=*", FilterOperator.IsNull },
                //{ "!*", FilterOperator.IsNotNull },
                //{ "@@", FilterOperator.IsEmpty },
                //{ "!@", FilterOperator.IsNotEmpty },
                //{ "@*", FilterOperator.IsNullOrWhiteSpace },
                //{ "*@", FilterOperator.IsNotNullNorWhiteSpace }
            };

        private static Dictionary<FilterOperator, Func<Expression, Expression, Expression>> FilterExpressionsDictionary
            => new Dictionary<FilterOperator, Func<Expression, Expression, Expression>>
            {
                {FilterOperator.EqualTo, (member, constant) => Expression.Equal(member, constant)},
                {FilterOperator.NotEqualTo, (member, constant) => Expression.NotEqual(member, constant)},
                {FilterOperator.GreaterThan, (member, constant) => Expression.GreaterThan(member, constant)},
                {
                    FilterOperator.GreaterThanOrEqualTo,
                    (member, constant) => Expression.GreaterThanOrEqual(member, constant)
                },
                {FilterOperator.LessThan, (member, constant) => Expression.LessThan(member, constant)},
                {FilterOperator.LessThanOrEqualTo, (member, constant) => Expression.LessThanOrEqual(member, constant)},
                { FilterOperator.Contains, (member, constant) => Contains(member, constant) },
                {FilterOperator.StartsWith, (member, constant) => Expression.Call(member, StartsWithMethod, constant)},
                {FilterOperator.EndsWith, (member, constant) => Expression.Call(member, EndsWithMethod, constant)},
                //{ FilterOperator.IsNull, (member, constant) => Expression.Equal(member, Expression.Constant(null)) },
                //{ FilterOperator.IsNotNull, (member, constant) => Expression.NotEqual(member, Expression.Constant(null)) },
                //{ FilterOperator.IsEmpty, (member, constant) => Expression.Equal(member, Expression.Constant(string.Empty)) },
                //{ FilterOperator.IsNotEmpty, (member, constant) => Expression.NotEqual(member, Expression.Constant(string.Empty)) }
                //{ FilterOperator.IsNullOrWhiteSpace, (member, constant) => IsNullOrWhiteSpace(member) },
                //{ FilterOperator.IsNotNullNorWhiteSpace, (member, constant) => IsNotNullNorWhiteSpace(member) }
            };


        public static Dictionary<string, FilterOperator> GetFilterOperators => FilterOperatorsDictionary;

        public static Dictionary<FilterOperator, Func<Expression, Expression, Expression>> GetFilterExpressions => FilterExpressionsDictionary;

        public static Expression GetBodyExpression(MemberExpression member, FilterTerm filter)
        {
            var typeConverter = TypeDescriptor.GetConverter(member.Type);
            Expression resultExpr = null;

            foreach (var value in filter.Values)
            {
                dynamic constantVal = typeConverter.CanConvertFrom(typeof(string))
                    ? typeConverter.ConvertFrom(value)
                    : Convert.ChangeType(value, member.Type);
                var constant = FilterHelper.GetConstantExpression(constantVal);

                var body = FilterHelper.GetFilterExpressions[filter.Operator].Invoke(member, constant);
                resultExpr = resultExpr == null ?
                    (Expression)body :
                    (Expression)Expression.Or(resultExpr, body);
            }

            return resultExpr;
        }

        public static MemberExpression GetMemberExpression(Expression parameter, Type entityType, string propertyName)
        {
            var propertyInfo = entityType.GetProperties()
                .FirstOrDefault(x => x.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
            if (propertyInfo == null) return null;

            return Expression.PropertyOrField(parameter, propertyInfo.Name.Split('.').Last());
        }

        /// <summary>
        /// Retrieve 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expression GetConstantExpression(object value)
        {
            if (value == null) return null;

            Expression constant = Expression.Constant(value);

            if (value is string)
            {
                var trimConstantCall = Expression.Call(constant, TrimMethod);
                constant = Expression.Call(trimConstantCall, ToLowerMethod);
            }

            return constant;
        }


        //#region Operations 
        private static Expression Contains(Expression member, Expression expression)
        {
            MethodCallExpression contains = null;
            if (expression is ConstantExpression constant && constant.Value is IList && constant.Value.GetType().IsGenericType)
            {
                var type = constant.Value.GetType();
                var containsInfo = type.GetMethod("Contains", new[] { type.GetGenericArguments()[0] });
                contains = Expression.Call(constant, containsInfo, member);
            }

            return contains ?? Expression.Call(member, StringContainsMethod, expression); ;
        }

        //private static Expression Between(Expression member, Expression constant, Expression constant2)
        //{
        //    var left = Expressions[Operation.GreaterThanOrEqualTo].Invoke(member, constant, null);
        //    var right = Expressions[Operation.LessThanOrEqualTo].Invoke(member, constant2, null);

        //    return CombineExpressions(left, right, FilterStatementConnector.And);
        //}

        //private static Expression IsNullOrWhiteSpace(Expression member)
        //{
        //    Expression exprNull = Expression.Constant(null);
        //    var trimMemberCall = Expression.Call(member, helper.trimMethod);
        //    Expression exprEmpty = Expression.Constant(string.Empty);
        //    return Expression.OrElse(
        //                            Expression.Equal(member, exprNull),
        //                            Expression.Equal(trimMemberCall, exprEmpty));
        //}

        //private static Expression IsNotNullNorWhiteSpace(Expression member)
        //{
        //    Expression exprNull = Expression.Constant(null);
        //    var trimMemberCall = Expression.Call(member, helper.trimMethod);
        //    Expression exprEmpty = Expression.Constant(string.Empty);
        //    return Expression.AndAlso(
        //                            Expression.NotEqual(member, exprNull),
        //                            Expression.NotEqual(trimMemberCall, exprEmpty));
        //}
        //#endregion

    }
}

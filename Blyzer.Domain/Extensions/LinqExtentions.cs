using Blyzer.Domain.Enums;
using Blyzer.Domain.Models.Fsp;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Blyzer.Domain.Extensions
{
    /// <summary>
    /// LINQ Extentions
    /// </summary>
    public static partial class LinqExtentions
    {
        #region # ApplyFiltering(List<FilterTerm> filters)

        /// <summary>
        /// Apply filters
        /// </summary>
        /// <typeparam name="TEntity">Entity</typeparam>
        /// <param name="result"></param>
        /// <param name="filters">Filters list</param>
        /// <returns></returns>
        public static IQueryable<TEntity> ApplyFiltering<TEntity>(this IQueryable<TEntity> result, List<FilterTerm> filters)
        {
            if (filters == null) return result;

            Expression outerExpression = null;
            var parameterExpression = Expression.Parameter(typeof(TEntity), "e");

            foreach (var filterTerm in filters)
            {
                Expression innerExpression = null;

                foreach (var filterTermName in filterTerm.Names)
                {
                    if (filterTerm.Values == null) continue;

                    var propertyInfo = typeof(TEntity).GetProperties().FirstOrDefault(x => x.Name.Equals(filterTermName, StringComparison.OrdinalIgnoreCase));

                    //var fullName = propertyInfo.Value?.FullName;
                    //var property = propertyInfo.Key;

                    if (propertyInfo != null)
                    {
                        var typeConverter = TypeDescriptor.GetConverter(propertyInfo.PropertyType);
                        dynamic propValue = parameterExpression;
                        foreach (var part in propertyInfo.Name.Split('.'))
                        {
                            propValue = Expression.PropertyOrField(propValue, part);
                        }
                        if (filterTerm.Values == null) continue;

                        foreach (var filterTermValue in filterTerm.Values)
                        {

                            dynamic constantVal = typeConverter.CanConvertFrom(typeof(string))
                                ? typeConverter.ConvertFrom(filterTermValue)
                                : Convert.ChangeType(filterTermValue, propertyInfo.PropertyType);

                            Expression filterValue = Expression.Constant(constantVal, propertyInfo.PropertyType);

                            //propValue = Expression.Call(propValue,
                            //    typeof(string).GetMethods()
                            //        .First(m => m.Name == "ToUpper" && m.GetParameters().Length == 0));

                            filterValue = Expression.Call(filterValue,
                                typeof(string).GetMethods()
                                    .First(m => m.Name == "ToUpper" && m.GetParameters().Length == 0));

                            var expression = GetExpression(filterTerm, filterValue, propValue);

                            if (filterTerm.OperatorIsNegated)
                                expression = Expression.Not(expression);

                            innerExpression = innerExpression == null ?
                                (Expression)expression :
                                (Expression)Expression.Or(innerExpression, expression);
                        }
                    }
                    else
                        throw new Exception($"Property {typeof(TEntity)}.{filterTermName} is not found.");
                }
                if (outerExpression == null)
                {
                    outerExpression = innerExpression;
                    continue;
                }
                if (innerExpression == null) continue;
                outerExpression = Expression.And(outerExpression, innerExpression);
            }
            return outerExpression == null
                ? result
                : result.Where(Expression.Lambda<Func<TEntity, bool>>(outerExpression, parameterExpression));
        }

        #endregion
        #region #  ApplySorting<TEntity>(List<SortTerm> sortTerms)

        public static IQueryable<TEntity> ApplySorting<TEntity>(this IQueryable<TEntity> result, List<SortTerm> sortTerms)
        {
            if (sortTerms == null) return result;

            var useThenBy = false;
            foreach (var sortTerm in sortTerms)
            {
                var propertyInfo = typeof(TEntity).GetProperties().FirstOrDefault(x => x.Name.ToUpper() == sortTerm.Name.ToUpper());
                if (propertyInfo != null)
                    result = result.OrderByDynamic(propertyInfo, sortTerm.Descending, useThenBy);
                useThenBy = true;
            }
            return result;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="desc"></param>
        /// <param name="useThenBy"></param>
        /// <returns></returns>
        private static IQueryable<TEntity> OrderByDynamic<TEntity>(this IQueryable<TEntity> source
            , PropertyInfo propertyInfo
            , bool desc, bool useThenBy)
        {
            string command = desc ?
                (useThenBy ? "ThenByDescending" : "OrderByDescending") :
                (useThenBy ? "ThenBy" : "OrderBy");
            try
            {
                var type = typeof(TEntity);
                var parameter = Expression.Parameter(type, "p");

                dynamic propertyValue = parameter;
                propertyValue = Expression.PropertyOrField(propertyValue, propertyInfo.Name);

                var propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);
                var orderByExpression = Expression.Lambda(propertyAccess, parameter);
                var resultExpression = Expression.Call(typeof(Queryable), command
                    , new Type[] { type, propertyInfo.PropertyType },
                    source.Expression, Expression.Quote(orderByExpression));
                return source.Provider.CreateQuery<TEntity>(resultExpression);
            }
            catch (Exception ex)
            {
                Log.Error("{Error}", ex.Message);
            }

            return source;
        }


        /// <summary>
        /// Apply Pagination
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="result"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static async Task<Page<TEntity>> ApplyPagination<TEntity>(this IQueryable<TEntity> result, GetParametersModel model)
        {
            var page = model?.Page ?? 1;
            var pageSize = model?.PageSize ?? 25;
            var totalItem = await result.CountAsync();

            result = result.Skip((page - 1) * pageSize).Take(pageSize);

            return new Page<TEntity>(items: result, pageNumber: page, pageSize: pageSize, totalItem: totalItem);
        }


        private static Expression GetExpression(FilterTerm filterTerm, dynamic filterValue, dynamic propertyValue)
        {
            switch (filterTerm.OperatorParsed)
            {
                case FilterOperator.Equals:
                    return Expression.Equal(propertyValue, Expression.Constant(filterValue));
                case FilterOperator.NotEquals:
                    return Expression.NotEqual(propertyValue, filterValue);
                case FilterOperator.GreaterThan:
                    return Expression.GreaterThan(propertyValue, filterValue);
                case FilterOperator.LessThan:
                    return Expression.LessThan(propertyValue, filterValue);
                case FilterOperator.GreaterThanOrEqualTo:
                    return Expression.GreaterThanOrEqual(propertyValue, filterValue);
                case FilterOperator.LessThanOrEqualTo:
                    return Expression.LessThanOrEqual(propertyValue, filterValue);
                case FilterOperator.Contains:
                    return Expression.Call(propertyValue,
                        typeof(string).GetMethods()
                            .First(m => m.Name == "Contains" && m.GetParameters().Length == 1),
                        filterValue);
                case FilterOperator.StartsWith:
                    return Expression.Call(propertyValue,
                        typeof(string).GetMethods()
                            .First(m => m.Name == "StartsWith" && m.GetParameters().Length == 1),
                        filterValue);
                default:
                    return Expression.Equal(propertyValue, filterValue);
            }
        }


    }
}
